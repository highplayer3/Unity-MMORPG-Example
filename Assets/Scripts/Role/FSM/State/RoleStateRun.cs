using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 奔跑状态
/// </summary>
public class RoleStateRun : RoleStateAbstract
{
    private float m_RotationSpeed = 0.2f;
    private Quaternion m_TargetQuaternion;

    public RoleStateRun(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }
    /// <summary>
    ///实现基类的进入状态方法
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        //Debug.Log("进入Run");
        m_RotationSpeed = 0;
        this.CurRoleFSMMgr.CurRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToRun.ToString(), true);
    }
    /// <summary>
    /// 实现基类执行状态的方法
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        CurRoleAnimatorStateInfo = CurRoleFSMMgr.CurRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurRoleAnimatorStateInfo.IsName(RoleAnimationName.Run.ToString()))
        {
            CurRoleFSMMgr.CurRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurState.ToString(), (int)RoleState.Run);
        }
        else
        {
            CurRoleFSMMgr.CurRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurState.ToString(), 0);
        }

        /*角色控制部分代码*/
        if(Vector3.Distance(new Vector3(CurRoleFSMMgr.CurRoleCtrl.TargetPos.x,0,CurRoleFSMMgr.CurRoleCtrl.TargetPos.z), new Vector3(CurRoleFSMMgr.CurRoleCtrl.transform.position.x,0, CurRoleFSMMgr.CurRoleCtrl.transform.position.z)) > 0.2f)
        //if (Vector3.Distance(CurRoleFSMMgr.CurRoleCtrl.TargetPos, CurRoleFSMMgr.CurRoleCtrl.transform.position) > 0.1f)
        {
            //移动
            Vector3 motion = CurRoleFSMMgr.CurRoleCtrl.TargetPos - CurRoleFSMMgr.CurRoleCtrl.transform.position;

            motion = motion.normalized;//一定要归一化，不然那移动会很怪
            motion = motion * Time.deltaTime * CurRoleFSMMgr.CurRoleCtrl.Speed;
            motion.y = 0f;

            /*旋转 1：LookAt()方法;
                   2:利用四元数进行插值
            */
            if (m_RotationSpeed<=1f)
            {
                //Debug.Log("1");
                m_RotationSpeed += 10f*Time.deltaTime;
                m_TargetQuaternion = Quaternion.LookRotation(motion);
                CurRoleFSMMgr.CurRoleCtrl.transform.rotation = Quaternion.Lerp(CurRoleFSMMgr.CurRoleCtrl.transform.rotation, m_TargetQuaternion, m_RotationSpeed);
                if (Quaternion.Angle(CurRoleFSMMgr.CurRoleCtrl.transform.rotation, m_TargetQuaternion) < 1f)
                {
                    m_RotationSpeed = 0;
                }
            }
            CurRoleFSMMgr.CurRoleCtrl.m_characterController.Move(motion);
            
        }
        else
        {
            //Debug.Log("到了");
            CurRoleFSMMgr.CurRoleCtrl.ToIdle();
        }
    }
    /// <summary>
    /// 实现基类离开状态方法
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        this.CurRoleFSMMgr.CurRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToRun.ToString(), false);
    }
}
