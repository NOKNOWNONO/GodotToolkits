# GodotToolkits.MVVM

## 组件

- `BaseViewModel` - 基础 ViewModel 类 所有ViewModel都应该继承此类
- `View` - 视图特性

```csharp
[View]
public class MyView : Control
```

- `ObservableProperty` - 可观察属性

```csharp
public class MyViewModel : BaseViewModel
{
 [ObservableProperty]
 private int _name;
}
- `ObservableCollection` - 可观察集合
```csharp
var myList = new ObservableCollection<int>();
myList.CollectionChanged +=
 () => { myList.ForEach(i => Console.WriteLine(i));};
myList.Add(1);
myList.Add(2);
```

第一次添加打印

```csharp
1
```

第二次添加打印

```csharp
1
2
```

- `ObservableDictionary` - 可观察字典

```csharp
var myDict = new ObservableDictionary<string, int>();
myDict.DictionaryChanged +=
 () => { foreach (var tuple in myDict) Console.WriteLine(tuple.Key + " : " + tuple.Value)};
myDict["key1"] = 1;
myDict["key2"] = 2;
```

第一次添加打印

```csharp
key1 : 1

```

第二次添加打印

```csharp
key1 : 1
key2 : 2
```

生成示例请参考[示例项目](https://github.com/NOKNOWNONO/GodotToolkits/tree/master/samples/GodotToolkits.MVVM.Sample)
