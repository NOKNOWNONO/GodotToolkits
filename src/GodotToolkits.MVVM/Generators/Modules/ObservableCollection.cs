using Microsoft.CodeAnalysis;
using Utils;
using ProjectInfo = Utils.ProjectInfo;

namespace GodotToolkits.MVVM.Generators.Modules;

[Generator(LanguageNames.CSharp)]
public sealed class ObservableCollection : IIncrementalGenerator
{
	public static readonly string GeneratedTitle =
		AttributeStringBuild.GeneratedTitle;

	public static readonly string GeneratedCode =
		AttributeStringBuild.GeneratedCode
		(
			$"{ProjectInfo.Title}.Modules.{nameof(ObservableCollection)}",
			ProjectInfo.Version
		);


	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterPostInitializationOutput
		(f =>
			{
				f.AddSource
				(
					$"{nameof(ObservableCollection)}.g.cs",
					GenCode
						.Replace
						(
							$"T{nameof(ObservableCollection)}",
							nameof(ObservableCollection)
						)
						.Replace
						(
							"GodotToolkits.MVVM.Templates",
							"GodotToolkits.MVVM.Collections"
						)
						.Replace("//GeneratedCode", GeneratedCode)
						.Replace("//GeneratedTitle", GeneratedTitle)
				);
			}
		);
	}


	public const string GenCode =
		$@"//GeneratedTitle
using global::System;
using global::System.Collections;
using global::System.Collections.Generic;

namespace GodotToolkits.MVVM.Templates;

public sealed class TObservableCollection<T>
	: ICollection<T>,
		IEnumerable<T>,
		IEnumerable,
		IList<T>,
		ICollection,
		IList
{{
	private readonly List<T> _list = [];

	public Action? CollectionChanged;

	//GeneratedCode
	public IEnumerator<T> GetEnumerator()
	{{
		return _list.GetEnumerator();
	}}

	//GeneratedCode
	IEnumerator IEnumerable.GetEnumerator()
	{{
		return _list.GetEnumerator();
	}}

	//GeneratedCode
	public void Add(T item)
	{{
		_list.Add(item);
		CollectionChanged?.Invoke();
	}}

	//GeneratedCode
	public int Add(object? value)
	{{
		var r = ((IList)_list).Add(value);
		CollectionChanged?.Invoke();
		return r;
	}}

	//GeneratedCode
	void IList.Clear()
	{{
		_list.Clear();
		CollectionChanged?.Invoke();
	}}

	//GeneratedCode
	public bool Contains(object? value)
	{{
		return ((IList)_list).Contains(value);
	}}

	//GeneratedCode
	public int IndexOf(object value)
	{{
		return ((IList)_list).IndexOf(value);
	}}

	//GeneratedCode
	public void Insert(int index, object value)
	{{
		((IList)_list).Insert(index, value);
		CollectionChanged?.Invoke();
	}}

	//GeneratedCode
	public void Remove(object value)
	{{
		((IList)_list).Remove(value);
		CollectionChanged?.Invoke();
	}}

	//GeneratedCode
	void IList.RemoveAt(int index)
	{{
		_list.RemoveAt(index);
		CollectionChanged?.Invoke();
	}}

	public bool IsFixedSize => ((IList)_list).IsFixedSize;

	//GeneratedCode
	void ICollection<T>.Clear()
	{{
		_list.Clear();
		CollectionChanged?.Invoke();
	}}

	//GeneratedCode
	public bool Contains(T item)
	{{
		return _list.Contains(item);
	}}

	//GeneratedCode
	public void CopyTo(T[] array, int arrayIndex)
	{{
		_list.CopyTo(array, arrayIndex);
	}}

	//GeneratedCode
	public bool Remove(T item)
	{{
		var r = _list.Remove(item);
		CollectionChanged?.Invoke();
		return r;
	}}

	//GeneratedCode
	public void CopyTo(Array array, int index)
	{{
		((ICollection)_list).CopyTo(array, index);
	}}

	public int Count => _list.Count;

	public bool IsSynchronized => ((ICollection)_list).IsSynchronized;

	public object SyncRoot => ((ICollection)_list).SyncRoot;

	public bool IsReadOnly => false;

	object IList.this[int index]
	{{
		get => ((IList)_list)[index];
		set
		{{
			((IList)_list)[index] = value;
			CollectionChanged?.Invoke();
		}}
	}}

	//GeneratedCode
	public int IndexOf(T item)
	{{
		return _list.IndexOf(item);
	}}

	//GeneratedCode
	public void Insert(int index, T item)
	{{
		_list.Insert(index, item);
		CollectionChanged?.Invoke();
	}}

	//GeneratedCode
	void IList<T>.RemoveAt(int index)
	{{
		_list.RemoveAt(index);
		CollectionChanged?.Invoke();
	}}

	public T this[int index]
	{{
		get => _list[index];
		set
		{{
			_list[index] = value;
			CollectionChanged?.Invoke();
		}}
	}}

	//GeneratedCode
	public void AddRange(IEnumerable<T> collection)
	{{
		foreach (var item in collection)
			_list.Add(item);

		CollectionChanged?.Invoke();
	}}
}}
";
}