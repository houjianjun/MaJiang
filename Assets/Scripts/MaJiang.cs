using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 麻将对象
/// </summary>
public class MaJiang : MonoBehaviour
{
    private static MaJiangComparer pc;
    private static MaJiang instance;
    public static MaJiang GetInstance()
    {
        return instance;
    }
    void Awake()
    {
        instance = this;
    }

    private int first;      //第一张牌
    private int second;     //第二张牌
    private int sorting;       //顺序
    private bool pairs;     //是否是对子
    private int point;      //单牌点数

    #region 字段属性
    public int First
    {
        get
        {
            return first;
        }

        set
        {
            first = value;
        }
    }

    public int Second
    {
        get
        {
            return second;
        }

        set
        {
            second = value;
        }
    }

    public int Sorting
    {
        get
        {
            return sorting;
        }

        set
        {
            sorting = value;
        }
    }

    public bool Pairs
    {
        get
        {
            return pairs;
        }

        set
        {
            pairs = value;
        }
    }

    public int Point
    {
        get
        {
            return point;
        }

        set
        {
            point = value;
        }
    }
    #endregion

    /// <summary>
    /// 洗牌，并随之机数组
    /// </summary>
    /// <param name="array">定义一个数组即可</param>
    /// <returns></returns>
    public static int[] shuffle(int[] array)
    {
        for (int i = 0; i < array.Length; i++)      //把牌按顺序牌好
            array[i] = i + 1;

        for (int i = 0; i < array.Length; i++)//洗牌
        {
            //从第2张开始，取顺序为1的牌，从1~9随机取一张牌放到第1张处
            //下一张取顺序为1的牌，从1到9随机取一张牌放到第1张处，以此类推
            int j = UnityEngine.Random.Range(1, 10);
            int temp = array[i];
            array[i] = array[j - 1];
            array[j - 1] = temp;
        }
        return array;
    }

    /// <summary>
    /// 比较
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static List<Hashtable> Comparison(List<Hashtable> maJiangList)
    {
        int sort = 1;
        List<Hashtable> listSingle = new List<Hashtable>();
        List<Hashtable> listPairs = new List<Hashtable>();
        foreach (var item in maJiangList)
        {
            Hashtable maJiang = new Hashtable();
            maJiang["First"] = item["First"];
            maJiang["Second"] = item["Second"];
            if (Convert.ToInt32(item["First"]) == Convert.ToInt32(item["Second"]))
            {
                maJiang.Add("Pairs", true);
                listPairs.Add(maJiang);
            }
            else
            {
                Hashtable newcard = SingleCompute(maJiang);
                listSingle.Add(newcard);
            }
        }
        if (pc == null) {
            pc = Instantiate(MaJiangComparer.GetInstance());
        }
       
        //对子排序
        listPairs.Sort(pc);
        listPairs.Reverse();
       
        //单牌排序
        listSingle.Sort(pc);
        listSingle.Reverse();

        List<Hashtable> resultList = new List<Hashtable>();
        foreach (var item in listPairs)
        {
            resultList.Add(item);
        }

        foreach (var item in listSingle)
        {
            resultList.Add(item);
        }
        //添加编号
        for (int i = 0; i < resultList.Count; i++)
        {
            if (Convert.ToBoolean(resultList[i]["Pairs"]) && i >= 1 && Convert.ToInt32(resultList[i]["First"]) != Convert.ToInt32(resultList[i - 1]["First"]))
            {
                sort++;
                resultList[i].Add("Sorting", sort);
            }
            else if (i >= 1 && Convert.ToInt32(resultList[i]["Point"]) != Convert.ToInt32(resultList[i - 1]["Point"]))
            {
                sort++;
                resultList[i].Add("Sorting", sort);
            }
            else {
                resultList[i].Add("Sorting", sort);
            }
        }
        //返回结果
        return listPairs.Concat(listSingle).ToList<Hashtable>();
    }

    /// <summary>
    /// 单牌计算
    /// </summary>
    /// <param name="card"></param>
    public static Hashtable SingleCompute(Hashtable maJiang)
    {
        int total = Convert.ToInt32(maJiang["First"]) + Convert.ToInt32(maJiang["Second"]);
        if (total / 10 > 0)
        {
            maJiang.Add("Point", total % 10);
        }
        else if (total % 10 == 0)
        {
            maJiang.Add("Point", 0);
        }
        else
        {
            maJiang["Point"] = total;
        }

        return maJiang;
    }

    /// <summary>
    /// 对子判断
    /// </summary>
    /// <returns></returns>
    public static bool IsPairs(Hashtable maJiang)
    {
        if (Convert.ToInt32(maJiang["First"]) == Convert.ToInt32(maJiang["Second"]))
        {
            return true;
        }
        return false;
    }
}
