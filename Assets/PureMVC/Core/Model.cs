namespace PureMVC.Core
{
    using PureMVC.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    /// <summary>
    /// 数据类
    /// </summary>
    public class Model : IModel
    {
        protected static volatile IModel m_instance;
        protected IDictionary<string, IProxy> m_proxyMap = new Dictionary<string, IProxy>();
        protected static readonly object m_staticSyncRoot = new object();
        protected readonly object m_syncRoot = new object();
        /// <summary>
        /// 构造函数
        /// </summary>
        protected Model()
        {
            this.InitializeModel();
        }
        //查找返回Bool判断是否查找到要的数据
        public virtual bool HasProxy(string proxyName)
        {
            lock (this.m_syncRoot)
            {
                return this.m_proxyMap.ContainsKey(proxyName);
            }
        }

        protected virtual void InitializeModel()
        {
        }
        /// <summary>
        /// 注册代理
        /// </summary>
        /// <param name="proxy"></param>
        public virtual void RegisterProxy(IProxy proxy)
        {
            lock (this.m_syncRoot)
            {
                this.m_proxyMap[proxy.ProxyName] = proxy;
            }
            proxy.OnRegister();
        }
        //删除并返回代理
        public virtual IProxy RemoveProxy(string proxyName)
        {
            IProxy proxy = null;
            lock (this.m_syncRoot)
            {
                //判断代理名是否存在
                if (this.m_proxyMap.ContainsKey(proxyName))
                {
                    proxy = this.RetrieveProxy(proxyName);
                    this.m_proxyMap.Remove(proxyName);
                }
            }
            if (proxy != null)
            {
                proxy.OnRemove();
            }
            return proxy;
        }
        /// <summary>
        /// 查找代理
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns></returns>
        public virtual IProxy RetrieveProxy(string proxyName)
        {
            lock (this.m_syncRoot)
            {
                if (!this.m_proxyMap.ContainsKey(proxyName))
                {
                    return null;
                }
                return this.m_proxyMap[proxyName];
            }
        }

        public static IModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new Model();
                        }
                    }
                }
                return m_instance;
            }
        }
    }
}