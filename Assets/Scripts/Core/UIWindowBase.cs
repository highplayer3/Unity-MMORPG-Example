using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有窗口UI的基类
/// </summary>
public class UIWindowBase : UIBase
{
    /// <summary>
    /// 挂点类型
    /// </summary>
    [SerializeField]
    public WindowUIContainerType containerType = WindowUIContainerType.Center;
    /// <summary>
    /// 打开方式
    /// </summary>
    [SerializeField]
    public WindowShowStyle showStyle = WindowShowStyle.Normal;

    /// <summary>
    /// 当前窗口类型
    /// </summary>
    [HideInInspector]
    public WindowUIType CurrentUIType;

    protected WindowUIType NextOpenWindow = WindowUIType.None;

    protected virtual void Close()
    {
        WindowUIMgr.Instance.CloseWindow(CurrentUIType);
    }
    /// <summary>
    /// 销毁之前执行的
    /// </summary>
    protected override void BeforeOnDestory()
    {
        if (NextOpenWindow == WindowUIType.None) return;
        WindowUIMgr.Instance.LoadWindow(NextOpenWindow);

    }
}
