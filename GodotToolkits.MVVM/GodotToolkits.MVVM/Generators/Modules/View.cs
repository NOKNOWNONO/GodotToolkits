using System;
using GeneratorUtils;
using Microsoft.CodeAnalysis;
using ProjectInfo = GeneratorUtils.ProjectInfo;

namespace GodotToolkits.MVVM.Generators.Modules;

[Generator(LanguageNames.CSharp)]
public sealed class View : IIncrementalGenerator
{
	public const string AttributeName = "View";
	public const string ClassName = "ViewAttribute";
	public static readonly string Namespace = ProjectInfo.RootNamespace;

	public static string BuildCode()
	{
		return $@"{AttributeStringBuild.GeneratedTitle}

namespace {Namespace};
using global::System;

[AttributeUsage(AttributeTargets.Class)]
{AttributeStringBuild.GeneratedCode
(
	$"{ProjectInfo.RootNamespace}.Generators.Modules.View"
	, ProjectInfo.Version
)}

public sealed class {ClassName} : Attribute
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
