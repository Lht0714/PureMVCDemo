using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMediator : Mediator
{
    private BackgroundProxy backProxy = null;//��̨����
    public new const string NAME = "BackgroundMediator";
    private BackgroumdView View//�˿͵Ľ���
    {
        get { return (BackgroumdView)ViewComponent; }
    }
    public BackgroundMediator(BackgroumdView view) : base(NAME, view)
    {
        
    }
    public override void OnRegister()
    {
        backProxy= Facade.RetrieveProxy(BackgroundProxy.NAME) as BackgroundProxy;
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> notifications = new List<string>();
        notifications.Add(OrderSystemEvent.RefreshBack);
        notifications.Add(OrderSystemEvent.RefreshMoney);
        return notifications;
    }
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case OrderSystemEvent.RefreshBack:
                {
                    string str = notification.Body as string;
                    View.AddBackText(str);
                }
                break;
            case OrderSystemEvent.RefreshMoney:
                {
                    float money =(float)notification.Body;
                    View.RefreshMoney(money);
                }
                break;
        }
    }
}
