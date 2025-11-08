using System;
using GodotToolkits.Utils;
using Microsoft.CodeAnalysis;
using ProjectInfo = Utils.ProjectInfo;

namespace GodotToolkits.MVVM.Generator.Generators.Modules;

[Generator(LanguageNames.CSharp)]
public sealed class BaseViewModel : IIncrementalGenerator
{
	public const string ClassName = "BaseViewModel";
	public const string Namespace = ProjectInfo.Title + ".MVVM";

	public static string BuildCode()
	{
		return $@"{AttributeStringBuild.GeneratedTitle(nameof(BaseViewModel), ProjectInfo.MvvmVersion,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))}

namespace {Namespace};
using global::System;

{AttributeStringBuild.GeneratedCode
(
	$"{Namespace}.Generators.Modules.BaseViewModel", ProjectInfo.
		MvvmVersion)}
public abstract class {ClassName}
{{
}}";
	}

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterPostInitializationOutput(post =>
		{
			post.AddSource($"{ClassName}.g.cs", BuildCode());
		});
	}
}
