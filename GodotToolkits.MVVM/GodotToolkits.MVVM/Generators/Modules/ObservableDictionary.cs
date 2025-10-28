using Microsoft.CodeAnalysis;
using Utils;
using ProjectInfo = Utils.ProjectInfo;

namespace GodotToolkits.MVVM.Generators.Modules;

[Generator(LanguageNames.CSharp)]
public sealed class ObservableDictionary : IIncrementalGenerator
{
	public static readonly string GeneratedTitle =
		AttributeStringBuild.GeneratedTitle;

	public static readonly string GeneratedCode =
		AttributeStringBuild.GeneratedCode(
			$"{ProjectInfo.Title}.Modules.{nameof(ObservableDictionary)}",
			ProjectInfo.Version
		);

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterPostInitializationOutput(f =>
		{
			f.AddSource(
				$"{nameof(ObservableDictionary)}.g.cs",
				GenCode
					.Replace(
						$"T{nameof(ObservableDictionary)}",
						nameof(ObservableDictionary)
					)
					.Replace(
						"GodotToolkits.MVVM.Templates;",
						"GodotToolkits.MVVM.Collections"
					)
					.Replace("//GeneratedCode", GeneratedCode)
					.Replace("//GeneratedTitle", GeneratedTitle)
			);
		});
	}

	public const string GenCode =
		$@"//GeneratedTitle
using global::System;
using global::System.Collections;
using global::System.Collections.Generic;

namespace GodotToolkits.MVVM.Templates;

public class TObservableDictionary<TKey, TValue>
	: ICollection<KeyValuePair<TKey, TValue>>,
		IEnumerable<KeyValuePair<TKey, TValue>>,
		IEnumerable,
		IDictionary<TKey, TValue>,
		ICollection,
		IDictionary
{{
	private readonly Dictionary<TKey, TValue> _dictionary = [];
	public Action? DictionaryChanged;

	//GeneratedCode
	public bool Contains(object key)
	{{
		return ((IDictionary)_dictionary).Contains(key);
	}}

	//GeneratedCode
	IDictionaryEnumerator IDictionary.GetEnumerator()
	{{
		return ((IDictionary)_dictionary).GetEnumerator();
	}}

	//GeneratedCode
	public void Remove(object key)
	{{
		((IDictionary)_dictionary).Remove(key);
	}}

	public bool IsFixedSize => ((IDictionary)_dictionary).IsFixedSize;

	//GeneratedCode
	IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<
		KeyValuePair<TKey, TValue>
	>.GetEnumerator()
	{{
		return _dictionary.GetEnumerator();
	}}

	//GeneratedCode
	IEnumerator IEnumerable.GetEnumerator()
	{{
		return ((IEnumerable)_dictionary).GetEnumerator();
	}}

	//GeneratedCode
	public void Add(KeyValuePair<TKey, TValue> item)
	{{
		((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Add(item);
		DictionaryChanged?.Invoke();
	}}

	//GeneratedCode
	public void Add(object key, object value)
	{{
		((IDictionary)_dictionary).Add(key, value);
	}}

	//GeneratedCode
	void IDictionary.Clear()
	{{
		_dictionary.Clear();
	}}

	//GeneratedCode
	void ICollection<KeyValuePair<TKey, TValue>>.Clear()
	{{
		_dictionary.Clear();
	}}

	//GeneratedCode
	public bool Contains(KeyValuePair<TKey, TValue> item)
	{{
		return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Contains(
			item
		);
	}}

	//GeneratedCode
	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
	{{
		((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).CopyTo(
			array,
			arrayIndex
		);
	}}

	//GeneratedCode
	public bool Remove(KeyValuePair<TKey, TValue> item)
	{{
		var result = (
			(ICollection<KeyValuePair<TKey, TValue>>)_dictionary
		).Remove(item);
		DictionaryChanged?.Invoke();
		return result;
	}}

	//GeneratedCode
	public void CopyTo(Array array, int index)
	{{
		((ICollection)_dictionary).CopyTo(array, index);
	}}

	public int Count => _dictionary.Count;

	public bool IsSynchronized => ((ICollection)_dictionary).IsSynchronized;

	public object SyncRoot => ((ICollection)_dictionary).SyncRoot;

	public bool IsReadOnly => false;

	public object this[object key]
	{{
		get => ((IDictionary)_dictionary)[key];
		set
		{{
			((IDictionary)_dictionary)[key] = value;
			DictionaryChanged?.Invoke();
		}}
	}}

	//GeneratedCode
	public void Add(TKey key, TValue value)
	{{
		_dictionary.Add(key, value);
		DictionaryChanged?.Invoke();
	}}

	//GeneratedCode
	public bool ContainsKey(TKey key)
	{{
		return _dictionary.ContainsKey(key);
	}}

	//GeneratedCode
	public bool Remove(TKey key)
	{{
		var result = _dictionary.Remove(key);
		DictionaryChanged?.Invoke();
		return result;
	}}

	//GeneratedCode
	public bool TryGetValue(TKey key, out TValue value)
	{{
		return _dictionary.TryGetValue(key, out value);
	}}

	public TValue this[TKey key]
	{{
		get => _dictionary[key];
		set
		{{
			_dictionary[key] = value;
			DictionaryChanged?.Invoke();
		}}
	}}

	ICollection<TKey> IDictionary<TKey, TValue>.Keys => _dictionary.Keys;

	ICollection IDictionary.Values => ((IDictionary)_dictionary).Values;

	ICollection IDictionary.Keys => ((IDictionary)_dictionary).Keys;

	ICollection<TValue> IDictionary<TKey, TValue>.Values => _dictionary.Values;
}}
";
}
