using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMediator : Mediator 
{
    public RoomProxy roomProxy = null;//房间代理
    public new const string NAME = "RoomMediator";
    private RoomView View//顾客的界面
    {
        get { return (RoomView)ViewComponent; }
    }
    public RoomMediator(RoomView roomView) : base(NAME, roomView)
    {
        //View.Pay += () => { SendNotification(OrderSystemEvent.); };
    }
    /// <summary>
    /// 添加中介时会调用
    /// </summary>
    public override void OnRegister()
    {
        base.OnRegister();
        roomProxy = Facade.RetrieveProxy(RoomProxy.NAME) as RoomProxy;
        if (null == roomProxy)
        {
            throw new Exception("获取" + RoomProxy.NAME + "代理失败");
        }
        View.UpdateRoom(roomProxy.Rooms);
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> notifications = new List<string>();
        notifications.Add(OrderSystemEvent.Get_RoomInfo);
        notifications.Add(OrderSystemEvent.CHECKIN);
        return notifications;
    }
    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        switch (notification.Name)
        {
            case OrderSystemEvent.Get_RoomInfo:
                {
                    ClientItem client = notification.Body as ClientItem;
                    Debug.Log("发送一条打开面板的消息");
                    //client.state++;
                    SendNotification(OrderSystemEvent.ADD_GUEST, client);
                    SendNotification(OrderSystemEvent.GET_ROOMORDER, client, "GetRoomInfo");
                }
                break;
            case OrderSystemEvent.CHECKIN:
                {
                    //这里有每一卓顾客订单的所有信息（吃饭和住店）
                    RoomOrder roomOrder = notification.Body as RoomOrder;
                    SendNotification(OrderSystemEvent.Add_AllInfo, roomOrder, "RoomOrder");
                    //发送一条更改房间数据的命令
                    View.ResfrshRoom(roomOrder.rooms);
                }
                break;
                //发送一条修改后台房间信息的消息(添加一条结账的信息)
        }
    }
}
