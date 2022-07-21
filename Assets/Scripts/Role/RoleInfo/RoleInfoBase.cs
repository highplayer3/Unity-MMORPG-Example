using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色信息基类
/// </summary>
public class RoleInfoBase 
{
    /// <summary>
    /// 角色服务器ID
    /// </summary>
    public long RoleServerID;
    /// <summary>
    /// 玩家编号
    /// </summary>
    public int RoleID;
    /// <summary>
    /// 角色昵称
    /// </summary>
    public string NickName;
    /// <summary>
    /// 血量信息
    /// </summary>
    public int MaxHP;
    public int CurHP;
}
