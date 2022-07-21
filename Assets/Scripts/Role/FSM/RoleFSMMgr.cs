using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleFSMMgr 
{
    public RoleCtrl CurRoleCtrl { get; private set; }
    /// <summary>
    /// 当前角色状态枚举
    /// </summary>
    public  RoleState CurRoleStateEnum { get; private set; }
    /// <summary>
    /// 当前角色状态
    /// </summary>
    private RoleStateAbstract m_curRoleState = null;

    private Dictionary<RoleState, RoleStateAbstract> m_roleStateDic;
    /// <summary>
    /// 构造函数
    /// </summary>
    public RoleFSMMgr(RoleCtrl curRoleCtrl)
    {
        CurRoleCtrl = curRoleCtrl;
        m_roleStateDic = new Dictionary<RoleState, RoleStateAbstract>();
        m_roleStateDic[RoleState.Idle] = new RoleStateIdle(this);
        m_roleStateDic[RoleState.Run] = new RoleStateRun(this);
        m_roleStateDic[RoleState.Attack] = new RoleStateAttack(this);
        m_roleStateDic[RoleState.Hurt] = new RoleStateHurt(this);
        m_roleStateDic[RoleState.Death] = new RoleStateDeath(this);

        if (m_roleStateDic.ContainsKey(CurRoleStateEnum))
        {
            m_curRoleState = m_roleStateDic[CurRoleStateEnum];
        }
    }
    /// <summary>
    /// 每帧执行的
    /// </summary>
    public void OnUpdate()
    {
        if (m_curRoleState != null)
        {
            m_curRoleState.OnUpdate();
        }
    }
    public void ChangeState(RoleState newState)
    {
        if (CurRoleStateEnum == newState) return;//状态一样就不用切换
        //调用以前状态的离开方法
        if (m_curRoleState != null)
        {
            m_curRoleState.OnLeave();
        }
        //更改当前状态枚举
        CurRoleStateEnum = newState;
        //更改当前状态
        m_curRoleState = m_roleStateDic[newState];
        //调用新状态的进入方法
        m_curRoleState.OnEnter();
    }
}
