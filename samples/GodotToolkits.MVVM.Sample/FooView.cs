using GodotToolkits.MVVM.Modules;

namespace GodotToolkits.MVVM.Sample;

[View]
public partial class FooView
{
	partial void InitializeBindings()
	{
		var dict = new ObservableDictionary<object, object>();
	}

	partial void InitializeComponents() { }
}
