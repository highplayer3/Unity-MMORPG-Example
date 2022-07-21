using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleMgr : Singleton<RoleMgr>
{
    #region LoadRole 根据角色名称生成角色
    /// <summary>
    /// 根据角色预设名称克隆角色
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject LoadRole(string name,RoleType type)
    {
        string path = string.Empty;
        switch (type)
        {
            case RoleType.MainPlayer:
                path = "Player";
                break;
            case RoleType.Monster:
                path = "Monster";
                break;
        }
        return ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.Role, string.Format("{0}/{1}",path, name), cache: true);
    }
    #endregion


    //释放资源
    public override void Dispose()
    {
        base.Dispose();
    }
}
