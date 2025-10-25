using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Utils;
using ProjectInfo = Utils.ProjectInfo;

namespace GodotToolkits.I18N.Generators;

[Generator(LanguageNames.CSharp)]
public sealed class CsvFileGenerator : IIncrementalGenerator
{
	private readonly string _namespace = ProjectInfo.Title;

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var files = context.AdditionalTextsProvider.Where(file =>
			file.Path.EndsWith(".csv")
		);
		context.RegisterSourceOutput(files, GenerateCode);
	}

	private void GenerateCode(
		SourceProductionContext context,
		AdditionalText source
	)
	{
		var fileName = Path.GetFileNameWithoutExtension(source.Path);
		var lines = source.GetText()?.ToString().Split('\n') ?? [];
		if (lines.Length < 2)
			return;
		var indexes = GetIndexes([.. lines.Skip(1)]);

		context.AddSource(
			$"{fileName}.Generated.cs",
			BuildCode(indexes, fileName, _namespace)
		);
	}

	public static string BuildCode(
		List<string> indexes,
		string className,
		string? @namespace = null
	)
	{
		var code = new StringBuilder();
		code.AppendLine(AttributeStringBuild.GeneratedTitle);
		if (!string.IsNullOrEmpty(@namespace))
			code.AppendLine($"namespace {@namespace};");
		code.AppendLine();
		code.AppendLine($"public static class {className}Index {{");
		foreach (
			var index in indexes.Where(index => !string.IsNullOrEmpty(index))
		)
		{
			code.AppendLine($"\tpublic const string {index} = \"{index}\";");
		}

		code.AppendLine("}");
		return code.ToString();
	}

	public static List<string> GetIndexes(string[] csvLines)
	{
		var indexes = new List<string>();
		foreach (var line in csvLines)
		{
			var first = line.Split(',')[0];
			indexes.Add(first);
		}

		return indexes;
	}
}
