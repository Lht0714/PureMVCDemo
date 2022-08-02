using UnityEngine;

namespace PureMVC.Core
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// 界面显示层C层
    /// </summary>
    public class View : IView
    {
        protected static volatile IView m_instance;
        protected IDictionary<string, IMediator> m_mediatorMap = new Dictionary<string, IMediator>();
        protected IDictionary<string, IList<IObserver>> m_observerMap = new Dictionary<string, IList<IObserver>>();
        protected static readonly object m_staticSyncRoot = new object();
        protected readonly object m_syncRoot = new object();

        protected View()
        {
            this.InitializeView();
        }
        //查找中介
        public virtual bool HasMediator(string mediatorName)
        {
            lock (this.m_syncRoot)
            {
                return this.m_mediatorMap.ContainsKey(mediatorName);
            }
        }
        //通知观察者
        protected virtual void InitializeView()
        {
        }
        //通知观察者
        public virtual void NotifyObservers(INotification notification)
        {
            IList<IObserver> list = null;
            lock (this.m_syncRoot)
            {
                //判断是否存在该观察者
                if (this.m_observerMap.ContainsKey(notification.Name))
                {
                    //存在：创建一个观察者接口集合将对应的观察者添加到集合里
                    IList<IObserver> collection = this.m_observerMap[notification.Name];
                    list = new List<IObserver>(collection);
                }
            }
            //判断集合是否为空，不为空遍历执行每一个观察者的NotifyObserver
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    //执行每一个观察者的NotifyObserver
                    list[i].NotifyObserver(notification);
                }
            }
        }
        //添加中介
        public virtual void RegisterMediator(IMediator mediator)
        {
            lock (this.m_syncRoot)
            {
                //判断中介库里是否存在相同的中介
                if (this.m_mediatorMap.ContainsKey(mediator.MediatorName))
                {
                    return;
                }
                //不存在：将此中介添加到中介库里，以中介名字为键
                this.m_mediatorMap[mediator.MediatorName] = mediator;
                //根据（ListNotificationInterests）方法获取中介的消息集合
                IList<string> list = mediator.ListNotificationInterests();
                //判断消息集合是否大于0
                if (list.Count > 0)
                {
                    //大于0：创建一个观察者，观察中介里的方法handleNotification
                    IObserver observer = new Observer("handleNotification", mediator);
                    for (int i = 0; i < list.Count; i++)
                    {
                        //遍历消息集合调用（RegisterObserver）将方法名注册给对应的观察者（将每条消息和观察者作为参数）
                        this.RegisterObserver(list[i].ToString(), observer);
                    }
                }
            }
            mediator.OnRegister();
        }
        //添加观察者
        public virtual void RegisterObserver(string notificationName, IObserver observer)
        {
            lock (this.m_syncRoot)
            {
                //通过消息名判断观察者库里是否存在关注这条消息的观察者集合
                if (!this.m_observerMap.ContainsKey(notificationName))
                {
                    //不存在：开辟一个以这条消息名为键的空间
                    this.m_observerMap[notificationName] = new List<IObserver>();
                }
                //将消息名作为键观察者集合作为值存入
                this.m_observerMap[notificationName].Add(observer);
            }
            
        }
        //删除中介
        public virtual IMediator RemoveMediator(string mediatorName)
        {
            //中介
            IMediator notifyContext = null;
            lock (this.m_syncRoot)
            {
                //判断是否存在该中介
                if (!this.m_mediatorMap.ContainsKey(mediatorName))
                {
                    return null;
                }
                //获取到该中介
                notifyContext = this.m_mediatorMap[mediatorName];
                //获取中介身上存在的消息
                IList<string> list = notifyContext.ListNotificationInterests();
                //遍历消息集合
                for (int i = 0; i < list.Count; i++)
                {
                    //删除对应观察者
                    this.RemoveObserver(list[i], notifyContext);
                }
                //删除中介库里的所传名字所对应的中介
                this.m_mediatorMap.Remove(mediatorName);
            }
            if (notifyContext != null)
            {
                notifyContext.OnRemove();
            }
            return notifyContext;
        }
        //删除观察者
        public virtual void RemoveObserver(string notificationName, object notifyContext)
        {
            lock (this.m_syncRoot)
            {
                //查找观察者库里使否存在关注这条消息的观察者
                if (this.m_observerMap.ContainsKey(notificationName))
                {
                    //将关注这条消息的观察者集合拿出来
                    IList<IObserver> list = this.m_observerMap[notificationName];
                    //遍历这个观察者集合
                    for (int i = 0; i < list.Count; i++)
                    {
                        //执行Observer里的方法判断和当前观察者所存的消息实体（中介）是否一样
                        if (list[i].CompareNotifyContext(notifyContext))
                        {
                            //如果相同将观察者删除
                            list.RemoveAt(i);
                            break;
                        }
                    }
                    //如果所有观察者全被删除
                    if (list.Count == 0)
                    {
                        //在观察者字典里将侦听这条消息的观察者集合删除
                        this.m_observerMap.Remove(notificationName);
                    }
                }
            }
        }
        //查找中介
        public virtual IMediator RetrieveMediator(string mediatorName)
        {
            lock (this.m_syncRoot)
            {
                //判断中介库里是否存在该中介
                if (!this.m_mediatorMap.ContainsKey(mediatorName))
                {
                    return null;
                }
                //返回
                return this.m_mediatorMap[mediatorName];
            }
        }
        //View层单例使其他类可以调用此类的方法
        public static IView Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new View();
                        }
                    }
                }
                return m_instance;
            }
        }
    }
}

