using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OrderSystem;
/// <summary>
/// 所以数据的综合
/// 后台
/// </summary>
public class RoomCommend : SimpleCommand
{
    
    public override void Execute(INotification notification)
    {
        BackgroundProxy BackgroundProxy = Facade.RetrieveProxy(BackgroundProxy.NAME) as BackgroundProxy;
        if (notification.Type=="Order")
        {
            Order order = notification.Body as Order;
            BackgroundProxy.AddBackgroundInfoOrder(order);
            //显示更新
        }
        else if (notification.Type=="RoomOrder")
        {
            RoomOrder order = notification.Body as RoomOrder;
            BackgroundProxy.AddBackgroundInfoRoomOrder(order);
        }

    }
}
