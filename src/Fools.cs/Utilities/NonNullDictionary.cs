// NonNullDictionary.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

// ReSharper disable InconsistentNaming
namespace Fools.cs.Utilities
{
	[PublicAPI]
	public sealed class NonNullDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		[NotNull] private readonly Dictionary<TKey, TValue> _impl = new Dictionary<TKey, TValue>();

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			((IDictionary<TKey, TValue>) _impl).CopyTo(array, index);
		}

		public void CopyTo([NotNull] Array array, int index)
		{
			((ICollection) _impl).CopyTo(array, index);
		}

		[NotNull]
		public object SyncRoot { get { return ((IDictionary) _impl).SyncRoot; } }

		public bool IsSynchronized { get { return ((IDictionary) _impl).IsSynchronized; } }

		public ICollection<TKey> Keys { get { return _impl.Keys; } }

		public ICollection<TValue> Values { get { return _impl.Values; } }

		public void Add([NotNull] TKey key, [NotNull] TValue value)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(key != null, "key != null");
			Debug.Assert(value != null, "value != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			_impl.Add(key, value);
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(item.Key != null, "key != null");
			Debug.Assert(item.Value != null, "value != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			_impl.Add(item.Key, item.Value);
		}

		public void Clear()
		{
			_impl.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(item.Key != null, "key != null");
			Debug.Assert(item.Value != null, "value != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			return ((IDictionary<TKey, TValue>) _impl).Contains(item);
		}

		public bool Remove([NotNull] TKey key)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(key != null, "key != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			return _impl.Remove(key);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(item.Key != null, "key != null");
			Debug.Assert(item.Value != null, "value != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			return ((IDictionary<TKey, TValue>) _impl).Remove(item);
		}

		public bool ContainsKey([NotNull] TKey key)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(key != null, "key != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			return _impl.ContainsKey(key);
		}

		public bool ContainsValue([NotNull] TValue value)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(value != null, "value != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			return _impl.ContainsValue(value);
		}

		public void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
		{
			_impl.GetObjectData(info, context);
		}

		public void OnDeserialization(object sender)
		{
			_impl.OnDeserialization(sender);
		}

		public bool TryGetValue([NotNull] TKey key, [NotNull] out TValue value)
		{
			// ReSharper disable CompareNonConstrainedGenericWithNull
			Debug.Assert(key != null, "key != null");
			// ReSharper restore CompareNonConstrainedGenericWithNull
			return _impl.TryGetValue(key, out value);
		}

		public IEqualityComparer<TKey> Comparer { get { return _impl.Comparer; } }

		public int Count { get { return _impl.Count; } }

		public bool IsReadOnly { get { return ((IDictionary) _impl).IsReadOnly; } }

		[NotNull]
		public TValue this[[NotNull] TKey key]
		{
			// ReSharper disable AssignNullToNotNullAttribute
			get { return _impl[key]; }
			// ReSharper restore AssignNullToNotNullAttribute
			set
			{
				// ReSharper disable CompareNonConstrainedGenericWithNull
				Debug.Assert(key != null, "key != null");
				// ReSharper restore CompareNonConstrainedGenericWithNull
				_impl[key] = value;
			}
		}

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return _impl.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _impl.GetEnumerator();
		}
	}
}
// ReSharper restore InconsistentNaming
