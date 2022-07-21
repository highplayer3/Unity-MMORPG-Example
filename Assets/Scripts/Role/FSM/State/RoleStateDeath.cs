using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 死亡状态
/// </summary>
public class RoleStateDeath :RoleStateAbstract
{
    public RoleStateDeath(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }
    /// <summary>
    ///实现基类的进入状态方法
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        this.CurRoleFSMMgr.CurRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToDead.ToString(), true);
    }
    /// <summary>
    /// 实现基类执行状态的方法
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        CurRoleAnimatorStateInfo = CurRoleFSMMgr.CurRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurRoleAnimatorStateInfo.IsName(RoleAnimationName.Death.ToString()))
        {
            CurRoleFSMMgr.CurRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurState.ToString(), (int)RoleState.Death);

            if (CurRoleAnimatorStateInfo.normalizedTime > 1)
            {
                CurRoleFSMMgr.CurRoleCtrl.OnRoleDeath(CurRoleFSMMgr.CurRoleCtrl);//执行死亡委托
            }
        }
    }
    /// <summary>
    /// 实现基类离开状态方法
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        this.CurRoleFSMMgr.CurRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToDead.ToString(), false);
    }
}
