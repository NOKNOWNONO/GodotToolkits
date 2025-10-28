using GodotToolkits.MVVM.Generators;
using Xunit;

namespace GodotToolkits.MVVM.Tests.Generators;

public class ObservablePropertyGeneratorTest
{
	[Theory]
	[InlineData("_s", "S")]
	[InlineData("___s", "S")]
	public void GetPropertyName(string input, string expected)
	{
		var result = ObservablePropertyGenerator.GetPropertyName(input);
		Assert.Equal(result, expected);
	}


	[Fact]
	public void GetOnPropertyChanged()
	{
		var result = ObservablePropertyGenerator.GetOnPropertyChanged
		(
			"Test",
			"string"
		);
		Assert.Equal("partial void OnTestChanged(string newValue);", result);
	}
}