using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 怪物的AI
/// </summary>
public class RoleMonsterAI : IRoleAI
{
    /// <summary>
    /// 当前AI的角色控制器
    /// </summary>
    public RoleCtrl curRole { get; set; }

    /// <summary>
    /// 下次巡逻时间
    /// </summary>
    private float m_NextPatrolTime = 0f;
    /// <summary>
    /// 攻击间隔
    /// </summary>
    private float m_NextAttackTime = 0f;

    public RoleMonsterAI(RoleCtrl roleCtrl)
    {
        curRole = roleCtrl;
    }
    public void DoAI()
    {
        //死亡就直接返回
        if (curRole.curRoleFSMMgr.CurRoleStateEnum == RoleState.Death)
        {
            return;
        }
        //如果没有发现敌人
        if (curRole.LockEnemy == null)
        {
            //如果是待机状态
            if (curRole.curRoleFSMMgr.CurRoleStateEnum == RoleState.Idle)
            {
                //巡逻
                if (Time.time > m_NextPatrolTime + Random.Range(6f, 12f))
                {
                    m_NextPatrolTime = Time.time;
                    curRole.MoveTo(new Vector3(curRole.BornPoint.x + Random.Range(curRole.PatrolRange * -1, curRole.PatrolRange), curRole.BornPoint.y, curRole.BornPoint.z + Random.Range(curRole.PatrolRange * -1, curRole.PatrolRange)));
                }
            }
            //搜索附近的敌人
            //不利于性能优化,因为怪物的敌人只有主角Collider[] colliderArr = Physics.OverlapSphere(curRole.transform.position, curRole.ViewRange, 1 << LayerMask.NameToLayer("Role"));
            if (Vector3.Distance(curRole.transform.position, GlobalInit.Instance.curPlayer.transform.position) <= curRole.ViewRange)
            {
                curRole.LockEnemy = GlobalInit.Instance.curPlayer;
            }
        }
        else
        {
            if (curRole.LockEnemy.curRoleInfo.CurHP <= 0)
            {
                curRole.LockEnemy = null;
                return;
            }
            //丢失了敌人
            if(Vector3.Distance(curRole.transform.position, GlobalInit.Instance.curPlayer.transform.position) > curRole.ViewRange)
            {
                curRole.LockEnemy = null;
                return;
            }

            //没有丢失就判断是否在攻击范围里
            if(Vector3.Distance(curRole.transform.position, GlobalInit.Instance.curPlayer.transform.position) <= curRole.AttackRange)
            {
                //攻击
                if (Time.time > m_NextAttackTime&&curRole.curRoleFSMMgr.CurRoleStateEnum!=RoleState.Attack)
                {
                    m_NextAttackTime = Time.time + 2f;
                    curRole.ToAttack();
                }
                
            }
            else
            {
                if (curRole.curRoleFSMMgr.CurRoleStateEnum == RoleState.Idle)
                {
                    curRole.MoveTo(new Vector3(curRole.LockEnemy.transform.position.x + Random.Range(curRole.AttackRange * -1, curRole.AttackRange), curRole.LockEnemy.transform.position.y, curRole.LockEnemy.transform.position.z + Random.Range(curRole.AttackRange * -1, curRole.AttackRange)));
                }
            }
            
        }
    }
}
