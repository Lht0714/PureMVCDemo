using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMenuItem : MonoBehaviour
{
    public int id { get; set; }
    public string name { get; set; }
    public float price { get; set; }
    public int numpro;//人数
    public bool instock { get; set; } //是否有人
    public bool iselected { get; set; }

    public RoomMenuItem(int id, string name, float price, bool instock,bool iselected)
    {
        this.id = id; 
        this.name = name;
        this.price = price;
        this.instock = instock;
        this.iselected = iselected;
    }
    public override string ToString()
    {
        if (instock)
        {
            return id + "号房间" + "\n" + name + "已有人入住";
        }
        else
        {
            return id + "号房间" + "\n" + name;
        }
    }
}
