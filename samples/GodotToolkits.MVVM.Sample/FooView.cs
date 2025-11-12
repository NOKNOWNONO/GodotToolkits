using GodotToolkits.MVVM.Modules;

namespace GodotToolkits.MVVM.Sample;

[View]
public partial class FooView
{
	partial void InitializeBindings()
	{
		var dict = new ObservableDictionary<object, object>();
		var coll = new ObservableCollection<object>();
	}

	partial void InitializeComponents() { }
}
