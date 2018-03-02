using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine.UI;
#if UNITY_EDITOR  
using UnityEditor;
#endif 

public class ShowContent
{
    /// <summary>
    /// 显示排序结果
    /// </summary>
    /// <param name="list"></param>
    public static void ShowSort(List<Hashtable> list, Text txtOriginal)
    {
        //获取结果面板
        List<Hashtable> list2 = MaJiang.Comparison(list);

        txtOriginal.text = "";
        StringBuilder sb = new StringBuilder();
        //最终结果
        for (int i = list.Count - 1; i >= 0; i--)
        {
            foreach (var item in list2)
            {
                if (Convert.ToInt32(list[i]["First"]) == Convert.ToInt32(item["First"]) && Convert.ToInt32(list[i]["Second"]) == Convert.ToInt32(item["Second"]))
                {
                    string point = "";
                    if (Convert.ToBoolean(item["Pairs"]))
                    {
                        point = "对子：" + item["First"];
                    }
                    else
                    {
                        point = item["Point"].ToString();
                    }
                    sb.Append(item["Sorting"] + "(" + point + ")     ");
                    //将排序值存入HashList
                }
            }
        }

        foreach (var item in list2)
        {
            Debug.Log(Environment.NewLine + item["First"] + ":" + item["Second"] + "对子：" + item["Pairs"] + "点数：" + item["Point"] + "序号：" + item["Sorting"]);
        }

        showOriginal(sb.ToString(), txtOriginal);
    }
    /// <summary>
    /// 显示原始内容
    /// </summary>
    /// <param name="str"></param>
    /// <param name="txtOriginal"></param>
    public static void showOriginal(string str, Text txtOriginal)
    {
        txtOriginal.text = "原始大小：" + str;
    }
    /// <summary>
    /// 改变选中牌的颜色
    /// </summary>
    /// <param name="index"></param>
    public static void changeColor(int index, Color color, GameObject plane)
    {
        Image child = plane.transform.GetChild(index).GetComponent<Image>();
        child.color = color;
    }
    /// <summary>
    /// 改变面板上的内容
    /// </summary>
    /// <param name="index"></param>
    /// <param name="plane"></param>
    public static void ChangValue(int index, int value, GameObject plane)
    {
        Image child = plane.transform.GetChild(index).GetComponent<Image>();
        //改变资源
        //  child.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/images/pin" + value + ".png");
        child.overrideSprite = Resources.Load("images/pin"+value, typeof(Sprite)) as Sprite;
    }

}
