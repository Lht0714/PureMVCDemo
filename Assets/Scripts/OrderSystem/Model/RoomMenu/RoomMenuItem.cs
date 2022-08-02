using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMenuItem : MonoBehaviour
{
    public int id { get; set; }
    public string name { get; set; }
    public float price { get; set; }
    public int numpro;//����
    public bool instock { get; set; } //�Ƿ�����
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
            return id + "�ŷ���" + "\n" + name + "��������ס";
        }
        else
        {
            return id + "�ŷ���" + "\n" + name;
        }
    }
}
