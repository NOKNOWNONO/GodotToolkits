using System;
using GodotToolkits.Utils;
using Microsoft.CodeAnalysis;
using ProjectInfo = Utils.ProjectInfo;

namespace GodotToolkits.MVVM.Generator.Generators.Modules;

[Generator(LanguageNames.CSharp)]
public sealed class View : IIncrementalGenerator
{
	public const string AttributeName = "View";
	public const string ClassName = "ViewAttribute";
	public const string Namespace = ProjectInfo.Title + ".MVVM";

	public static string BuildCode()
	{
		return $@"{AttributeStringBuild.GeneratedTitle(nameof(View), ProjectInfo.MvvmVersion,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))}

namespace {Namespace};
using global::System;

[AttributeUsage(AttributeTargets.Class)]
{AttributeStringBuild.GeneratedCode
(
	$"{ProjectInfo.Title}.Generators.Modules.View"
	, ProjectInfo.MvvmVersion
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
