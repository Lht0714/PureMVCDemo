using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRoomInfoCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        RoomMenuProxy roomProxy = PureMVC.Patterns.Facade.Instance.RetrieveProxy("RoomMenuProxy") as RoomMenuProxy;
        RoomOrderProxy roomOrderProxy = PureMVC.Patterns.Facade.Instance.RetrieveProxy("RoomOrderProxy") as RoomOrderProxy;
        if (notification.Type== "GetRoomInfo")
        {
            RoomOrder roomOrder = new RoomOrder(notification.Body as ClientItem, roomProxy.Rooms);
            roomOrderProxy.AddOrder(roomOrder);
            SendNotification(OrderSystemEvent.UP_ROOMMENU, roomOrder);
        }
        else if(notification.Type == "CloseRoomIn")
        {
            RoomOrder roomOrder = new RoomOrder(notification.Body as ClientItem, roomProxy.Rooms);
            roomOrderProxy.AddOrder(roomOrder);
            for (int i = 0; i < roomOrder.rooms.Count; i++)
            {
                roomOrder.rooms[i].instock = false;
            }
        }
    }
}
