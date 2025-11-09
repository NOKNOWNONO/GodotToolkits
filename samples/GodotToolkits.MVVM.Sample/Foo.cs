using GodotToolkits.MVVM.Modules;

namespace GodotToolkits.MVVM.Sample;

public partial class Foo
{
	[ObservableProperty]
	private string _name = "";

	[ObservableProperty]
	private A _a = new();
}

public class A { }
