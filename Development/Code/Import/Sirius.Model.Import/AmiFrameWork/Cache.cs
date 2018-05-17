using System;
using System.Collections.Generic;

namespace Ami.Framework.Collection
{
  public class Cache<TKey, TData>
  {
    private readonly Dictionary<TKey, TData> dictionary;
    private readonly Func<TKey, TData> generator;
    private readonly object locker;

    public TData this[TKey key]
    {
      get
      {
        lock (this.locker)
        {
          TData local_0;
          if (!this.dictionary.TryGetValue(key, out local_0))
          {
            local_0 = this.generator(key);
            this.dictionary.Add(key, local_0);
          }
          return local_0;
        }
      }
    }

    public Cache(Func<TKey, TData> cacheGenerator)
    {
      this.locker = new object();
      this.generator = cacheGenerator;
      this.dictionary = new Dictionary<TKey, TData>();
    }

    public Cache(Func<TKey, TData> cacheGenerator, IEqualityComparer<TKey> cacheKeyEqualityComparer)
    {
      this.locker = new object();
      this.generator = cacheGenerator;
      this.dictionary = new Dictionary<TKey, TData>(cacheKeyEqualityComparer);
    }

    public bool Contains(TKey key)
    {
      lock (this.locker)
        return this.dictionary.ContainsKey(key);
    }

    public void Clear()
    {
      lock (this.locker)
        this.dictionary.Clear();
    }

    public void Remove(TKey key)
    {
      lock (this.locker)
      {
        if (!this.dictionary.ContainsKey(key))
          return;
        this.dictionary.Remove(key);
      }
    }
  }
}
