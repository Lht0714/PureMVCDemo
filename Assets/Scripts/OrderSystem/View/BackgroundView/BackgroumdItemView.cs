using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroumdItemView : MonoBehaviour
{
    public Text info;//œÍœ∏–≈œ¢

    // Start is called before the first frame update
    private void Awake()
    {
        info = transform.GetComponent<Text>();
    }
    public void Init(string str)
    {
        info.text = str;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
