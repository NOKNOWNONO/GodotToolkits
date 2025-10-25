using GodotToolkits.I18N.Generators;
using Xunit;

namespace GodotToolkits.I18N.Tests.Generators;

public class CsvFileGeneratorTest
{
	[Fact]
	public void GetIndexes()
	{
		var result = CsvFileGenerator.GetIndexes(
			["hello,hello,你好", "world,world,世界"]
		);
		Assert.Equal(2, result.Count);
		Assert.Equal("hello", result[0]);
		Assert.Equal("world", result[1]);
	}
}
