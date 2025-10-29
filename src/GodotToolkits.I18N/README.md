# GodotToolkits.I18N

为项目中所有属性为`AdditionalFiles`的csv文件,生成对应的翻译文件

## 生成示例

在项目中创建一个csv文件,命名为`foo.csv`，并修改属性为`AdditionalFiles`,内容如下:

```csv
id,en,zh
hello,Hello,你好
world,World,世界
```

会生成2个类一个索引类一个内容类

### 索引类

```csharp
namespace GodotToolkits.I18NExtension;

#if GODOT
using global::Godot;
[GlobalClass]
public static class FooIndex : RefCounted
#else
public static class FooIndex
#endif
{
 public const string hello = "hello";
 public const string world = "world";
}
```

### 内容类

```csharp
namespace GodotToolkits.I18NExtension;
using global::System.Collections.Generic;

public static class Foo {

 public static List<Dictionary<string, string>> Data => [
  @hello,
  @world,
 ];

 public static Dictionary<string, string> @hello => new()
 {
  {"id", "hello"},
  {"en", "Hello"},
  {"zh", "你好"},
 };

 public static Dictionary<string, string> @world => new()
 {
  {"id", "world"},
  {"en", "World"},
  {"zh", "世界"},
 };

}
```

详细生成示例请参考[示例项目](https://github.com/NOKNOWNONO/GodotToolkits/tree/master/samples/GodotToolkits.I18N.Sample)
