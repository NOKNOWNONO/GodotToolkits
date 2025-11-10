using System.IO;

namespace InfoGenerator;

public abstract class FileReader
{
	public static string ReadFile(string path)
	{
		using StreamReader reader = new(path);
		return reader.ReadToEnd();
	}
}
