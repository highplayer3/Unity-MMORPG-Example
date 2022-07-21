using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowUIMgr : Singleton<WindowUIMgr>
{
    private Dictionary<WindowUIType, UIWindowBase> dic = new Dictionary<WindowUIType, UIWindowBase>();
    
    #region 加载窗口
    public GameObject LoadWindow(WindowUIType type, WindowUIContainerType containerType = WindowUIContainerType.Center, WindowShowStyle showStyle = WindowShowStyle.Normal)
    {
        if (dic.ContainsKey(type)) return null;
        Transform transParent = null;
        GameObject obj = null;
        switch (type)
        {
            case WindowUIType.LogIn:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.UIWindows, "textLogin", cache: true);
                break;
            case WindowUIType.Register:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.UIWindows, "textRegister", cache: true);
                break;
            case WindowUIType.RoleInfo:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.UIWindows, "RoleInfoPage", cache: true);
                break;
        }
        if (obj == null) return null;
        UIWindowBase windowBase = obj.GetComponent<UIWindowBase>();
        if (windowBase == null) return null;
        dic.Add(type, windowBase);
        windowBase.CurrentUIType = type;
        switch (windowBase.containerType) 
        {
            case WindowUIContainerType.Center:
                transParent = SceneUIMgr.Instance.CurrentUIScene.Container_center;
                break;
        }
        obj.transform.parent = transParent;
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(false);
        StartShowWindow(windowBase, true);
        return obj;
    }
    #endregion

    private void StartShowWindow(UIWindowBase windowBase,bool isOpen)
    {
        switch (windowBase.showStyle)
        {
            case WindowShowStyle.Normal:
                ShowNormal(windowBase, isOpen);
                break;
            case WindowShowStyle.CenterToBig:
                ShowCenterToBig(windowBase, isOpen);
                break;
            case WindowShowStyle.FromTop:
                break;
            case WindowShowStyle.FromDown:
                break;
            case WindowShowStyle.FromLeft:
                break;
            case WindowShowStyle.FromRight:
                break;
        }
    }

    #region 各种打开效果
    /// <summary>
    /// 正常打开
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="isOpen"></param>
    private void ShowNormal(UIWindowBase windowBase,bool isOpen)
    {
        if (isOpen)
        {
            windowBase.gameObject.SetActive(true);
        }
        else
        {
            DestoryWindow(windowBase);
        }
    }
    /// <summary>
    /// 中间开始变大
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="isOpen"></param>
    private void ShowCenterToBig(UIWindowBase windowBase, bool isOpen)
    {
        if (isOpen)
        {
            iTween.ScaleFrom(windowBase.gameObject, Vector3.zero, 2f);
            //TweenScale ts=obj.GetComponent<>
            windowBase.gameObject.SetActive(true);
        }
        else
        {
            DestoryWindow(windowBase);
        }
    }
    #endregion

    public void CloseWindow(WindowUIType type)
    {
        if (dic.ContainsKey(type))
        {
            StartShowWindow(dic[type],false);
        }
    }

    private void DestoryWindow(UIWindowBase windowBase)
    {
        GameObject.Destroy(windowBase.gameObject);
        dic.Remove(windowBase.CurrentUIType);
    }
}

