using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour
{
    private GameObject majiangPlane;    //麻将面板
    public static List<int> listMajiangAuto;        //自动麻将列表
    public static List<int> listMajiangManual = new List<int>();  //手动
    private GameObject movePlane;   //移动面板
    private GameObject[] majiangs;  //麻将资源
    public static Text txtResult;          //结果文本
    private static Text txtOriginal;               //获取文本对象

    public static Button btn0;       //换牌按钮
    public static Button btn7;       //重置

    private StringBuilder sbData = new StringBuilder(); //结果数据

    private Text txtMoveCount;
    private int moveStep;

    private Toggle chkAuto;
    private Toggle chkInput;

    //输入记数
    public static int inputNum = 15;

    //出门、末门、庄家
    private Dictionary<string, string[]> dicResult = new Dictionary<string, string[]>();

    void Start()
    {
        //解决键盘被输入法劫持问题
        Win32Help.SetImeEnable(false);
        //初始化所求值
        string[] outDoor = { "1234", "2341", "4123" };
        string[] endDoor = { "4321", "3214", "1423" };
        string[] banker = { "1224", "2124", "1122" };

        dicResult.Add("outDoor", outDoor);
        dicResult.Add("endDoor", endDoor);
        dicResult.Add("banker", banker);


        //资源初始化
        MaJiangResInit();

        //指定固定分辨率
        Screen.SetResolution(1024, 768, false);
        //移动面板
        movePlane = GameObject.Find("Canvas/MovePanel");
        txtMoveCount = GameObject.Find("Canvas/txtMoveCount").GetComponent<Text>();
        //结果
        txtResult = GameObject.Find("Canvas/txtResult").GetComponent<Text>();
        txtOriginal = GameObject.Find("txtOriginal").GetComponent<Text>();
        chkAuto = GameObject.Find("Canvas/chkAuto").GetComponent<Toggle>();
        chkAuto.isOn = false;

        GameObject btnChange = GameObject.Find("btnChange");
        btn0 = btnChange.GetComponent<Button>();
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
        //左移一位
        GameObject btnLeftMove = GameObject.Find("btnLeftMove");
        Button btn5 = btnLeftMove.GetComponent<Button>();
        btn5.onClick.AddListener(delegate ()
        {
            this.OnClick(btnLeftMove);
        });
        //右移一位
        GameObject btnRightMove = GameObject.Find("btnRightMove");
        Button btn6 = btnRightMove.GetComponent<Button>();
        btn6.onClick.AddListener(delegate ()
        {
            this.OnClick(btnRightMove);
        });
        //重置
        GameObject btnReset = GameObject.Find("btnReset");
        btn7 = btnReset.GetComponent<Button>();
        btn7.onClick.AddListener(delegate ()
        {
            this.OnClick(btnReset);
        });

        chkInput = GameObject.Find("chkInput").GetComponent<Toggle>();
        chkInput.onValueChanged.AddListener((bool value) => EventHandler.OnToggleClick(chkInput, value));
        chkInput.isOn = true;

        majiangPlane = GameObject.Find("Canvas/Plane");
        //生成牌
        GenerateMaJiang();
        if (chkInput.isOn)
        {
            //禁用切牌按钮
            btn0.interactable = false;
            listMajiangAuto = listMajiangManual;
        }
        InitLoadMaJing(movePlane);
    }

    /// <summary>
    /// GUI方法
    /// </summary>
    void OnGUI()
    {

    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Debug.Log("您按下了W键");
        //    txtResult.text = "W";
        //}
        //解决键盘被输入法劫持问题
        Win32Help.SetImeEnable(false);
        EventHandler.getKeyDownCode(majiangPlane);
    }
    /// <summary>
    /// 生成随机麻将牌
    /// </summary>
    private void GenerateMaJiang()
    {
        for (int i = 0; i < 16; i++)
        {
            listMajiangManual.Add(0);
        }

        //产生随数
        int[] random = new int[9];
        listMajiangAuto = new List<int>();
        random = MaJiang.shuffle(random);
        listMajiangAuto.AddRange(random);
        random = MaJiang.shuffle(random);
        listMajiangAuto.AddRange(random);
        InitLoadMaJing(majiangPlane);
    }
    /// <summary>
    /// 初始加载
    /// </summary>
    /// <param name="plane"></param>
    private void InitLoadMaJing(GameObject plane)
    {
        //移除所有
        for (int i = 0; i < plane.transform.childCount; i++)
        {
            Destroy(plane.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < (listMajiangAuto.Count > 16 ? listMajiangAuto.Count - 2 : listMajiangAuto.Count); i++)
        {
            GameObject mj = null;
            if (chkInput.isOn)
            {
                mj = Instantiate(majiangs[0]);
            }
            else
            {
                mj = Instantiate(majiangs[listMajiangAuto[i]]);
            }
            //实例化对象
            mj.name = "majiang" + i;
            mj.SetActive(true);
            mj.GetComponent<Transform>().SetParent(plane.GetComponent<Transform>(), true);
        }
    }
    /// <summary>
    /// 加载麻将
    /// </summary>
    /// <param name="plane"></param>
    private void LoadMaJing(GameObject plane)
    {
        //移除所有
        for (int i = 0; i < plane.transform.childCount; i++)
        {
            Destroy(plane.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < (listMajiangAuto.Count > 16 ? listMajiangAuto.Count - 2 : listMajiangAuto.Count); i++)
        {
            GameObject mj = null;
            mj = Instantiate(majiangs[listMajiangAuto[i]]);
            //实例化对象
            mj.name = "majiang" + i;
            mj.SetActive(true);
            mj.GetComponent<Transform>().SetParent(plane.GetComponent<Transform>(), true);
        }
    }
    /// <summary>
    /// 麻将资源初始化
    /// </summary>
    private GameObject[] MaJiangResInit()
    {
        majiangs = new GameObject[10];
        for (int i = 0; i <= 9; i++)
        {
            //加载预设体资源,必须创建一个Resources的文件夹
            GameObject majiang = Resources.Load<GameObject>("Prefabs/majiang" + i);
            majiang.name = "majiangRes" + i;
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
    private List<Hashtable> SquatOut(GameObject plane)
    {
        List<Hashtable> listSquat = new List<Hashtable>();
        int count = 0;
        if (chkInput.isOn)
        {
            count = listMajiangAuto.Count;
        }
        else
        {
            count = listMajiangAuto.Count - 2;
        }

        Color color = Color.yellow;
        //第一行
        for (int i = 1; i <= 4; i++)
        {
            if (i % 2 == 0)
            {
                color = Color.green;
            }
            else
            {
                color = Color.yellow;
            }
            Hashtable ht = new Hashtable();
            ht.Add("First", listMajiangAuto[(count) / 2 - i]);
            ht.Add("Second", listMajiangAuto[(count) - i]);

            ShowContent.changeColor((count) / 2 - i, color, plane);
            ShowContent.changeColor((count) - i, color, plane);
            listSquat.Add(ht);
        }

        return listSquat;
    }
    /// <summary>
    /// 蹲出计算
    /// </summary>
    /// <typeparam name="Hashtable"></typeparam>
    /// <param name=""></param>
    private void SquatOutComputer()
    {
        if (chkAuto.isOn)
        {
            ///////////////////////////////////////左移
            string showValue = MoveComputer("SquatOut", 1);

            //////////////////////////////右移
            showValue += MoveComputer("SquatOut", 0);
            showComputerResult(showValue, "蹲出");
        }
    }
    /// <summary>
    /// 平出计算
    /// </summary>
    private void FlatOutComputer()
    {
        if (chkAuto.isOn)
        {
            ///////////////////////////////////////左移
            string showValue = MoveComputer("FlatOut", 1);

            //////////////////////////////右移
            showValue += MoveComputer("FlatOut", 0);
            showComputerResult(showValue, "平出");
        }
    }
    /// <summary>
    /// 跳出计算
    /// </summary>
    private void JumpOutComputer()
    {
        if (chkAuto.isOn)
        {
            ///////////////////////////////////////左移
            sbData.Append(MoveComputer("JumpOut", 1));
            //////////////////////////////右移
            sbData.Append(MoveComputer("JumpOut", 0));
            showComputerResult(sbData.ToString(), "跳出");
        }
    }
    /// <summary>
    /// 显示计算结果
    /// </summary>
    /// <param name="showValue"></param>
    private void showComputerResult(string showValue, string type)
    {
        txtResult.text = "运算结果：" + type + Environment.NewLine + showValue;
    }

    /// <summary>
    /// 左移算法
    /// </summary>
    private string MoveComputer(string type, int dir)
    {
        List<Hashtable> oldList = null;
        List<Hashtable> result;
        List<List<Hashtable>> finalList = new List<List<Hashtable>>();
        for (int num = 0; num < 1; num++)
        {
            switch (dir)
            {
                case 0:
                    //右移
                    LoopComputer.ClockwiseeMove(listMajiangAuto);
                    break;
                case 1:
                    //左移
                    LoopComputer.InverseMove(listMajiangAuto);
                    break;
                default:
                    break;
            }

            switch (type)
            {
                case "SquatOut":
                    //蹲出
                    oldList = SquatOut(majiangPlane);
                    break;
                case "FlatOut":
                    //蹲出
                    oldList = FlatOut(majiangPlane);
                    break;
                case "JumpOut":
                    //蹲出
                    oldList = JumpOut(majiangPlane);
                    break;
                default:
                    break;
            }
            result = MaJiang.Comparison(oldList);
#if UNITY_EDITOR
            for (int i = 0; i < listMajiangAuto.Count - 2; i++)
            {
                //显示指定的牌
                ShowContent.ChangValue(i, listMajiangAuto[i], majiangPlane);
            }
#endif
            // ShowSort(oldList);
            List<Hashtable> resultList = AutoComputer(oldList, result, num, 1);
            finalList.Add(resultList);
        }
        Debug.Log(finalList.Count);
        string showValue = "";
        int count = 0;
        foreach (var item in finalList)
        {
            string valueSort = "";
            foreach (var table in item)
            {
                valueSort += table["Sorting"];
            }
            string[] outDoor = dicResult["outDoor"];
            foreach (var itemOutDoor in outDoor)
            {
                if (valueSort == itemOutDoor)
                {
                    Debug.Log("出门" + valueSort);
                    count++;
                    showValue += MoveNum(valueSort + "->", count, "出门", dir);
                }
            }
            string[] endDoor = dicResult["endDoor"];
            foreach (var itemEndDoor in endDoor)
            {
                if (valueSort == itemEndDoor)
                {
                    Debug.Log("末门" + valueSort);
                    count++;
                    showValue += MoveNum(valueSort + "->", count, "末门", dir);
                }
            }
            string[] banker = dicResult["banker"];
            foreach (var itemBanker in banker)
            {
                if (valueSort == itemBanker)
                {
                    Debug.Log("庄家" + valueSort);
                    count++;
                    showValue += MoveNum(valueSort + "->", count, "庄家", dir);
                }
            }
            Debug.Log(valueSort);
        }
        return showValue;
    }

    /// <summary>
    /// 移动次数
    /// </summary>
    /// <param name="showValue"></param>
    /// <param name="count"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private static string MoveNum(string showValue, int count, string type, int dir)
    {
        string values = "";
        if (count < 3)
        {
            values = type + "->" + showValue + (dir == 1 ? ":左移" : ":右移") + count + Environment.NewLine;
        }
        else
        {
            //每次3张
            int num = count / 3;
            values = type + "->" + showValue + (dir == 1 ? ":左移3张" : ":右移3张") + num + "次" + (count % 3 > 0 ? "->再移" + count % 3 + "张一次" : "") + Environment.NewLine;
        }
        return values;
    }
    /// <summary>
    /// 自动计算
    /// </summary>
    /// <param name="oldList"></param>
    /// <param name="result"></param>
    /// <param name="count"></param>
    /// <param name="dir"></param>
    /// <returns></returns>
    private List<Hashtable> AutoComputer(List<Hashtable> oldList, List<Hashtable> result, int count, int dir)
    {
        List<Hashtable> list = new List<Hashtable>();
        for (int i = oldList.Count - 1; i >= 0; i--)
        {
            foreach (var item in result)
            {
                if (Convert.ToInt32(oldList[i]["First"]) == Convert.ToInt32(item["First"]) && Convert.ToInt32(oldList[i]["Second"]) == Convert.ToInt32(item["Second"]))
                {
                    Hashtable ht = new Hashtable();
                    string point = "";
                    //点数
                    if (Convert.ToBoolean(item["Pairs"]))
                    {
                        ht.Add("Point", 0);
                        ht.Add("Pairs", 1);
                        point = "对子：" + item["First"];
                    }
                    else
                    {
                        ht.Add("Point", point);
                        ht.Add("Pairs", 0);
                        point = item["Point"].ToString();
                    }
                    //排序
                    ht.Add("Sorting", item["Sorting"]);
                    //类型
                    ht.Add("type", count);
                    //方向
                    ht.Add("dir", dir);
                    //添加到列表
                    list.Add(ht);
                }
            }
        }
        return list;
    }

    /// <summary>
    /// 平出
    /// </summary>
    /// <returns></returns>
    private List<Hashtable> FlatOut(GameObject plane)
    {
        List<Hashtable> list = new List<Hashtable>();
        Color color = Color.yellow;

        for (int i = 16; i > 12; i = i - 2)
        {
            if (i < 16)
            {
                color = Color.magenta;
            }
            Hashtable ht = new Hashtable();
            ht.Add("First", listMajiangAuto[i - 1]);
            ht.Add("Second", listMajiangAuto[i - 2]);

            ShowContent.changeColor(i - 1, color, plane);
            ShowContent.changeColor(i - 2, color, plane);
            list.Add(ht);
        }
        color = Color.magenta;
        for (int i = 8; i > 4; i = i - 2)
        {
            if (i < 8)
            {
                color = Color.yellow;
            }
            Hashtable ht1 = new Hashtable();
            ht1.Add("First", listMajiangAuto[i - 1]);
            ht1.Add("Second", listMajiangAuto[i - 2]);

            ShowContent.changeColor(i - 1, color, plane);
            ShowContent.changeColor(i - 2, color, plane);
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
    private List<Hashtable> JumpOut(GameObject plane)
    {
        List<Hashtable> list = new List<Hashtable>();
        Color color = Color.magenta;

        for (int i = 16; i > 10; i = i - 4)
        {
            if (i < 16)
            {
                color = Color.green;
            }
            Hashtable ht = new Hashtable();
            ht.Add("First", listMajiangAuto[i - 1]);
            ht.Add("Second", listMajiangAuto[i - 3]);

            ShowContent.changeColor(i - 1, color, plane);
            ShowContent.changeColor(i - 3, color, plane);
            list.Add(ht);
        }

        color = Color.yellow;
        for (int i = 8; i > 2; i = i - 4)
        {
            if (i < 8)
            {
                color = Color.cyan;
            }
            Hashtable ht = new Hashtable();
            ht.Add("First", listMajiangAuto[i - 1]);
            ht.Add("Second", listMajiangAuto[i - 3]);

            ShowContent.changeColor(i - 1, color, plane);
            ShowContent.changeColor(i - 3, color, plane);
            list.Add(ht);
        }

        foreach (var item in list)
        {
            Debug.Log(Environment.NewLine + item["First"] + ":" + item["Second"]);
        }
        return list;
    }

    /// <summary>
    /// 恢复原来颜色
    /// </summary>
    private void RestoreColor()
    {
        foreach (Transform item in majiangPlane.transform)
        {
            Image image = item.GetComponent<Image>();
            image.color = new Color(1, 1, 1);
        }
    }
    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="sender"></param>
    private void OnClick(GameObject sender)
    {
        string btnName = sender.name;
        if (btnName == "btnReset")
        {
            txtResult.text = "结果：";
            txtOriginal.text = "原始大小：";
            EventHandler.Reset();
            LoadMaJing(majiangPlane);
           
        }
        else if (btnName == "btnChange")
        {
            sbData.Length = 0;
            showComputerResult("", "");
            ShowContent.showOriginal("", txtOriginal);
            //变换牌面
            //清除面上的所有子元素
            for (int i = 0; i < majiangPlane.transform.childCount; i++)
            {
                Destroy(majiangPlane.transform.GetChild(i).gameObject);
            }
            //重新加载
            GenerateMaJiang();
            for (int i = 0; i < movePlane.transform.childCount; i++)
            {
                Destroy(movePlane.transform.GetChild(i).gameObject);
            }
            LoadMaJing(movePlane);
        }
        else
        {
            //判断麻将值是否有效果
            foreach (var item in listMajiangAuto)
            {
                if (item == 0)
                {
                    txtResult.text = "请确认麻将值是否有效！";
                    return;
                }
            }
        }
        //恢复颜色
        RestoreColor();
        switch (btnName)
        {
            case "btnQuit":
                //退出应用程
                Application.Quit();
                break;

            case "btnSquat":
                sbData.Length = 0;
                showComputerResult("", "");
                ShowContent.showOriginal("", txtOriginal);
                //蹲出
                if (chkAuto.isOn)
                {
                    SquatOutComputer();
                }
                else
                {
                    ShowContent.ShowSort(SquatOut(majiangPlane), txtOriginal);
                }
                break;
            case "btnFlat":
                sbData.Length = 0;
                showComputerResult("", "");
                ShowContent.showOriginal("", txtOriginal);
                if (chkAuto.isOn)
                {
                    FlatOutComputer();
                }
                else
                {
                    //平出
                    ShowContent.ShowSort(FlatOut(majiangPlane), txtOriginal);
                }

                break;
            case "btnJump":
                sbData.Length = 0;
                showComputerResult("", "");
                ShowContent.showOriginal("", txtOriginal);
                if (chkAuto.isOn)
                {
                    JumpOutComputer();
                }
                else
                {
                    //跳出
                    ShowContent.ShowSort(JumpOut(majiangPlane), txtOriginal);
                }
                break;
            case "btnLeftMove":
                sbData.Length = 0;
                showComputerResult("", "");
                ShowContent.showOriginal("", txtOriginal);
                //左称一位
                LeftMoveMaJing();
                break;
            case "btnRightMove":
                sbData.Length = 0;
                showComputerResult("", "");
                ShowContent.showOriginal("", txtOriginal);
                //右移一位
                RightMoveMaJing();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Left移牌计算
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public void LeftMoveMaJing()
    {
        moveStep--;
        if (moveStep < -15)
        {
            moveStep = 0;
        }
        txtMoveCount.text = "左移：" + Math.Abs(moveStep);
        LoopComputer.InverseMove(listMajiangAuto);

        LoadMaJing(majiangPlane);
    }

    /// <summary>
    /// 右移移牌计算
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public void RightMoveMaJing()
    {
        //移除所有
        for (int i = 0; i < majiangPlane.transform.childCount; i++)
        {
            Destroy(majiangPlane.transform.GetChild(i).gameObject);
        }
        moveStep++;
        if (moveStep > 15 || moveStep < 0)
        {
            moveStep = 0;
        }
        txtMoveCount.text = "右移：" + moveStep;

        LoopComputer.ClockwiseeMove(listMajiangAuto);

        LoadMaJing(majiangPlane);
    }

}