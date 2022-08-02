using System.Collections;
using System.Collections.Generic;
using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

/// <summary>
/// 厨师命令
/// </summary>
public class CookCommend : SimpleCommand
{
   
    public override void Execute(INotification notification)
    {
        CookProxy cookProxy = Facade.RetrieveProxy(CookProxy.NAME) as CookProxy; //厨师的代理
        Order order = notification.Body as Order;//订单
        cookProxy.CookCooking(order);//给厨师的代理赋值
    }
}
