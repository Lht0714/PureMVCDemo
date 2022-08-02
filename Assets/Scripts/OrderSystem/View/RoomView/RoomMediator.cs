using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMediator : Mediator 
{
    public RoomProxy roomProxy = null;//�������
    public new const string NAME = "RoomMediator";
    private RoomView View//�˿͵Ľ���
    {
        get { return (RoomView)ViewComponent; }
    }
    public RoomMediator(RoomView roomView) : base(NAME, roomView)
    {
        //View.Pay += () => { SendNotification(OrderSystemEvent.); };
    }
    /// <summary>
    /// ����н�ʱ�����
    /// </summary>
    public override void OnRegister()
    {
        base.OnRegister();
        roomProxy = Facade.RetrieveProxy(RoomProxy.NAME) as RoomProxy;
        if (null == roomProxy)
        {
            throw new Exception("��ȡ" + RoomProxy.NAME + "����ʧ��");
        }
        View.UpdateRoom(roomProxy.Rooms);
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> notifications = new List<string>();
        notifications.Add(OrderSystemEvent.Get_RoomInfo);
        notifications.Add(OrderSystemEvent.CHECKIN);
        return notifications;
    }
    public override void HandleNotification(INotification notification)
    {
        base.HandleNotification(notification);
        switch (notification.Name)
        {
            case OrderSystemEvent.Get_RoomInfo:
                {
                    ClientItem client = notification.Body as ClientItem;
                    Debug.Log("����һ����������Ϣ");
                    //client.state++;
                    SendNotification(OrderSystemEvent.ADD_GUEST, client);
                    SendNotification(OrderSystemEvent.GET_ROOMORDER, client, "GetRoomInfo");
                }
                break;
            case OrderSystemEvent.CHECKIN:
                {
                    //������ÿһ׿�˿Ͷ�����������Ϣ���Է���ס�꣩
                    RoomOrder roomOrder = notification.Body as RoomOrder;
                    SendNotification(OrderSystemEvent.Add_AllInfo, roomOrder, "RoomOrder");
                    //����һ�����ķ������ݵ�����
                    View.ResfrshRoom(roomOrder.rooms);
                }
                break;
                //����һ���޸ĺ�̨������Ϣ����Ϣ(���һ�����˵���Ϣ)
        }
    }
}
