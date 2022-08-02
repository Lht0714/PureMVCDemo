using System.Collections;
using System.Collections.Generic;
using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

/// <summary>
/// 顾客命令
/// </summary>
public class GuestBeAwayCommed : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
       ClientProxy clientProxy =   Facade.RetrieveProxy(ClientProxy.NAME) as ClientProxy;//顾客代理 
       if (notification.Type =="Add")
       {
            ClientItem client =  notification.Body as ClientItem;
            client.state = 0;
            client.population = Random.Range(3, 14);
            clientProxy.AddClient(client);//客人代理赋值
       }else if (notification.Type == "Remove")
       {
           Debug.Log("客人走了");
           Debug.Log(notification.Body.GetType());
           clientProxy.DeleteClient(notification.Body as ClientItem);//删除对应的数据
        }
    }
}
