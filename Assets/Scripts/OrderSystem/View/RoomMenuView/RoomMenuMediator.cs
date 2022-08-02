using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMenuMediator : Mediator
{
    private RoomMenuProxy menuProxy = null;
    public new const string NAME = "RoomMenuMediator";
    public RoomMenuView RoomMenuView
    {
        get { return (RoomMenuView)ViewComponent; }
    }

    public RoomMenuMediator(RoomMenuView view) : base(NAME, view)
    {
        RoomMenuView.Submit += order => { SendNotification(OrderSystemEvent.ROOMSUBMITMENU, order); };
        RoomMenuView.Cancel += () => { SendNotification(OrderSystemEvent.CANCEL_ORDER); };
    }

    public override void OnRegister()
    {
        base.OnRegister();
        menuProxy = Facade.RetrieveProxy(RoomMenuProxy.NAME) as RoomMenuProxy;
        if (null == menuProxy)
            throw new Exception(RoomMenuProxy.NAME + "is null!");
        RoomMenuView.UpdateMenu(menuProxy.Rooms);
    }

    public override IList<string> ListNotificationInterests()
    {
        IList<string> notifications = new List<string>();
        notifications.Add(OrderSystemEvent.UP_ROOMMENU);
        notifications.Add(OrderSystemEvent.ROOMSUBMITMENU);
        return notifications;
    }
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case OrderSystemEvent.UP_ROOMMENU:
                {
                    RoomOrder roomOrder = notification.Body as RoomOrder;
                    if (null == roomOrder)
                        throw new Exception("order is null ,plase check it!");
                    RoomMenuView.UpMenu(roomOrder);
                    SendNotification(OrderSystemEvent.GET_ROOMORDER, roomOrder, "CloseRoomIn");
                }
                break;
            case OrderSystemEvent.ROOMSUBMITMENU:
                {
                    RoomOrder roomOrder = notification.Body as RoomOrder;
                    //把所有选择的房间的状态改变
                    RoomMenuView.SubmitMenu(roomOrder);
                    //发送一条修改房间信息的命令
                    SendNotification(OrderSystemEvent.CHECKIN,roomOrder);                  
                }
                break;

        }

    }
}
