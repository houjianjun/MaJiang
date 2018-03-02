using UnityEngine;
using UnityEngine.UI;
using System;
/// <summary>
/// 事件处理类
/// </summary>
public class EventHandler
{

    /// <summary>
    /// 手动输入事件
    /// </summary>
    /// <param name="toggle"></param>
    /// <param name="value"></param>
    public static void OnToggleClick(Toggle toggle, bool value)
    {
        if (value)
        {
            //禁用切牌按钮
            main.btn0.interactable = false;
            main.listMajiangAuto = main.listMajiangManual;
            main.btn7.interactable = true;
        }
        else
        {
            main.btn7.interactable = false;
            main.btn0.interactable = true;
        }
    }

    /// <summary>
    /// 按键事件
    /// </summary>
    /// <returns></returns>
    public static void getKeyDownCode(GameObject plane)
    {
        //if (Input.GetKeyDown(KeyCode.Keypad0)|| Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    Debug.Log("您按下了0键");
        //}

        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            KeyShowValue(plane, 1);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            KeyShowValue(plane, 2);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            KeyShowValue(plane, 3);
        }

        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            KeyShowValue(plane, 4);
        }

        if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
        {
            KeyShowValue(plane, 5);
        }

        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
        {
            KeyShowValue(plane, 6);
        }

        if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7))
        {
            KeyShowValue(plane, 7);
        }

        if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8))
        {
            KeyShowValue(plane, 8);
        }

        if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9))
        {
            KeyShowValue(plane, 9);
        }
    }
    /// <summary>
    /// 根据按键显示值
    /// </summary>
    /// <param name="plane"></param>
    private static void KeyShowValue(GameObject plane, int value)
    {
        if (main.inputNum < 0)
        {
            main.inputNum = 15;
        }
        int index = main.inputNum--;
        ShowContent.ChangValue(index, value, plane);
        main.listMajiangAuto[index] = value;
    }
    /// <summary>
    /// 重置牌面
    /// </summary>
    /// <param name="plane"></param>
    public static void Reset()
    {
        main.inputNum = 15;
        for (int i = 0; i < main.listMajiangAuto.Count; i++)
        {
            main.listMajiangAuto[i] = 0;
        }
        main.listMajiangManual = main.listMajiangAuto;
    }
}
