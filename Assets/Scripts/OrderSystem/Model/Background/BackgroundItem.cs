using OrderSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 后台数据
/// </summary>
public class BackgroundItem
{
    /// <summary>
    /// 所有的顾客信息和点菜信息
    /// </summary>
    public List<Order> ClientInfos = new List<Order>();
    public List<RoomOrder> RoomInfos = new List<RoomOrder>();
    public int AllMoney;
    public List<string> GetAllInfo()
    {
        List<string> str = new List<string>();
        for (int i = 0; i < ClientInfos.Count; i++)
        {
            str.Add(ClientInfos[i].ToString());
        }
        for (int i = 0; i < RoomInfos.Count; i++)
        {
            str.Add(RoomInfos[i].ToString());
        }
        return str;
    }
}
