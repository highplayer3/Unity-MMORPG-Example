using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 待机状态
/// </summary>
public class RoleStateIdle : RoleStateAbstract
{
    public RoleStateIdle(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }
    /// <summary>
    ///实现基类的进入状态方法
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        this.CurRoleFSMMgr.CurRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToIdleNormal.ToString(),true);
    }
    /// <summary>
    /// 实现基类执行状态的方法
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        CurRoleAnimatorStateInfo = CurRoleFSMMgr.CurRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurRoleAnimatorStateInfo.IsName(RoleAnimationName.Idle_Normal.ToString()))
        {
            CurRoleFSMMgr.CurRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurState.ToString(), (int)RoleState.Idle);
        }
    }
    /// <summary>
    /// 实现基类离开状态方法
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        this.CurRoleFSMMgr.CurRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToIdleNormal.ToString(), false);
    }
}
