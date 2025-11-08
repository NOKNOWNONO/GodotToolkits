using System;
using System.Collections;
using System.Collections.Generic;

namespace GodotToolkits.MVVM.Modules;

public sealed class ObservableCollection<T>
	: ICollection<T>,
		IEnumerable<T>,
		IEnumerable,
		IList<T>,
		ICollection,
		IList
{
	private readonly List<T> _list = [];

	public Action? CollectionChanged;

	public IEnumerator<T> GetEnumerator()
	{
		return _list.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return _list.GetEnumerator();
	}

	public void Add(T item)
	{
		_list.Add(item);
		CollectionChanged?.Invoke();
	}

	public int Add(object? value)
	{
		var r = ((IList)_list).Add(value);
		CollectionChanged?.Invoke();
		return r;
	}

	void IList.Clear()
	{
		_list.Clear();
		CollectionChanged?.Invoke();
	}

	public bool Contains(object? value)
	{
		return ((IList)_list).Contains(value);
	}

	public int IndexOf(object value)
	{
		return ((IList)_list).IndexOf(value);
	}

	public void Insert(int index, object value)
	{
		((IList)_list).Insert(index, value);
		CollectionChanged?.Invoke();
	}

	public void Remove(object value)
	{
		((IList)_list).Remove(value);
		CollectionChanged?.Invoke();
	}

	void IList.RemoveAt(int index)
	{
		_list.RemoveAt(index);
		CollectionChanged?.Invoke();
	}

	public bool IsFixedSize => ((IList)_list).IsFixedSize;

	void ICollection<T>.Clear()
	{
		_list.Clear();
		CollectionChanged?.Invoke();
	}

	public bool Contains(T item)
	{
		return _list.Contains(item);
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		_list.CopyTo(array, arrayIndex);
	}

	public bool Remove(T item)
	{
		var r = _list.Remove(item);
		CollectionChanged?.Invoke();
		return r;
	}

	public void CopyTo(Array array, int index)
	{
		((ICollection)_list).CopyTo(array, index);
	}

	public int Count => _list.Count;

	public bool IsSynchronized => ((ICollection)_list).IsSynchronized;

	public object SyncRoot => ((ICollection)_list).SyncRoot;

	public bool IsReadOnly => false;

	object IList.this[int index]
	{
		get => ((IList)_list)[index];
		set
		{
			((IList)_list)[index] = value;
			CollectionChanged?.Invoke();
		}
	}

	public int IndexOf(T item)
	{
		return _list.IndexOf(item);
	}

	public void Insert(int index, T item)
	{
		_list.Insert(index, item);
		CollectionChanged?.Invoke();
	}

	void IList<T>.RemoveAt(int index)
	{
		_list.RemoveAt(index);
		CollectionChanged?.Invoke();
	}

	public T this[int index]
	{
		get => _list[index];
		set
		{
			_list[index] = value;
			CollectionChanged?.Invoke();
		}
	}

	public void AddRange(IEnumerable<T> collection)
	{
		foreach (var item in collection)
			_list.Add(item);

		CollectionChanged?.Invoke();
	}
}
