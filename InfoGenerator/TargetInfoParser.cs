using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace InfoGenerator;

public abstract class TargetInfoParser
{
	public static string Parse(string rootPath, TargetInfo targetInfo)
	{
		return targetInfo.Type switch
		{
			"xml" => XmlParse(rootPath, targetInfo),
			_ => throw new System.NotImplementedException(
				$"TargetInfoParser for type {targetInfo.Type} not implemented."
			),
		};
	}

	public static string XmlParse(string rootPath, TargetInfo targetInfo)
	{
		var fullPath = Path.Combine(rootPath, targetInfo.FilePath);
		var text = FileReader.ReadFile(fullPath);
		var xml = XDocument.Parse(text);
		var result = xml.Element(targetInfo.Path[0]);
		if (targetInfo.Path.Count <= 1 || result == null)
		{
			return result?.ToString() ?? "";
		}

		foreach (var path in targetInfo.Path.Skip(1))
		{
			result = result.Element(path);
			if (result == null)
			{
				break;
			}
		}

		return result?.Value.ToString() ?? "";
	}
}
