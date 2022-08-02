using OrderSystem;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundProxy : Proxy
{
    public new const string NAME = "BackgroundProxy";
    public float Money = 0;
    public BackgroundItem BackgroundInfo
    {
        get { return (BackgroundItem)base.Data; }
    }
    public BackgroundProxy():base(NAME, new BackgroundItem())
    {
        
    }
    public void AddBackgroundInfoOrder(Order order)
    {
        BackgroundInfo.ClientInfos.Add(order);
        Money += order.pay;
        SendNotification(OrderSystemEvent.RefreshBack, order.ToString());
        SendNotification(OrderSystemEvent.RefreshMoney, Money);
    }
    public void AddBackgroundInfoRoomOrder(RoomOrder roomorder)
    {
        BackgroundInfo.RoomInfos.Add(roomorder);
        Money += roomorder.pay;
        for (int i = 0; i < roomorder.rooms.Count; i++)
        {
            roomorder.rooms[i].instock = true;
        }
        SendNotification(OrderSystemEvent.RefreshBack, roomorder.ToString());
        SendNotification(OrderSystemEvent.RefreshMoney, Money);
    }
}
