// NonNullList.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace Fools.cs.Utilities
{
	[PublicAPI]
	public class NonNullList<T> : IList<T>
	{
		[NotNull] private readonly List<T> _impl = new List<T>();

		public void CopyTo([NotNull] Array array, int index)
		{
			((ICollection) _impl).CopyTo(array, index);
		}

		public object SyncRoot { get { return ((IList) _impl).SyncRoot; } }

		public bool IsSynchronized { get { return ((IList) _impl).IsSynchronized; } }

		public bool IsFixedSize { get { return ((IList) _impl).IsFixedSize; } }

		public void Add([NotNull] T item)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(item != null, "item != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			_impl.Add(item);
		}

		public void AddRange([NotNull] IEnumerable<T> collection)
		{
			var items = collection.ToList();
			foreach (var item in items)
			{
				// ReSharper disable CompareNonConstrainedGenericWithNull
				Debug.Assert(item != null, "item != null");
				// ReSharper restore CompareNonConstrainedGenericWithNull
			}
			_impl.AddRange(items);
		}

		[NotNull]
		public ReadOnlyCollection<T> AsReadOnly()
		{
			return _impl.AsReadOnly();
		}

		public int BinarySearch(int index, int count, [NotNull] T item, IComparer<T> comparer)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(item != null, "item != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			return _impl.BinarySearch(index, count, item, comparer);
		}

		public int BinarySearch([NotNull] T item)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(item != null, "item != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			return _impl.BinarySearch(item);
		}

		public int BinarySearch([NotNull] T item, IComparer<T> comparer)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(item != null, "item != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			return _impl.BinarySearch(item, comparer);
		}

		public void Clear()
		{
			_impl.Clear();
		}

		public bool Contains([NotNull] T item)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(item != null, "item != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			return _impl.Contains(item);
		}

		[NotNull]
		public List<TOutput> ConvertAll<TOutput>([NotNull] Converter<T, TOutput> converter)
		{
			return _impl.ConvertAll(converter);
		}

		public void CopyTo([NotNull] T[] array)
		{
			_impl.CopyTo(array);
		}

		public void CopyTo(int index, [NotNull] T[] array, int array_index, int count)
		{
			_impl.CopyTo(index, array, array_index, count);
		}

		public void CopyTo(T[] array, int array_index)
		{
			_impl.CopyTo(array, array_index);
		}

		public bool Exists([NotNull] Predicate<T> match)
		{
			return _impl.Exists(match);
		}

		public T Find([NotNull] Predicate<T> match)
		{
			return _impl.Find(match);
		}

		public List<T> FindAll([NotNull] Predicate<T> match)
		{
			return _impl.FindAll(match);
		}

		public int FindIndex([NotNull] Predicate<T> match)
		{
			return _impl.FindIndex(match);
		}

		public int FindIndex(int start_index, [NotNull] Predicate<T> match)
		{
			return _impl.FindIndex(start_index, match);
		}

		public int FindIndex(int start_index, int count, [NotNull] Predicate<T> match)
		{
			return _impl.FindIndex(start_index, count, match);
		}

		public T FindLast([NotNull] Predicate<T> match)
		{
			return _impl.FindLast(match);
		}

		public int FindLastIndex([NotNull] Predicate<T> match)
		{
			return _impl.FindLastIndex(match);
		}

		public int FindLastIndex(int start_index, [NotNull] Predicate<T> match)
		{
			return _impl.FindLastIndex(start_index, match);
		}

		public int FindLastIndex(int start_index, int count, [NotNull] Predicate<T> match)
		{
			return _impl.FindLastIndex(start_index, count, match);
		}

		public void ForEach([NotNull] Action<T> action)
		{
			_impl.ForEach(action);
		}

		public List<T> GetRange(int index, int count)
		{
			return _impl.GetRange(index, count);
		}

		public int IndexOf([NotNull] T item)
		{
			return _impl.IndexOf(item);
		}

		public int IndexOf([NotNull] T item, int index)
		{
			return _impl.IndexOf(item, index);
		}

		public int IndexOf([NotNull] T item, int index, int count)
		{
			return _impl.IndexOf(item, index, count);
		}

		public void Insert(int index, [NotNull] T item)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(item != null, "item != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			_impl.Insert(index, item);
		}

		public void InsertRange(int index, [NotNull] IEnumerable<T> collection)
		{
			var items = collection.ToList();
			foreach (var item in items)
			{
				// ReSharper disable CompareNonConstrainedGenericWithNull
				Debug.Assert(item != null, "item != null");
				// ReSharper restore CompareNonConstrainedGenericWithNull
			}
			_impl.InsertRange(index, items);
		}

		public int LastIndexOf([NotNull] T item)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(item != null, "item != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			return _impl.LastIndexOf(item);
		}

		public int LastIndexOf([NotNull] T item, int index)
		{
			return _impl.LastIndexOf(item, index);
		}

		public int LastIndexOf([NotNull] T item, int index, int count)
		{
			return _impl.LastIndexOf(item, index, count);
		}

		public bool Remove([NotNull] T item)
		{
			return _impl.Remove(item);
		}

		public int RemoveAll([NotNull] Predicate<T> match)
		{
			return _impl.RemoveAll(match);
		}

		public void RemoveAt(int index)
		{
			_impl.RemoveAt(index);
		}

		public void RemoveRange(int index, int count)
		{
			_impl.RemoveRange(index, count);
		}

		public void Reverse()
		{
			_impl.Reverse();
		}

		public void Reverse(int index, int count)
		{
			_impl.Reverse(index, count);
		}

		public void Sort()
		{
			_impl.Sort();
		}

		public void Sort([NotNull] IComparer<T> comparer)
		{
			_impl.Sort(comparer);
		}

		public void Sort(int index, int count, [NotNull] IComparer<T> comparer)
		{
			_impl.Sort(index, count, comparer);
		}

		public void Sort([NotNull] Comparison<T> comparison)
		{
			_impl.Sort(comparison);
		}

		[NotNull]
		public T[] ToArray()
		{
			return _impl.ToArray();
		}

		public void TrimExcess()
		{
			_impl.TrimExcess();
		}

		public bool TrueForAll([NotNull] Predicate<T> match)
		{
			return _impl.TrueForAll(match);
		}

		public int Capacity { get { return _impl.Capacity; } set { _impl.Capacity = value; } }

		public int Count { get { return _impl.Count; } }

		[NotNull]
		public T this[int index]
		{
			get
			{
				// ReSharper disable AssignNullToNotNullAttribute
				return _impl[index];
				// ReSharper restore AssignNullToNotNullAttribute
			}
			set
			{
				// ReSharper disable CompareNonConstrainedGenericWithNull
				Debug.Assert(value != null, "value != null");
				// ReSharper restore CompareNonConstrainedGenericWithNull
				_impl[index] = value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) _impl).GetEnumerator();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _impl.GetEnumerator();
		}

		public bool IsReadOnly { get { return ((IList) _impl).IsReadOnly; } }
	}

	public static class NonNullListExtensions
	{
		[NotNull]
		public static NonNullList<T> ToNonNullList<T>([NotNull] this IEnumerable<T> values)
		{
			var result = new NonNullList<T>();
			result.AddRange(values);
			return result;
		}
	}
}

// ReSharper restore InconsistentNaming
