using System.Reflection;

namespace GeneratorUtils;

public static class ProjectInfo
{
	public static string Version = GetAssemblyVersion();
	public static string RootNamespace = GetAssemblyTitle();

	private static string GetAssemblyVersion()
	{
		var assembly = Assembly.GetExecutingAssembly();
		var attribute =
			assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
		return attribute?.Version
			?? throw new NullReferenceException("Version attribute is null");
	}

	private static string GetAssemblyTitle()
	{
		var assembly = Assembly.GetExecutingAssembly();
		var attribute = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
		return attribute?.Title
			?? throw new NullReferenceException("Title attribute is null");
	}
}
