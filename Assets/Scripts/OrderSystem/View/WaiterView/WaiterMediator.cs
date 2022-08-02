
/*=========================================
* Author: Administrator
* DateTime:2017/6/20 19:21:23
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
    /// 服务员中介
    /// </summary>
    internal class WaiterMediator : Mediator
    {
        private WaiterProxy waiterProxy = null;
        public new const string NAME = "WaiterMediator";
        public WaiterView WaiterView
        {
            get { return (WaiterView) base.ViewComponent; }
        }

        // 订单代理也就是订单的数据（这条数据是由顾客点单得来的）
        private OrderProxy orderProxy = null;

        //给服务员的View里的委托添加方法侦听
        public WaiterMediator( WaiterView view ) : base(NAME, view)
        {
            WaiterView.CallWaiter += ( ) => { SendNotification(OrderSystemEvent.CALL_WAITER); };
            WaiterView.Order += data => { SendNotification(OrderSystemEvent.ORDER , data); };
            WaiterView.Pay += ( ) => { SendNotification(OrderSystemEvent.PAY); };
            WaiterView.CallCook += ( ) => { SendNotification(OrderSystemEvent.CALL_COOK); };
            WaiterView.ServerFood += item => { SendNotification(OrderSystemEvent.selectWaiter, item, "FINISH"); //付完款之和将服务员状态变更
                                                                                                                    };
        }

        public override void OnRegister( )
        {
            base.OnRegister();
            //注册代理
            waiterProxy = Facade.RetrieveProxy(WaiterProxy.NAME) as WaiterProxy;
            orderProxy = Facade.RetrieveProxy(OrderProxy.NAME) as OrderProxy;
            if ( null == waiterProxy )
            {
                throw new Exception(WaiterProxy.NAME + "is null,please check it!");
            }
            if ( null == orderProxy )
            {
                throw new Exception(OrderProxy.NAME + "is null,please check it!");
            }
            WaiterView.UpdateWaiter(waiterProxy.Waiters);
        }
        /// <summary>
        /// 消息添加
        /// </summary>
        /// <returns></returns>
        public override IList<string> ListNotificationInterests( )
        {
            IList<string> notifications = new List<string>();
            notifications.Add(OrderSystemEvent.CALL_WAITER);
            notifications.Add(OrderSystemEvent.ORDER);
            notifications.Add(OrderSystemEvent.GET_PAY);
            notifications.Add(OrderSystemEvent.FOOD_TO_CLIENT);
            notifications.Add(OrderSystemEvent.ResfrshWarite);
            return notifications;
        }
        public override void HandleNotification( INotification notification )
        {
            //根据不同的消息执行不同的逻辑
            switch (notification.Name)
            {
                case OrderSystemEvent.CALL_WAITER:
                    ClientItem client = notification.Body as ClientItem;
                    //请求获取菜单的命令 GetAndExitOrderCommed
                    SendNotification(OrderSystemEvent.GET_ORDER, client, "Get");
                    break;
                case OrderSystemEvent.ORDER:
                    SendNotification(OrderSystemEvent.CALL_COOK , notification.Body);
                    break;
                case OrderSystemEvent.GET_PAY:
                    Debug.Log(" 服务员拿到顾客的付款 ");
                    //顾客数据
                    ClientItem item = notification.Body as ClientItem;
                    //付完款之和将服务员状态变更
                    SendNotification(OrderSystemEvent.GUEST_BE_AWAY, item, "Remove");
                    break;
                case OrderSystemEvent.FOOD_TO_CLIENT:
                    Debug.Log(" 服务员上菜 ");
                    //服务员数据
                    WaiterItem waiterItem = notification.Body as WaiterItem;
                    waiterItem.order.client.state++;
                    SendNotification(OrderSystemEvent.PAY, waiterItem);
                    break;
                case OrderSystemEvent.ResfrshWarite:
                    //创建一个服务员代理
                    waiterProxy = Facade.RetrieveProxy(WaiterProxy.NAME) as WaiterProxy;
                    //刷新一下服务员的状态
                    WaiterView.Move(waiterProxy.Waiters);
                    break;
            }
        }
        
    }
}