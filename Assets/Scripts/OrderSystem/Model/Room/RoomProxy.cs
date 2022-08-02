using OrderSystem;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomProxy : Proxy
{
    public new const string NAME = "RoomProxy";
    public IList<RoomItem> Rooms
    {
        get 
        {
            return (IList<RoomItem>)base.Data;
        }
    }
    public void DeleteRoom(ClientItem item)
    {
        Debug.Log(item);
        for (int i = 0; i < Rooms.Count; i++)
        {
            if (Rooms[i].id == item.id)
            {
                Rooms[i].state = State.Null;
                //SendNotification(OrderSystemEvent.ADD_Roomguest, Rooms[i]);
                return;
            }
        }
    }
    public RoomProxy() : base(NAME, new List<RoomItem>())
    {
        AddRoom(new RoomItem(1, "豪华套房", 2, State.Leisure));
        AddRoom(new RoomItem(2, "总统套房", 1, State.Leisure));
        AddRoom(new RoomItem(3, "普通套房", 4, State.Leisure));
        AddRoom(new RoomItem(4, "标间", 1, State.Leisure));
        AddRoom(new RoomItem(5,"双人标间", 2, State.Leisure));
    }
    public void AddRoom(RoomItem item)
    {
        if (Rooms.Count < 5)
        {
            Rooms.Add(item);
        }
        UpdateRoom(item);

    }
    public void UpdateRoom(RoomItem item)
    {
        Debug.Log(item.state);
        for (int i = 0; i < Rooms.Count; i++)
        {
            if (Rooms[i].id == item.id)
            {
                Rooms[i] = item;
                Rooms[i].state = item.state;
                Rooms[i].population = item.population;
                //SendNotification(OrderSystemEvent.ADD_Roomguest, Rooms[i]);
                return;
            }
        }
    }
}
