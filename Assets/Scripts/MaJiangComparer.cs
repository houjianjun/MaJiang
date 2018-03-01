using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

/// <summary>
/// 比较器
/// </summary>
public class MaJiangComparer : MonoBehaviour, IComparer<Hashtable>
{
    private static MaJiangComparer instance;
    public static MaJiangComparer GetInstance()
    {
        return instance;
    }
    void Awake()
    {
        instance = this;
    }
    public int Compare(Hashtable x, Hashtable y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        var diff = Convert.ToInt32(x["Point"]) - Convert.ToInt32( y["Point"]);
        if (diff > 0) return 1;
        if (diff < 0) return -1;
        return 0;
    }
}
