using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色状态的抽象基类
/// </summary>
public abstract class RoleStateAbstract 
{
    public RoleFSMMgr CurRoleFSMMgr { get; private set; }

    public AnimatorStateInfo CurRoleAnimatorStateInfo { get; set; }
    public RoleStateAbstract(RoleFSMMgr roleFSMMgr)
    {
        this.CurRoleFSMMgr = roleFSMMgr;
    }
    //对于一个状态，有离开，进入，执行三种行为
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnLeave() { }
}
