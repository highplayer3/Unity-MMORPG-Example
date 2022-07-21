using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CitySceneCtrl : MonoBehaviour
{
    /// <summary>
    /// 主角的出生点
    /// </summary>
    [SerializeField]
    private Transform m_PlayerBirthplace;

    private void Awake()
    {
        SceneUIMgr.Instance.LoadSceneUI(SceneUIMgr.SceneUIType.MainCity);
    }
    private void Start()
    {
        
        //加载玩家
        GameObject obj = RoleMgr.Instance.LoadRole("Role_MainPlayer",RoleType.MainPlayer);
        obj.transform.position = m_PlayerBirthplace.transform.position;
        //给当前玩家赋值
        GlobalInit.Instance.curPlayer = obj.GetComponent<RoleCtrl>();
        GlobalInit.Instance.curPlayer.Init(RoleType.MainPlayer,new RoleInfoBase() { NickName=GlobalInit.Instance.curRoleNickName,MaxHP=10000,CurHP=10000},new RoleMainPlayerCityAI(GlobalInit.Instance.curPlayer));
        UIPlayerInfo.Instance.SetPlayerInfo();
        
    }
    private void Update()
    {
        #region 点击移动
        /*射线检测代码*/
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;//用于保存射线检测的信息

            //去角色层检测是否点击了角色
            RaycastHit[] hitArr = Physics.RaycastAll(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("Role"));
            if (hitArr.Length > 0)
            {
                RoleCtrl hitRole = hitArr[0].collider.gameObject.GetComponent<RoleCtrl>();
                if (hitRole.curRoleType == RoleType.Monster)
                {
                    GlobalInit.Instance.curPlayer.LockEnemy = hitRole;
                }
            }
            else
            {
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.collider.gameObject.CompareTag("Ground"))
                    {
                        //Debug.Log(GlobalInit.Instance.curPlayer);
                        if (GlobalInit.Instance.curPlayer != null)
                        {
                            GlobalInit.Instance.curPlayer.LockEnemy = null;
                            GlobalInit.Instance.curPlayer.MoveTo(hitInfo.point);
                            //Debug.Log("点击");
                        }

                    }
                }
            }
            
            
        }
        #endregion
    }
}
