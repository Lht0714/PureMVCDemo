using System;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

namespace OrderSystem
{
    internal class StartUpCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            //菜单代理
            MenuProxy menuProxy = new MenuProxy();
            Facade.RegisterProxy(menuProxy);

            //客户端代理
            ClientProxy clientProxy = new ClientProxy();
            Facade.RegisterProxy(clientProxy);

            //服务员代理
            WaiterProxy waitProxy = new WaiterProxy();
            Facade.RegisterProxy(waitProxy);

            //厨师代理
            CookProxy cookProxy = new CookProxy();
            Facade.RegisterProxy(cookProxy);
            //订单代理
            OrderProxy orderProxy = new OrderProxy();
            Facade.RegisterProxy(orderProxy);
            //MainUi
            //房间代理
            RoomProxy roomProxy = new RoomProxy();
            Facade.RegisterProxy(roomProxy);

            RoomMenuProxy roomMenuProxy = new RoomMenuProxy();
            Facade.RegisterProxy(roomMenuProxy);

            RoomOrderProxy roomOrderProxy = new RoomOrderProxy();
            Facade.RegisterProxy(roomOrderProxy);

            BackgroundProxy backgroundProxy = new BackgroundProxy();
            Facade.RegisterProxy(backgroundProxy);
            MainUI mainUI = notification.Body as MainUI;

            if(null == mainUI)
                throw new Exception("程序启动失败..");
            //注册中介
            Facade.RegisterMediator(new MenuMediator(mainUI.MenuView));
            Facade.RegisterMediator(new ClientMediator(mainUI.ClientView)); 
            Facade.RegisterMediator(new WaiterMediator(mainUI.WaitView));
            Facade.RegisterMediator(new CookMediator(mainUI.CookView));
            Facade.RegisterMediator(new RoomMediator(mainUI.roomView));
            Facade.RegisterMediator(new RoomMenuMediator(mainUI.RoomMenuView));
            Facade.RegisterMediator(new BackgroundMediator(mainUI.BackgroumdView));
            //注册命令
            Facade.RegisterCommand(OrderSystemEvent.GUEST_BE_AWAY,typeof(GuestBeAwayCommed));
            Facade.RegisterCommand(OrderSystemEvent.GET_ORDER,typeof(GetAndExitOrderCommed));
            Facade.RegisterCommand(OrderSystemEvent.CookCooking, typeof(CookCommend));
            Facade.RegisterCommand(OrderSystemEvent.selectWaiter, typeof(WaiterCommend));
            Facade.RegisterCommand(OrderSystemEvent.Add_AllInfo, typeof(RoomCommend));
            Facade.RegisterCommand(OrderSystemEvent.GET_ROOMORDER, typeof(GetRoomInfoCommand));
        }
    }
}