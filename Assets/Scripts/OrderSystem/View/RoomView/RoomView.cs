using OrderSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomView : MonoBehaviour
{  
    public UnityAction Pay = null;
    private ObjectPool<RoomItemView> objectPool = null;
    private List<RoomItemView> rooms = new List<RoomItemView>();
    private Transform parent = null;
    // Start is called before the first frame update
    private void Awake()
    {
        parent = this.transform.Find("Content");
        var prefab = Resources.Load<GameObject>("Prefabs/UI/RoomItem");
        objectPool = new ObjectPool<RoomItemView>(prefab, "RoomPool");
    }
    public void UpdateRoom(IList<RoomItem> rooms)
    {
        for (int i = 0; i < this.rooms.Count; i++)
            objectPool.Push(this.rooms[i]);
        this.rooms.AddRange(objectPool.Pop(rooms.Count));
        for (int i = 0; i < this.rooms.Count; i++)
        {
            var client = this.rooms[i];
            client.transform.SetParent(parent);
            client.Init(rooms[i]);
        }
    }
    public RoomOrder roomOrder;

    public void ResfrshRoom(IList<RoomMenuItem> rooms)
    {
        for (int i = 0; i < this.rooms.Count; i++)
        {
            for (int j = 0; j < rooms.Count; j++)
            {
                if (this.rooms[i].roomitem.name == rooms[j].name)
                {
                    this.rooms[i].transform.SetParent(parent);
                    var item = rooms[j];
                    //发送一条修改数据的命令（用来修改房间是否入住）
                    this.rooms[i].ChangeItem(item);
                    break;
                }
            }
        }
    }

    

    /// <summary>
    /// 入住线程
    /// </summary>
    /// <param name="roomItem"></param>
    /// <returns></returns>
    private string InRoom(RoomItem roomItem)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
