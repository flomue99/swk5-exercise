using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace HashDictionary.Impl;

/// <summary>
/// Collecton type storing key value pairs in an unsorted manner
/// </summary>
/// <typeparam name="K">Key type</typeparam>
/// <typeparam name="V">Value type</typeparam>
public class HashDictionary<K, V> : IDictionary<K, V>
{
    private const int INITIAL_HASHTABLE_SIZE = 8;
    //can also be used than i dont have to implement the ctor -> values have to be set then to the props
    private class Node(K key, V value, Node next = null)
    {
        //default values can be set here, if i have a ctor i can make new Node(...) instead of new Node{...}
        //public Node(K key, V value, Node next = null)
        //{
        //    this.Key = key;
        //    this.Value = value;
        //    this.Next = next;
        //}
        public K Key { get; init; } = key;
        public V Value { get; set; } = value;
        public Node Next { get; set; } = next;

    }
    #region helper methods
    //Math.Abs to get an positive value
    private int IndexFor(K key) => Math.Abs(key.GetHashCode()) % ht.Length;

    private static EqualityComparer<K> comparer = EqualityComparer<K>.Default;

    private bool TryAdd(K key, V value, out Node node)
    {
        node = FindNode(key);
        if (node is not null) return false; //key already exists

        int idx = IndexFor(key);
        node = ht[idx] = new Node(key, value, next: ht[idx]);
        Count++;

        return true;
    }

    private Node FindNode(K key)
    {
        Node node = ht[IndexFor(key)];
        for (; node is not null; node = node.Next)
        {
            //Here ne need equals because we have a Gerneric type
            //if(node.Key.Equals(key)) return node;
            //better version
            if (comparer.Equals(node.Key, key)) return node;
        }

        return null;
    }
    #endregion

    private Node[] ht = new Node[INITIAL_HASHTABLE_SIZE];

    public V this[K key]
    {
        get
        {
            Node n = FindNode(key);
            if (n is null) throw new KeyNotFoundException($"Key {key} does not exist");
            return n.Value;
        }

        set
        {
            if (!TryAdd(key, value, out Node n))
            {
                n.Value = value;
            };
        }
    }

    public ICollection<K> Keys
    {
        get
        {
            IList<K> keys = new List<K>(Count);
            foreach (var (k, _) in this)
            {
                keys.Add(k);
            }
            return keys;
        }
    }

    public ICollection<V> Values
    {
        get
        {
            IList<V> values = new List<V>(Count);
            foreach (var (_, v) in this)
            {
                values.Add(v);
            }
            return values;
        }
    }

    public int Count { get; private set; }

    public bool IsReadOnly => false;

    public void Add(K key, V value)
    {
        //_ because we dont need the node here
        if (!TryAdd(key, value, out _))
        {
            throw new ArgumentException($"Entry with key{key} already exsits.");
        }
    }

    public void Add(KeyValuePair<K, V> item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        ht = new Node[INITIAL_HASHTABLE_SIZE];
        Count = 0;
    }

    public bool Contains(KeyValuePair<K, V> item) => ContainsKey(item.Key);

    public bool ContainsKey(K key) => FindNode(key) is not null;

    public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
    {
        for (int i = 0; i < ht.Length; i++)
        {
            for (Node n = ht[i]; n is not null; n = n.Next)
            {
                yield return new KeyValuePair<K, V>(n.Key, n.Value);
            }
        }
    }

    public bool Remove(K key)
    {
        throw new NotImplementedException();
    }

    public bool Remove(KeyValuePair<K, V> item)
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue(K key, [MaybeNullWhen(false)] out V value)
    {
        Node n = FindNode(key);
        value = n is null ? default : n.Value;
        return n is not null;
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

}
