using GodotToolkits.Utils;
using Microsoft.CodeAnalysis;
using Utils;
using ProjectInfo = Utils.ProjectInfo;

namespace GodotToolkits.MVVM.Generator.Generators.Modules;

[Generator(LanguageNames.CSharp)]
public sealed class BaseViewModel : IIncrementalGenerator
{
	public const string ClassName = "BaseViewModel";
	public static readonly string Namespace = ProjectInfo.Title;

	public static string BuildCode()
	{
		return $@"{AttributeStringBuild.GeneratedTitle}

namespace {Namespace};
using global::System;

{AttributeStringBuild.GeneratedCode
(
	$"{Namespace}.Generators.Modules.BaseViewModel", ProjectInfo.
		Version)}
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
