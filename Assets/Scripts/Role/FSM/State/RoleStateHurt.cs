using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 受伤状态
/// </summary>
public class RoleStateHurt : RoleStateAbstract
{
    public RoleStateHurt(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }
    /// <summary>
    ///实现基类的进入状态方法
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        this.CurRoleFSMMgr.CurRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToHurt.ToString(), true);
    }
    /// <summary>
    /// 实现基类执行状态的方法
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        CurRoleAnimatorStateInfo = CurRoleFSMMgr.CurRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurRoleAnimatorStateInfo.IsName(RoleAnimationName.Hurt.ToString()))
        {
            CurRoleFSMMgr.CurRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurState.ToString(), (int)RoleState.Hurt);
            if (CurRoleAnimatorStateInfo.normalizedTime > 1f)
            {
                CurRoleFSMMgr.CurRoleCtrl.ToIdle();
            }
        }
    }
    /// <summary>
    /// 实现基类离开状态方法
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        this.CurRoleFSMMgr.CurRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToHurt.ToString(), false);

    }
}
