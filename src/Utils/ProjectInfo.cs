using System;
using System.Reflection;

namespace Utils;

public static class ProjectInfo
{
	public static string Version => GetAssemblyVersion();
	public static string Title   => GetAssemblyTitle();


	private static string GetAssemblyVersion()
	{
		var attribute = Assembly
			.GetExecutingAssembly()
			.GetCustomAttribute<AssemblyFileVersionAttribute>();
		return attribute?.Version
			?? throw new NullReferenceException("Version attribute is null");
	}


	public static string GetAssemblyTitle()
	{
		var attribute = Assembly
			.GetExecutingAssembly()
			.GetCustomAttribute<AssemblyTitleAttribute>();
		return attribute?.Title
			?? throw new NullReferenceException("Title attribute is null");
	}
}