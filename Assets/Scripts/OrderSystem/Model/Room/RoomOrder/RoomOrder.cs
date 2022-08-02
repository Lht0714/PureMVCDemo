using OrderSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOrder 
{
    public int id { get; set; }
    public ClientItem client { get; set; }//��һ����������Ϣ
    public IList<RoomMenuItem> rooms { get; set; }//���з�����Ϣ
    public float pay
    {
        get
        {
            var money = 0.0f;
            foreach (RoomMenuItem menu in rooms)
                money += menu.price;
            return money;
        }
    }
    public string names
    {
        get
        {
            string name = "";
            foreach (RoomMenuItem menu in rooms)
                name += menu.name + ",";
            return name;
        }
    }

    public RoomOrder(ClientItem client, IList<RoomMenuItem> rooms)
    {
        this.client = client;
        this.rooms = rooms;
    }
    public override string ToString()
    {
        return client.id + "�ŷ��� " + rooms.Count + "�䷿" + pay + "Ԫ";
    }
}
