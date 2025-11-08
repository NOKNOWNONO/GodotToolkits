using System;
using System.Collections;
using System.Collections.Generic;

namespace GodotToolkits.MVVM.Modules;

public class ObservableDictionary<TKey, TValue>
	: IDictionary<TKey, TValue>,
		IDictionary
{
	private readonly Dictionary<TKey, TValue> _dictionary = [];
	public Action? DictionaryChanged;

	public bool Contains(object key)
	{
		return ((IDictionary)_dictionary).Contains(key);
	}

	IDictionaryEnumerator IDictionary.GetEnumerator()
	{
		return ((IDictionary)_dictionary).GetEnumerator();
	}

	public void Remove(object key)
	{
		((IDictionary)_dictionary).Remove(key);
	}

	public bool IsFixedSize => ((IDictionary)_dictionary).IsFixedSize;

	IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<
		KeyValuePair<TKey, TValue>
	>.GetEnumerator()
	{
		return _dictionary.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)_dictionary).GetEnumerator();
	}

	public void Add(KeyValuePair<TKey, TValue> item)
	{
		((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Add(item);
		DictionaryChanged?.Invoke();
	}

	public void Add(object key, object value)
	{
		((IDictionary)_dictionary).Add(key, value);
	}

	void IDictionary.Clear()
	{
		_dictionary.Clear();
	}

	void ICollection<KeyValuePair<TKey, TValue>>.Clear()
	{
		_dictionary.Clear();
	}

	public bool Contains(KeyValuePair<TKey, TValue> item)
	{
		return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Contains(
			item
		);
	}

	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
	{
		((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).CopyTo(
			array,
			arrayIndex
		);
	}

	public bool Remove(KeyValuePair<TKey, TValue> item)
	{
		var result = (
			(ICollection<KeyValuePair<TKey, TValue>>)_dictionary
		).Remove(item);
		DictionaryChanged?.Invoke();
		return result;
	}

	public void CopyTo(Array array, int index)
	{
		((ICollection)_dictionary).CopyTo(array, index);
	}

	public int Count => _dictionary.Count;

	public bool IsSynchronized => ((ICollection)_dictionary).IsSynchronized;

	public object SyncRoot => ((ICollection)_dictionary).SyncRoot;

	public bool IsReadOnly => false;

	public object this[object key]
	{
		get => ((IDictionary)_dictionary)[key];
		set
		{
			((IDictionary)_dictionary)[key] = value;
			DictionaryChanged?.Invoke();
		}
	}

	public void Add(TKey key, TValue value)
	{
		_dictionary.Add(key, value);
		DictionaryChanged?.Invoke();
	}

	public bool ContainsKey(TKey key)
	{
		return _dictionary.ContainsKey(key);
	}

	public bool Remove(TKey key)
	{
		var result = _dictionary.Remove(key);
		DictionaryChanged?.Invoke();
		return result;
	}

	public bool TryGetValue(TKey key, out TValue value)
	{
		return _dictionary.TryGetValue(key, out value);
	}

	public TValue this[TKey key]
	{
		get => _dictionary[key];
		set
		{
			_dictionary[key] = value;
			DictionaryChanged?.Invoke();
		}
	}

	ICollection<TKey> IDictionary<TKey, TValue>.Keys => _dictionary.Keys;

	ICollection IDictionary.Values => ((IDictionary)_dictionary).Values;

	ICollection IDictionary.Keys => ((IDictionary)_dictionary).Keys;

	ICollection<TValue> IDictionary<TKey, TValue>.Values => _dictionary.Values;
}
