using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 攻击状态
/// </summary>
public class RoleStateAttack : RoleStateAbstract
{

    public RoleStateAttack(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }
    /// <summary>
    ///实现基类的进入状态方法
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        this.CurRoleFSMMgr.CurRoleCtrl.Animator.SetInteger(ToAnimatorCondition.ToPhyAttack.ToString(), 1);
        if (CurRoleFSMMgr.CurRoleCtrl.LockEnemy != null)
        {
            this.CurRoleFSMMgr.CurRoleCtrl.transform.LookAt(new Vector3(CurRoleFSMMgr.CurRoleCtrl.LockEnemy.transform.position.x, CurRoleFSMMgr.CurRoleCtrl.transform.position.y, CurRoleFSMMgr.CurRoleCtrl.LockEnemy.transform.position.z));
        }
    }
    /// <summary>
    /// 实现基类执行状态的方法
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        CurRoleAnimatorStateInfo = CurRoleFSMMgr.CurRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurRoleAnimatorStateInfo.IsName(RoleAnimationName.PhyAttack1.ToString()))
        {
            CurRoleFSMMgr.CurRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurState.ToString(), (int)RoleState.Attack);
            //动画执行了一遍就切换到待机
            if (CurRoleAnimatorStateInfo.normalizedTime > 1f)
            {
                CurRoleFSMMgr.CurRoleCtrl.ToIdle();
            }
        }
        else
        {
            CurRoleFSMMgr.CurRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurState.ToString(), 0);
        }
    }
    /// <summary>
    /// 实现基类离开状态方法
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        this.CurRoleFSMMgr.CurRoleCtrl.Animator.SetInteger(ToAnimatorCondition.ToPhyAttack.ToString(), 0);
    }
}
