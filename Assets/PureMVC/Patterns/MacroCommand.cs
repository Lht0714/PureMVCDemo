namespace PureMVC.Patterns
{
    using PureMVC.Interfaces;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// 控制层
    /// 1.管理Proxy与Mediator层，负责注册，查询获取，移除等。
    /// 2.直接调用多个Proxy，进行复杂业务逻辑处理。
    /// 3.对(继承MonoBehaviour)的脚本做动态管理与对象加载操作。
    /// 4.Command本身生命周期很短，在整个生命周期中并没有类的实例在运行，而是通过反射技术，一次性的得到类的对象(object),执行完(Execute)后结束。
    /// </summary>
    public class MacroCommand : Notifier, ICommand, INotifier
    {
        private IList<Type> m_subCommands = new List<Type>();

        public MacroCommand()
        {
            this.InitializeMacroCommand();
        }

        protected void AddSubCommand(Type commandType)
        {
            this.m_subCommands.Add(commandType);
        }

        public void Execute(INotification notification)
        {
            while (this.m_subCommands.Count > 0)
            {
                Type type = this.m_subCommands[0];
                object obj2 = Activator.CreateInstance(type);
                if (obj2 is ICommand)
                {
                    ((ICommand)obj2).Execute(notification);
                }
                this.m_subCommands.RemoveAt(0);
            }
        }

        protected virtual void InitializeMacroCommand()
        {
        }
    }
}

