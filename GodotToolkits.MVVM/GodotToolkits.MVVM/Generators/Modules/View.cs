using System;
using Utils;
using Microsoft.CodeAnalysis;
using ProjectInfo = Utils.ProjectInfo;

namespace GodotToolkits.MVVM.Generators.Modules;

[Generator(LanguageNames.CSharp)]
public sealed class View : IIncrementalGenerator
{
	public const string AttributeName = "View";
	public const string ClassName = "ViewAttribute";
	public static readonly string Namespace = ProjectInfo.Title;

	public static string BuildCode()
	{
		return $@"{AttributeStringBuild.GeneratedTitle}

namespace {Namespace};
using global::System;

[AttributeUsage(AttributeTargets.Class)]
{AttributeStringBuild.GeneratedCode
(
	$"{ProjectInfo.Title}.Generators.Modules.View"
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
