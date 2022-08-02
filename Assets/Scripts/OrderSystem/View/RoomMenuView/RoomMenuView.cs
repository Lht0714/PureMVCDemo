using OrderSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomMenuView : MonoBehaviour
{
    public UnityAction<RoomOrder> Submit = null;
    public UnityAction Cancel = null;

    private ObjectPool<RoomMenuItemView> objectPool = null;
    private List<RoomMenuItemView> Roommenus = new List<RoomMenuItemView>();
    private Transform parent = null;

    private void Awake()
    {
        parent = this.transform.Find("Content");
        var prefab = Resources.Load<GameObject>("Prefabs/UI/RoomMenuItem");
        objectPool = new ObjectPool<RoomMenuItemView>(prefab, "RoomMenuPool");
        transform.Find("SubmitButton").GetComponent<Button>().onClick.AddListener(() => { Submit(indexOrder); });
        transform.Find("CancelButton").GetComponent<Button>().onClick.AddListener(CancelMenu);
    }

    public void UpdateMenu(IList<RoomMenuItem> menus)
    {
        for (int i = 0; i < this.Roommenus.Count; i++)
            objectPool.Push(this.Roommenus[i]);

        this.Roommenus.AddRange(objectPool.Pop(menus.Count));

        for (int i = 0; i < this.Roommenus.Count; i++)
        {
            this.Roommenus[i].transform.SetParent(parent);
            var item = this.Roommenus[i];
            item.InitData(menus[i]);
        } 
    }

    private RoomOrder indexOrder = null;
    public void UpMenu(RoomOrder order)
    {
        ResetMenu();
        indexOrder = order;
        this.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void SubmitMenu(RoomOrder order)
    {
        //房间订单里的选择房间信息
        order.rooms = GetSelected();
        CancelMenu();
    }

    public void CancelMenu()
    {
        this.transform.localPosition = new Vector3(0, -800, 0);
    }

    private IList<RoomMenuItem> GetSelected()
    {
        IList<RoomMenuItem> result = new List<RoomMenuItem>();
        for (int i = 0; i < Roommenus.Count; i++)
            if (Roommenus[i].RoomMenu.iselected)
                result.Add(Roommenus[i].RoomMenu);
        return result;
    }

    private void ResetMenu()
    {
        foreach (RoomMenuItemView menu in Roommenus)
            menu.toggle.isOn = false;
    }
}
