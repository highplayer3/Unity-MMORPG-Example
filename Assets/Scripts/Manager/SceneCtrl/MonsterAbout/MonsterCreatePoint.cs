using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 刷怪点
/// </summary>
public class MonsterCreatePoint : MonoBehaviour
{
    /// <summary>
    /// 最大刷怪数量
    /// </summary>
    [SerializeField]
    private int m_MaxCount;
    /// <summary>
    /// 当前数量
    /// </summary>
    private int m_CurCount;
    /// <summary>
    /// 怪物预设的名称
    /// </summary>
    [SerializeField]
    private string monsterName;

    private float m_PrevCreateTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurCount < m_MaxCount)
        {
            if (Time.time > m_PrevCreateTime + UnityEngine.Random.Range(1.5f, 3.5f))
            {
                m_PrevCreateTime = Time.time;

                //创建怪
                GameObject objClone=RoleMgr.Instance.LoadRole(monsterName, RoleType.Monster);
                objClone.transform.SetParent(this.transform);
                objClone.transform.position = transform.TransformPoint(new Vector3(UnityEngine.Random.Range(-0.6f, 0.6f), 0, UnityEngine.Random.Range(-0.6f, 0.6f)));
                RoleCtrl roleCtrl = objClone.GetComponent<RoleCtrl>();
                roleCtrl.BornPoint = objClone.transform.position;
                
                RoleInfoMonster roleInfoMonster = new RoleInfoMonster();
                roleInfoMonster.RoleID = 1;
                roleInfoMonster.RoleServerID = DateTime.Now.Ticks;
                roleInfoMonster.CurHP = roleInfoMonster.MaxHP = 1000;
                roleInfoMonster.NickName = "超级丧尸";

                roleCtrl.Init(RoleType.Monster, roleInfoMonster, new RoleMonsterAI(roleCtrl));

                roleCtrl.OnRoleDeath = RoleDeath;
                m_CurCount++;
            }
        }
    }

    private void RoleDeath(RoleCtrl obj)
    {
        m_CurCount--;
        Destroy(obj.gameObject);
    }
}
