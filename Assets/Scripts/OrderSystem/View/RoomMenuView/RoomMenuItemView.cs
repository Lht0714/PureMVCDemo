using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMenuItemView : MonoBehaviour
{
    public RoomMenuItem RoomMenu = null;

    public Toggle toggle = null;

    private void Awake()
    {
        toggle = transform.Find("Toggle").GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(isOn => { RoomMenu.iselected = isOn; });
    }

    public void InitData(RoomMenuItem menu)
    {
        RoomMenu = menu;
        transform.Find("Price").GetComponent<Text>().text = menu.price.ToString();
        string menuName = menu.name;
        toggle.transform.Find("Label").GetComponent<Text>().text = menuName;
        toggle.interactable = menu.instock;
        toggle.isOn = menu.iselected;
    }
}
