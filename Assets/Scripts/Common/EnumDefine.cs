using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 场景类型
public enum SceneType
{
    Login,
    City
}
#endregion

#region WindowUIType窗口类型
public enum WindowUIType
{
    None,
    /// <summary>
    /// 登录窗口
    /// </summary>
    LogIn,
    /// <summary>
    /// 注册窗口
    /// </summary>
    Register,
    /// <summary>
    /// 角色信息窗口
    /// </summary>
    RoleInfo

}
#endregion

#region WindowUIContainerType窗口容器类型
public enum WindowUIContainerType
{
    TL,
    TR,
    BL,
    BR,
    Center
}
#endregion

#region 窗口的打开方式
public enum WindowShowStyle
{
    /// <summary>
    /// 正常打开
    /// </summary>
    Normal,
    /// <summary>
    /// 从中间放大
    /// </summary>
    CenterToBig,
    /// <summary>
    /// 从上往下
    /// </summary>
    FromTop,
    /// <summary>
    /// 从下往上
    /// </summary>
    FromDown,
    /// <summary>
    /// 从左向右
    /// </summary>
    FromLeft,
    /// <summary>
    /// 从右向左
    /// </summary>
    FromRight
}
#endregion

#region RoleType 角色类型
public enum RoleType
{
    /// <summary>
    /// 未设置

    /// </summary>
    None=0,
    /// <summary>
    /// 当前角色
    /// </summary>
    MainPlayer=1,
    /// <summary>
    /// 怪物
    /// </summary>
    Monster=2
}
#endregion

public enum RoleState
{
    None=0,//未设置
    Idle=1,//待机
    Run=2,//跑
    Attack=3,//攻击
    Hurt=4,//受伤
    Death=5//死亡
}
/// <summary>
/// 角色动画名称
/// </summary>
public enum RoleAnimationName
{
    Idle_Normal,
    Idle_Fight,
    Run,
    Hurt,
    Death,
    PhyAttack1,
    PhyAttack2,
    PhyAttack3
}
public enum ToAnimatorCondition
{
    ToIdleNormal,
    ToIdleFight,
    ToRun,
    ToHurt,
    ToDead,
    ToPhyAttack,
    CurState
}