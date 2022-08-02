using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItemView : MonoBehaviour
{
    private Text text = null;
    private Image image = null; 
    public RoomItem roomitem = null;
    //public Toggle toggle = null;
    // Start is called before the first frame update
    private void Awake()
    {
        text = transform.Find("Id").GetComponent<Text>();
        image = transform.GetComponent<Image>();
    }
    public void Init(RoomItem roomItem)
    {
        this.roomitem = roomItem;
        text.text = roomitem.ToString();
        if (roomItem == null)
        {
            return;
        }
        Color color = Color.white;
        if (roomItem.state==State.Leisure)
        {
            color = Color.green;
        }
        else if (roomItem.state == State.Checkin)
        {
            color = Color.yellow;
        }
        image.color = color;
    }
    public void ChangeItem(RoomMenuItem item)
    {
        if (roomitem.state==State.Leisure)
        {
            text.text = item.ToString();
            image.color = Color.red;
            roomitem.state = State.Checkin;
            StartCoroutine(Leave());
        }
        else
        {
            Debug.Log("该房间已有人入住");
        }
    }
    IEnumerator Leave(float time = 10)
    {
        yield return new WaitForSeconds(time);
        //发送消息顾客离开
        image.color = Color.white; 
        StartCoroutine(AnewChange());
    }

    IEnumerator AnewChange(float time = 2)
    {
        yield return new WaitForSeconds(time);
        image.color = Color.green;
        roomitem.state = State.Leisure;
        text.text = roomitem.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
