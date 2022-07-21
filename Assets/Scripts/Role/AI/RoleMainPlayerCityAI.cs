using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 主角主城的AI
/// </summary>
public class RoleMainPlayerCityAI : IRoleAI
{
    public RoleCtrl curRole { get; set; }

    public RoleMainPlayerCityAI(RoleCtrl roleCtrl)
    {
        curRole = roleCtrl;
    }
    public void DoAI()
    {
        //1、如果主角有锁定敌人，就攻击
        if (curRole.LockEnemy != null)
        {
            if (curRole.LockEnemy.curRoleInfo.CurHP <= 0)
            {
                curRole.LockEnemy = null;
                return;
            }
            if(curRole.curRoleFSMMgr.CurRoleStateEnum != RoleState.Attack)
            {
                curRole.ToAttack();
            }
            
        }
    }
}
