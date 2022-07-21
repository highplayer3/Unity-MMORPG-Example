using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 资源管理器，用来管理全局资源
/// </summary>
public class ResourcesMgr:Singleton<ResourcesMgr>
{
    #region ResourcesType 资源类型

    /// <summary>
    /// 资源分类
    /// </summary>
    public enum ResourcesType
    {
        UIScenes,//场景UI
        UIWindows,//窗口UI
        Role,//角色
        Effect,//特效
        UIOther
    }
    #endregion

    private Hashtable m_PrefabTable;//缓存区

    public ResourcesMgr()
    {
        m_PrefabTable = new Hashtable();
    }

    #region Load 加载资源
    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="path">短路径</param>
    /// <param name="cache">是否放入缓存,默认为false,调用时不传就用默认的</param>
    /// <returns>预制体</returns>
    public GameObject Load(ResourcesType type,string path,bool cache=false)
    {
        GameObject obj = null;
        if (m_PrefabTable.Contains(path))
        {
            Debug.Log("资源从缓存中加载");
            obj = m_PrefabTable[path] as GameObject;
        }
        else
        {
            StringBuilder sbr = new StringBuilder();
            switch (type)
            {
                case ResourcesType.UIScenes:
                    sbr.Append("UIPrefabs/UIScenes/");
                    break;
                case ResourcesType.UIWindows:
                    sbr.Append("UIPrefabs/UIWindows/");
                    break;
                case ResourcesType.Role:
                    sbr.Append("RolePrefabs/");
                    break;
                case ResourcesType.Effect:
                    sbr.Append("EffectPrefabs/");
                    break;
                case ResourcesType.UIOther:
                    sbr.Append("UIPrefabs/UIOther/");
                    break;
            }
            sbr.Append(path);
            obj = Resources.Load(sbr.ToString()) as GameObject;//资源的镜像
            if (cache)
            {
                m_PrefabTable.Add(path, obj);
            }
        }
        

        return GameObject.Instantiate(obj);
    }
    #endregion

    #region 释放资源
    /// <summary>
    /// 释放资源
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();
        m_PrefabTable.Clear();
        Resources.UnloadUnusedAssets();
    }
    #endregion
}
