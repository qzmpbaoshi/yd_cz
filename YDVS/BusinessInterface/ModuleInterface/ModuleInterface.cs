using System;
using System.Windows;
using SystemModel;

namespace ModuleInterface
{
    public interface ModuleInterface
    {
        /// <summary>
        /// 当前子模块的名称
        /// </summary>
        string GetControlCode();
        /// <summary>
        /// 当前子模块的名称
        /// </summary>
        string GetControlName();

        /// <summary>
        /// 返回当前子模块的主页面
        /// </summary>
        /// <param name="User">用户</param>
        /// <returns>返回当前子模块的主页面</returns>
        UIElement GetControl(User User);
        /// <summary>
        /// 关闭当前子模块事件
        /// </summary>
        event EventHandler CloseEvent;
    }
}
