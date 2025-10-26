using Microsoft.CodeAnalysis;
using Utils;
using ProjectInfo = Utils.ProjectInfo;

namespace GodotToolkits.MVVM.Generators.Modules;

[Generator(LanguageNames.CSharp)]
public sealed class ObservableProperty : IIncrementalGenerator
{
	public const string AttributeName = "ObservableProperty";
	public const string ClassName = "ObservablePropertyAttribute";
	public static readonly string Namespace = ProjectInfo.Title;

	public static string BuildCode()
	{
		return $@"{AttributeStringBuild.GeneratedTitle}

namespace {Namespace};
using global::System;

[AttributeUsage(AttributeTargets.Field)]
{AttributeStringBuild.GeneratedCode(
	$"{ProjectInfo.Title}.Generators.Modules.ObservableProperty"
	, ProjectInfo.Version
)}
public class {ClassName} : Attribute
{{
}}
";
	}

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterPostInitializationOutput(post =>
		{
			post.AddSource($"{ClassName}.g.cs", BuildCode());
		});
	}
}
