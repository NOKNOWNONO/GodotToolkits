using GodotToolkits.I18N;
using GodotToolkits.I18N.Generator.Generators;
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

	[Fact]
	public void GetCsvData()
	{
		var result = CsvFileGenerator.GetCsvData(
			[
				"index,en,zh",
				"$hello,hello,你好",
				"$world,world,世界",
				"$do,Do,做",
			]
		);
		Assert.Equal(3, result.Count);
		Assert.Equal("$hello", result[0].Index);
		Assert.Equal("hello", result[0].Data["en"]);
		Assert.Equal("你好", result[0].Data["zh"]);
		Assert.Equal("$world", result[1].Index);
		Assert.Equal("world", result[1].Data["en"]);
		Assert.Equal("世界", result[1].Data["zh"]);
		Assert.Equal("$do", result[2].Index);
		Assert.Equal("Do", result[2].Data["en"]);
		Assert.Equal("做", result[2].Data["zh"]);
	}
}
