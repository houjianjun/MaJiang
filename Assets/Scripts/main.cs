using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour
{
    //private GameObject majang;
    List<int> listMajiang;

    void Start()
    {
        //指定固定分辨率
        Screen.SetResolution(1024, 768, false);
        //生成牌
        GenerateMaJiang();

        GameObject btnChange = GameObject.Find("btnChange");
        Button btn0 = btnChange.GetComponent<Button>();
        btn0.onClick.AddListener(delegate ()
        {
            this.OnClick(btnChange);
        });

        GameObject btnQuit = GameObject.Find("btnQuit");
        Button btn1 = btnQuit.GetComponent<Button>();
        btn1.onClick.AddListener(delegate ()
        {
            this.OnClick(btnQuit);
        });

        GameObject btnSquat = GameObject.Find("btnSquat");
        Button btn2 = btnSquat.GetComponent<Button>();
        btn2.onClick.AddListener(delegate ()
        {
            this.OnClick(btnSquat);
        });

        GameObject btnFlat = GameObject.Find("btnFlat");
        Button btn3 = btnFlat.GetComponent<Button>();
        btn3.onClick.AddListener(delegate ()
        {
            this.OnClick(btnFlat);
        });

        GameObject btnJump = GameObject.Find("btnJump");
        Button btn4 = btnJump.GetComponent<Button>();
        btn4.onClick.AddListener(delegate ()
        {
            this.OnClick(btnJump);
        });
    }
    /// <summary>
    /// GUI方法
    /// </summary>

    void OnGUI()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// 生成随机麻将牌
    /// </summary>
    private void GenerateMaJiang()
    {
        //产生随数
        int[] random = new int[9];
        listMajiang = new List<int>();
        random = MaJiang.shuffle(random);
        listMajiang.AddRange(random);
        random = MaJiang.shuffle(random);
        listMajiang.AddRange(random);

        GameObject[] majiangs = MaJiangResInit();

        for (int i = 0; i < listMajiang.Count - 2; i++)
        {
            //实例化对象
            var mj = Instantiate(majiangs[listMajiang[i]]);
            mj.name = "majiang" + i;
            mj.SetActive(true);
            mj.GetComponent<Transform>().SetParent(GameObject.Find("Canvas/Plane").GetComponent<Transform>(), true);
        }
    }

    /// <summary>
    /// 麻将资源初始化
    /// </summary>
    private GameObject[] MaJiangResInit()
    {
        GameObject[] majiangs = new GameObject[10];
        for (int i = 0; i <= 9; i++)
        {
            //加载预设体资源,必须创建一个Resources的文件夹
            GameObject majiang = Resources.Load<GameObject>("Prefabs/majiang" + i);
            //保存资源
            majiangs[i] = majiang;
        }
        return majiangs;
    }

    /// <summary>
    /// 点击事件方法
    /// </summary>
    /// <param name="go"></param>
    private void onClickButtonHandler(GameObject go)
    {
        //Majang = go;
        //move = true;
        //Debug.Log(Majang.transform.position.y);
    }

    /// <summary>
    /// 蹲出
    /// </summary>
    private List<Hashtable> SquatOut()
    {
        List<Hashtable> listSquat = new List<Hashtable>();
        //第一行
        for (int i = 1; i <= 4; i++)
        {
            Hashtable ht = new Hashtable();
            ht.Add("First", listMajiang[(listMajiang.Count - 2) / 2 - i]);
            ht.Add("Second", listMajiang[(listMajiang.Count - 2) - i]);
            listSquat.Add(ht);
        }
        return listSquat;
    }
    /// <summary>
    /// 平出
    /// </summary>
    /// <returns></returns>
    private List<Hashtable> FlatOut()
    {
        List<Hashtable> list = new List<Hashtable>();

        for (int i = 16; i > 12; i = i - 2)
        {
            Hashtable ht = new Hashtable();
            ht.Add("First", listMajiang[i - 1]);
            ht.Add("Second", listMajiang[i - 2]);
            list.Add(ht);
        }

        for (int i = 8; i > 4; i = i - 2)
        {
            Hashtable ht1 = new Hashtable();
            ht1.Add("First", listMajiang[i - 1]);
            ht1.Add("Second", listMajiang[i - 2]);
            list.Add(ht1);
        }

        foreach (var item in list)
        {
            Debug.Log(Environment.NewLine + item["First"] + ":" + item["Second"]);
        }
        return list;
    }
    /// <summary>
    /// 跳出
    /// </summary>
    /// <returns></returns>
    private List<Hashtable> JumpOut()
    {
        List<Hashtable> list = new List<Hashtable>();

        for (int i = 16; i > 10; i = i - 4)
        {
            Hashtable ht = new Hashtable();
            ht.Add("First", listMajiang[i - 1]);
            ht.Add("Second", listMajiang[i - 3]);
            list.Add(ht);
        }

        for (int i = 8; i > 2; i=i-4)
        {
            Hashtable ht = new Hashtable();
            ht.Add("First", listMajiang[i - 1]);
            ht.Add("Second", listMajiang[i - 3]);
            list.Add(ht);
        }

      
        foreach (var item in list)
        {
            Debug.Log(Environment.NewLine + item["First"] + ":" + item["Second"]);
        }
        return list;
    }

    /// <summary>
    /// 结果加载
    /// </summary>
    /// <param name="list"></param>
    public void ShowSort(List<Hashtable> list)
    {
        //获取结果面板
        List<Hashtable> list2 = MaJiang.Comparison(list);
        //获取文本对象
        Text txtOriginal = GameObject.Find("txtOriginal").GetComponent<Text>();
        txtOriginal.text = "";
        StringBuilder sb = new StringBuilder();
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
                }
            }
        }

        foreach (var item in list2)
        {

            Debug.Log(Environment.NewLine + item["First"] + ":" + item["Second"] + "对子：" + item["Pairs"] + "点数：" + item["Point"] + "序号：" + item["Sorting"]);
        }

        txtOriginal.text = "原始大小：" + sb.ToString();
    }


    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="sender"></param>
    private void OnClick(GameObject sender)
    {
        string btnName = sender.name;

        switch (btnName)
        {
            case "btnQuit":
                //退出应用程
                Application.Quit();
                break;
            case "btnChange":
                //变换牌面
                GameObject panel = GameObject.Find("Canvas/Plane");
                //清除面上的所有子元素
                for (int i = 0; i < panel.transform.childCount; i++)
                {
                    Destroy(panel.transform.GetChild(i).gameObject);
                }
                //重新加载
                GenerateMaJiang();
                break;
            case "btnSquat":
                //蹲出
                ShowSort(SquatOut());
                break;
            case "btnFlat":
                //平出
                ShowSort(FlatOut());
                break;
            case "btnJump":
                //跳出
                ShowSort(JumpOut());
                break;
            default:
                break;
        }
    }
}