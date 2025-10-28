using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using ProjectInfo = Utils.ProjectInfo;

namespace GodotToolkits.I18N.Generators;

[Generator(LanguageNames.CSharp)]
public sealed class CsvFileGenerator : IIncrementalGenerator
{
	private readonly string _namespace = $"{ProjectInfo.Title}Extension";

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
		var lines =
			source
				.GetText()
				?.ToString()
				.Split('\n')
				.Where(line => !string.IsNullOrEmpty(line))
				.ToArray() ?? [];
		if (lines.Length < 2)
			return;
		var indexes = GetIndexes([.. lines.Skip(1)]);

		context.AddSource(
			$"{fileName}.Index.Generated.cs",
			BuildIndexClass(fileName, indexes, _namespace)
		);
		context.AddSource(
			$"{fileName}.Generated.cs",
			BuildContentClass(fileName, GetCsvData(lines), _namespace)
		);
	}

	public static string BuildContentClass(
		string className,
		List<CsvContent> data,
		string? @namespace = null
	)
	{
		var code = new StringBuilder();
		if (!string.IsNullOrEmpty(@namespace))
			code.AppendLine($"namespace {@namespace};");
		code.AppendLine("using global::System.Collections.Generic;");
		code.AppendLine();
		code.AppendLine($"public static class {className} {{");
		code.AppendLine();
		code.AppendLine(
			$"\tpublic static List<Dictionary<string, string>> Data => ["
		);
		foreach (var csvContent in data)
		{
			code.AppendLine($"\t\t@{csvContent.Index},");
		}

		code.AppendLine("\t];");
		code.AppendLine();
		foreach (var content in data)
		{
			code.AppendLine(
				$"\tpublic static Dictionary<string, string> @{content.Index} => new()"
			);
			code.AppendLine("\t{");
			foreach (var tuple in content.Data)
			{
				var key = tuple.Key;
				var value = tuple.Value;
				code.AppendLine($"\t\t{{\"{key}\", \"{value}\"}},");
			}

			code.AppendLine("\t};");
			code.AppendLine();
		}

		code.AppendLine("}");
		return code.ToString();
	}

	public static string BuildIndexClass(
		string className,
		List<string> indexes,
		string? @namespace = null
	)
	{
		var code = new StringBuilder();
		if (!string.IsNullOrEmpty(@namespace))
			code.AppendLine($"namespace {@namespace};");
		code.AppendLine();
		code.AppendLine("#if GODOT");
		code.AppendLine("using global::Godot;");
		code.AppendLine("[GlobalClass]");
		code.AppendLine($"public static class {className}Index : RefCounted");
		code.AppendLine("#else");
		code.AppendLine($"public static class {className}Index");
		code.AppendLine("#endif");
		code.AppendLine("{");
		foreach (
			var index in indexes.Where(index => !string.IsNullOrEmpty(index))
		)
		{
			code.AppendLine($"\tpublic const string {index} = \"{index}\";");
		}

		code.AppendLine("}");
		return code.ToString();
	}

	public static List<CsvContent> GetCsvData(string[] csvLines)
	{
		var titles = csvLines[0]
			.Split(',')
			.Select(title => title.Trim())
			.ToArray();
		var data = new List<CsvContent>();
		for (var i = 1; i < csvLines.Length; i++)
		{
			var values = csvLines[i].Split(',');
			var content = new CsvContent { Index = values[0], Data = [] };
			for (var j = 0; j < titles.Length; j++)
			{
				content.Data[titles[j]] = values[j].Replace("\"", "\\\"");
			}

			data.Add(content);
		}

		return data;
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

	public sealed class CsvContent
	{
		public string Index { get; set; } = "";
		public Dictionary<string, string> Data { get; set; } = [];

		public override string ToString()
		{
			return $"{Index}: {string.Join(", ", Data.Select(pair => $"{pair.Key}: {pair.Value}"))}";
		}
	}
}
