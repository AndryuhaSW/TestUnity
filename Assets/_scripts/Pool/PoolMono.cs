using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    private List<T> pool;

    private Func<bool, T> CreateObject;

    public PoolMono(Func<bool, T> CreateObject, int count)
    {
        this.CreateObject = CreateObject;

        this.CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this.pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            T obj = this.CreateObject(false);
            pool.Add(obj);
        }
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var mono in this.pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
            return element;

        return this.CreateObject(true);
    }
}
