using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOrderProxy : Proxy
{
    public new const string NAME = "RoomOrderProxy";
    public IList<RoomOrder> Orders
    {
        get { return (IList<RoomOrder>)base.Data; }
    }

    public RoomOrderProxy() : base(NAME, new List<RoomOrder>())
    {
        //todo 订单应该自来于顾客
    }

    public void AddOrder(RoomOrder order)
    {
        order.id = Orders.Count + 1;
        Orders.Add(order);
    }
    public void RemoveOrder(RoomOrder order)
    {
        Orders.Remove(order);
    }
}
