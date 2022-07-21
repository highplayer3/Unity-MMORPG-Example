using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景UI管理器
/// </summary>
public class SceneUIMgr : Singleton<SceneUIMgr>
{
    #region 场景类型
    public enum SceneUIType
    {
        /// <summary>
        /// 登录
        /// </summary>
        LogIn,
        /// <summary>
        /// 加载
        /// </summary>
        Loading,
        /// <summary>
        /// 主城
        /// </summary>
        MainCity
    }
    #endregion
    /// <summary>
    /// 当前场景UI
    /// </summary>
    public UISceneBase CurrentUIScene;
    #region 加载场景UI
    public GameObject LoadSceneUI(SceneUIType type)
    {
        GameObject obj = null;
        switch (type)
        {
            case SceneUIType.LogIn:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.UIScenes, "MainPage");
                CurrentUIScene = obj.GetComponent<LoginSceneUI>();
                break;
            case SceneUIType.Loading:
                break;
            case SceneUIType.MainCity:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.UIScenes, "UI_Canvas_City");
                CurrentUIScene = obj.GetComponent<UISceneCityCtrl>();
                break;
        }
        return obj;
    }
    #endregion
}
