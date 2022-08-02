using System.Collections;
using System.Collections.Generic;
using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine; 
public class GetAndExitOrderCommed : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        OrderProxy orderProxy = PureMVC.Patterns.Facade.Instance.RetrieveProxy("OrderProxy") as OrderProxy;//订单代理
        MenuProxy menuProxy = PureMVC.Patterns.Facade.Instance.RetrieveProxy("MenuProxy") as MenuProxy;//菜单代理
        Debug.Log(notification.Type);
        if (notification.Type =="Get")
        {
            Order order = new Order(notification.Body as ClientItem,menuProxy.Menus); 
            orderProxy.AddOrder(order);
            SendNotification(OrderSystemEvent.UPMENU, order);//要更改数据所以发送命令
        }
        else if (notification.Type =="Exit") //删除菜单 
        {
            Order order = new Order(notification.Body as ClientItem, menuProxy.Menus);
            orderProxy.RemoveOrder(order); 
        }
    }
}
