using OrderSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��̨����
/// </summary>
public class BackgroundItem
{
    /// <summary>
    /// ���еĹ˿���Ϣ�͵����Ϣ
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
