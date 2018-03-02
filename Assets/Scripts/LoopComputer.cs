using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 循环计算
/// </summary>
public class LoopComputer  {

    /// <summary>
    /// 顺时针循环
    /// </summary>
    /// <param name="ints"></param>
    public static void ClockwiseeMove(List<int> ints)
    {
        int left = ints[7];
        int right = ints[8];
        for (int begin = 7, end = 8; begin > 0; begin--, end++)
        {

            ints[begin] = ints[begin - 1];
            ints[end] = ints[end + 1];
        }
        ints[15] = left;
        ints[0] = right;
    }

    /// <summary>
    /// 逆时针循环
    /// </summary>
    /// <param name="ints"></param>
    public static void InverseMove(List<int> ints)
    {
        int left = ints[0];
        int right = ints[15];
        for (int begin = 0, end = 15; begin < end; begin++, end--)
        {

            ints[begin] = ints[begin + 1];
            ints[end] = ints[end - 1];
        }
        ints[7] = right;
        ints[8] = left;
    }

}
