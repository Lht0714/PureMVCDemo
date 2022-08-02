using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace OrderSystem 
{ 
    /// <summary>
    /// 顾客的组件View
    /// </summary>
    public class ClientItemView : MonoBehaviour
    {
        private Text text = null;
        private Image image = null;
        public ClientItem client = null;
        public IList<Action<object>> actionList = new List<Action<object>>();
        private void Awake()
        {
            //查找所有组件
            text = transform.Find("Id").GetComponent<Text>();
            image = transform.GetComponent<Image>();
        }

        public void InitClient( ClientItem client )
        {
            this.client = client;
            UpdateState(); 
        }

        private void UpdateState(  )
        {
            if (client==null)
            {
                return;
            }
            Color color = Color.white;
            if ( this.client.state.Equals(0) )
            {  
                color = Color.green;
            }
            else if (this.client.state.Equals(1))
            {
                color = Color.yellow;
            }
            else if ( this.client.state.Equals(2) )
            {
                color = Color.red;
                StartCoroutine(eatting());
            }
            else if(this.client.state.Equals(3))
            {
                StartCoroutine(AddGuest());
            }
            Debug.Log(client.ToString());
            image.color = color;
            text.text = client.ToString();
        }
        /// <summary>
        /// 客人状态改变
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private IEnumerator eatting(float time = 5)
        {
            Debug.Log(client.id + "号桌客人正在就餐");
            yield return new WaitForSeconds(time);
            client.state++;

            Debug.Log(client.id + "客人是否入住");
            actionList[1].Invoke(client);
        }
        IEnumerator Serving(float time = 4)
        {
            yield return  new WaitForSeconds(time);
            actionList[client.state].Invoke(client);
        }

        IEnumerator WaitingMenu(float time = 4)
        {
            yield return new WaitForSeconds(time);
            actionList[client.state].Invoke(client);
        }

        /// <summary>
        /// 来客人了
        /// </summary>
        /// <returns></returns>
        IEnumerator AddGuest(float time = 4)
        {
            yield return new WaitForSeconds(time);
            actionList[0].Invoke(client);
        }
    }
}