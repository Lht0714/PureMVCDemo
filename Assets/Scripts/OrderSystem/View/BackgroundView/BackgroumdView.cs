using OrderSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroumdView : MonoBehaviour
{
    public Button CloseBtn;
    public Transform Contont;
    public Text Money;
    private ObjectPool<BackgroumdItemView> objectPool = null;
    List<BackgroumdItemView> itemView = new List<BackgroumdItemView>();
    int k = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        CloseBtn = transform.Find("Close").GetComponent<Button>();
        CloseBtn.onClick.AddListener(() =>
        {
            transform.localPosition = new Vector3(0, -800, 0);
        }
        );
        var prefab = Resources.Load<GameObject>("Prefabs/UI/Text");
        objectPool = new ObjectPool<BackgroumdItemView>(prefab, "BackPool");
        
    }
    public void AddBackText(string str)
    {
        itemView.AddRange(objectPool.Pop(1));
        itemView[k].transform.SetParent(Contont);
        itemView[k].gameObject.SetActive(true);
        itemView[k].Init(str);
        k++;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    internal void RefreshMoney(float money)
    {
    }
}
