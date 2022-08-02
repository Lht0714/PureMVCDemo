namespace PureMVC.Patterns
{
    using PureMVC.Interfaces;
    using System;

    /// <summary>
    /// 模型层
    /// 1.发送但不接收消息。
    /// 2.与服务器端连接，获取与上传业务数据。
    /// 3.大型网络游戏，可以进一步抽象出“数据代理服务层”,专门从事与服务器交互通信事宜。
    /// </summary>
    public class Proxy : Notifier, IProxy, INotifier
    {
        protected object m_data;
        protected string m_proxyName;
        public static string NAME = "Proxy";

        public Proxy() : this(NAME, null)
        {

        }
        public Proxy(string proxyName) : this(proxyName, null)
        {
        }

        public Proxy(string proxyName, object data)
        {
            this.m_proxyName = (proxyName != null) ? proxyName : NAME;
            if (data != null)
            {
                this.m_data = data;
            }
        }

        public virtual void OnRegister()
        {
        }

        public virtual void OnRemove()
        {
        }

        public object Data
        {
            get
            {
                return this.m_data;
            }
            set
            {
                this.m_data = value;
            }
        }

        public string ProxyName
        {
            get
            {
                return this.m_proxyName;
            }
        }
    }
}

