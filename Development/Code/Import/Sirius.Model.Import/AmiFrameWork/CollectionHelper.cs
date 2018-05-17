using System;
using System.Collections.Generic;
using System.Linq;

namespace Ami.Framework.Tool
{
  public static class CollectionHelper
  {
    public static bool EqualValues<T>(T[] array1, T[] array2, Func<T, T, bool> compare)
    {
      if (!CollectionHelper.AreBothNullOrNot((object) array1, (object) array2))
        return false;
      if (array1 == null)
        return true;
      int length1 = array1.Length;
      int length2 = array2.Length;
      if (length1 != length2)
        return false;
      for (int index = 0; index < length1; ++index)
      {
        if (!compare(array1[index], array2[index]))
          return false;
      }
      return true;
    }

    public static void Add<T>(ref T[] array, T toPush)
    {
      int length = array.Length;
      Array.Resize<T>(ref array, length + 1);
      array[length] = toPush;
    }

    public static void Add<T>(ref T[] array, T toAdd, int index, int allocSize)
    {
      if (index >= array.Length)
        Array.Resize<T>(ref array, array.Length + allocSize);
      array[index] = toAdd;
    }

    public static T FirstOrDefault<T>(this T[] array, Func<T, bool> predicate)
    {
      int length = array.Length;
      for (int index = 0; index < length; ++index)
      {
        T obj = array[index];
        if (predicate(obj))
          return obj;
      }
      return default (T);
    }

    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> iterateAction)
    {
      foreach (T obj in enumerable)
        iterateAction(obj);
    }

    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> iterateAction)
    {
      int num = 0;
      foreach (T obj in enumerable)
      {
        iterateAction(obj, num);
        ++num;
      }
    }

    public static void Set<T>(this IList<T> list, int maxIndex, T value)
    {
      for (int index = 0; index < maxIndex; ++index)
        list[index] = value;
    }

    public static bool AreBothNullOrNot(object item1, object item2)
    {
      if (item1 == null)
      {
        if (item2 != null)
          return false;
      }
      else if (item2 == null)
        return false;
      return true;
    }

    public static void InsertSorted<T>(this T[] array, T item, Func<T, bool> occupied, Func<T, bool> insertChoice)
    {
      for (int index1 = 0; index1 < array.Length; ++index1)
      {
        if (!occupied(array[index1]))
        {
          array[index1] = item;
          break;
        }
        if (insertChoice(array[index1]))
        {
          for (int index2 = array.Length - 1; index2 > index1; --index2)
            array[index2] = array[index2 - 1];
          array[index1] = item;
          break;
        }
      }
    }

    private static IEnumerable<TValue> CartesianElement<TValue>(IEnumerable<IEnumerable<TValue>> ensembles, int[] ensembleSizes, int combination)
    {
      int i = 0;
      int position = combination;
      foreach (IEnumerable<TValue> source in ensembles)
      {
        int remainder;
        position = Math.DivRem(position, ensembleSizes[i], out remainder);
        yield return Enumerable.ElementAt<TValue>(source, remainder);
        ++i;
      }
    }

    public static IEnumerable<IEnumerable<TValue>> CartesianProduct<TValue>(this IEnumerable<IEnumerable<TValue>> ensembles)
    {
      int nbEnsemble = Enumerable.Count<IEnumerable<TValue>>(ensembles);
      if (nbEnsemble != 0)
      {
        int[] ensembleSizes = new int[nbEnsemble];
        int nbCombination = 1;
        int i = 0;
        foreach (IEnumerable<TValue> source in ensembles)
        {
          int num = Enumerable.Count<TValue>(source);
          if (num == 0)
          {
            yield break;
          }
          else
          {
            nbCombination *= num;
            ensembleSizes[i++] = num;
          }
        }
        for (int c = 0; c < nbCombination; ++c)
          yield return CollectionHelper.CartesianElement<TValue>(ensembles, ensembleSizes, c);
      }
    }
  }
}
