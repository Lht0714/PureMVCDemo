using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMenuProxy : Proxy
{
    public new const string NAME = "RoomMenuProxy";
     
    public IList<RoomMenuItem> Rooms
    {
        get { return (IList<RoomMenuItem>)base.Data; }
    }
    /// <summary>
    /// 菜单数据初始
    /// </summary>
    public RoomMenuProxy() : base(NAME, new List<RoomMenuItem>())
    {
        AddRoomMenu(new RoomMenuItem(1, "豪华套房", 2000, true,false));
        AddRoomMenu(new RoomMenuItem(2, "总统套房", 1200, true, false));
        AddRoomMenu(new RoomMenuItem(3, "普通套房", 350, true, false));
        AddRoomMenu(new RoomMenuItem(4, "标间", 100, true, false));
        AddRoomMenu(new RoomMenuItem(5, "双人标间", 150, true, false));
    }

    public void AddRoomMenu(RoomMenuItem item)
    {
        
            Rooms.Add(item);
        
    }
    public void Remove(RoomMenuItem item)
    {
        if (Rooms.Contains(item))
        {
            Rooms.Remove(item);
        }
    }
    public void OutOfStock(RoomMenuItem item)
    {
        foreach (RoomMenuItem menuItem in Rooms)
        {
            if (menuItem.id == item.id)
            {
                menuItem.instock = false;
                return;
            }
        }
    }
}
