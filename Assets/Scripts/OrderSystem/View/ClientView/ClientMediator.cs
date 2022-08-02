
/*=========================================
* Author: Administrator
* DateTime:2017/6/20 19:20:37
* Description:$safeprojectname$
==========================================*/

using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

namespace OrderSystem
{
    /// <summary>
    /// 顾客的中介
    /// </summary>
    public class ClientMediator : Mediator
    {
        private ClientProxy clientProxy = null;//顾客代理
        public new const string NAME = "ClientMediator";
        private ClientView View//顾客的界面
        {
            get { return (ClientView)ViewComponent; }
        }

        public ClientMediator( ClientView view ) : base(NAME , view)
        {
            //给顾客的委托添加绑定事件
            view.CallWaiter += data => { SendNotification(OrderSystemEvent.CALL_WAITER , data); };
            view.Order += data => { SendNotification(OrderSystemEvent.ORDER , data); };
            view.Pay += ( ) => { SendNotification(OrderSystemEvent.PAY); };
        }

        public override void OnRegister()
        {
            base.OnRegister();
            clientProxy = Facade.RetrieveProxy(ClientProxy.NAME) as ClientProxy;
            if(null == clientProxy)
            {
                throw new Exception("获取" + ClientProxy.NAME + "代理失败");
            }    
            IList<Action<object> > actionList = new List<Action<object>>()
            {
                item =>  SendNotification(OrderSystemEvent.GUEST_BE_AWAY, item, "Add"),
                item =>  SendNotification(OrderSystemEvent.Get_RoomInfo, item), 
                item =>  SendNotification(OrderSystemEvent.GET_PAY, item),
            };
            View.UpdateClient(clientProxy.Clients,actionList);
        }
        /// <summary>
        /// 消息添加
        /// </summary>
        /// <returns></returns>
        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(OrderSystemEvent.CALL_WAITER);
            notifications.Add(OrderSystemEvent.ORDER);
            notifications.Add(OrderSystemEvent.PAY);
            notifications.Add(OrderSystemEvent.ADD_GUEST);
            return notifications;
        }
        /// <summary>
        /// 根据不同的消息名执行不同的逻辑
        /// </summary>
        /// <param name="notification"></param>
        public override void HandleNotification(INotification notification)
        {
            Debug.Log(notification.Name);
            switch (notification.Name)
            {
                case OrderSystemEvent.CALL_WAITER:
                    ClientItem client = notification.Body as ClientItem;
                    Debug.Log(client.id + " 号桌顾客呼叫服务员 , 索要菜单 ");
                    break;
                case OrderSystemEvent.ORDER: 
                    Order order1 = notification.Body as Order;
                    if(null == order1)
                        throw new Exception("order1 is null ,please check it!");
                    order1.client.state++;
                    View.UpdateState(order1.client);
                    break;
                case OrderSystemEvent.PAY:
                    WaiterItem item = notification.Body as WaiterItem;
                    View.UpdateState(item.order.client);
                    //发送去更改流水
                    SendNotification(OrderSystemEvent.Add_AllInfo, item.order, "Order");
                    break;
                case OrderSystemEvent.ADD_GUEST:
                    Debug.Log("刷新界面");
                    ClientItem clientItem = notification.Body as ClientItem;
                    if (null == clientProxy)
                        throw new Exception("获取" + ClientProxy.NAME + "代理失败");
                    View.UpdateState(clientItem); 
                    break;
            }
        }
    }
}