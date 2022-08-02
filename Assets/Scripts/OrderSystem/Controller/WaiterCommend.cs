using System.Collections;
using System.Collections.Generic;
using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
/// <summary>
/// 服务员命令
/// </summary>
public class WaiterCommend : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        WaiterProxy waiterProxy = Facade.RetrieveProxy(WaiterProxy.NAME) as WaiterProxy;
        //判断服务员的当前行为
        if (notification.Type == "SERVING")
        {
            Debug.Log("寻找服务员上菜");
            waiterProxy.ChangeWaiter(notification.Body as Order);
        }else if (notification.Type == "FINISH")//服务员完成所有工作
        {
            Debug.Log("服务员没事干");
            waiterProxy.RemoveWaiter(notification.Body as WaiterItem);
        }
    }
}
