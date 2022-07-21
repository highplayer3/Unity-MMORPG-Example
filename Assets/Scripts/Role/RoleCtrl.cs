using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoleCtrl : MonoBehaviour
{
    #region 成员变量
    [HideInInspector]
    public Vector3 TargetPos = Vector3.zero;//移动的目标点
    public float Speed = 10f;//移动速度,让其能够序列化,方便在Inspector窗口修改
    [HideInInspector]
    public CharacterController m_characterController;
    private Vector3 motion;
    
    //private bool isRotationOver = false;
    //[SerializeField]
    //private float m_sightRadius = 3f;
    [HideInInspector]
    public Animator Animator;
    /// <summary>
    /// 昵称挂点
    /// </summary>
    [SerializeField]
    private Transform m_HeadBarPos;
    /// <summary>
    /// 头顶UI
    /// </summary>
    private GameObject m_HeadBar;

    /// <summary>
    /// 当前角色类型
    /// </summary>
    public RoleType curRoleType = RoleType.None;
    /// <summary>
    /// 当前角色信息
    /// </summary>
    public RoleInfoBase curRoleInfo = null;
    /// <summary>
    /// 当前角色AI
    /// </summary>
    public IRoleAI curRoleAI = null;
    /// <summary>
    /// 当前角色的有限状态机管理器
    /// </summary>
    public RoleFSMMgr curRoleFSMMgr = null;

    #region AI相关
    [HideInInspector]
    public Vector3 BornPoint;//出生点
    public float ViewRange;//视野范围
    public float PatrolRange;//巡逻范围
    public float AttackRange;//攻击范围
    /// <summary>
    /// 锁定的敌人
    /// </summary>
    [HideInInspector]
    public RoleCtrl LockEnemy;
    private RoleHeadBarCtrl roleHeadBarCtrl;
    /// <summary>
    /// 角色受伤委托
    /// </summary>
    public Action OnRoleHurt;
    /// <summary>
    /// 角色死亡委托
    /// </summary>
    public Action<RoleCtrl> OnRoleDeath;
    #endregion
    #endregion

    /// <summary>
    /// 角色初始化
    /// </summary>
    /// <param name="roleType">角色类型</param>
    /// <param name="roleInfoBase">角色信息</param>
    /// <param name="ai">AI</param>
    public void Init(RoleType roleType,RoleInfoBase roleInfo,IRoleAI ai)
    {
        curRoleType = roleType;
        curRoleInfo = roleInfo;
        curRoleAI = ai;
    }


    void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        m_characterController = GetComponent<CharacterController>();
        if (curRoleType == RoleType.MainPlayer)
        {
            if (CameraCtrl.instance != null)
            {
                CameraCtrl.instance.Init();
            }
        }
        curRoleFSMMgr = new RoleFSMMgr(this);//只实例化一次
        ToIdle();
        InitTitleBar();
    }

    void Update()
    {
        //保证角色有AI
        if (curRoleAI == null) return;
        curRoleAI.DoAI();

        if (curRoleFSMMgr != null)
        {
            curRoleFSMMgr.OnUpdate();
        }
        
        //让角色瞬间贴着地面
        if (!m_characterController.isGrounded)
        {
            Debug.Log("贴地");
            m_characterController.Move((transform.position + new Vector3(0, -1000, 0)) - transform.position);
        }
        //射线检测目标
        if (Input.GetMouseButtonUp(1))
        {
            /*单个物体检测，一条射线打中一个物体即停止，不会进行穿透;要想检测多个就:
             RaycastHit [] hitArr=Physics.RaycastAll(ray,Mathf.Infinity,1<<...);
             if(hitArr.Length>0){
                for(int i=0;i<...)
             }
             */
           
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;
           if(Physics.Raycast(ray,out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Item")))
           {
                BoxCtrl boxCtrl = hit.collider.gameObject.GetComponent<BoxCtrl>();
                if (boxCtrl != null)
                {
                    boxCtrl.Hit();
                }
           }

            /*
             Collider[] colliderArr = Physics.OverlapSphere(transform.position, m_sightRadius, 1 << LayerMask.NameToLayer("Monster"));
             if (colliderArr.Length > 0)
             {
                 for(int i = 0; i < colliderArr.Length; i++)
                 {
                     Debug.Log("找到了" + colliderArr[i].gameObject.name);
                 }
             }
             */
        }

        //#region 点击移动
        ///*射线检测代码*/
        //if (Input.GetMouseButtonUp(0)&&!EventSystem.current.IsPointerOverGameObject())
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hitInfo;//用于保存射线检测的信息
        //    if(Physics.Raycast(ray,out hitInfo))
        //    {
        //        if (hitInfo.collider.gameObject.CompareTag("Ground"))
        //        {
        //            Debug.Log(GlobalInit.Instance.curPlayer);
        //            if (GlobalInit.Instance.curPlayer != null)
        //            {
        //                GlobalInit.Instance.curPlayer.MoveTo(hitInfo.point);
        //            }
                    
        //        }
        //    }
        //}
        //#endregion

        //#region 动画切换
        //if (Input.GetKeyUp(KeyCode.R))
        //{
            
        //}
        //else if(Input.GetKeyUp(KeyCode.O))
        //{
        //    ToAttack();
        //}else if (Input.GetKeyUp(KeyCode.N))
        //{
        //    ToIdle();
        //}
        //else if (Input.GetKeyUp(KeyCode.P))
        //{
        //    ToDie();
        //}else if (Input.GetKeyUp(KeyCode.H))
        //{
        //    ToHurt();
        //}
        //#endregion

    }
    /// <summary>
    /// 初始化头顶UI
    /// </summary>
    private void InitTitleBar()
    {
        if (RoleHeadBarRoot.Instance == null) return;
        if (curRoleInfo == null) return;
        m_HeadBar = ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.UIOther, "RoleHeadBar");
        m_HeadBar.transform.SetParent(RoleHeadBarRoot.Instance.gameObject.transform);
        m_HeadBar.transform.localScale = new Vector3(0.76f, 0.76f, 0.76f);
        m_HeadBar.transform.localPosition = Vector3.zero;
        roleHeadBarCtrl = m_HeadBar.GetComponent<RoleHeadBarCtrl>();

        //给预设赋值
        roleHeadBarCtrl.Init(m_HeadBarPos, curRoleInfo.NickName,isShowHPBar:(curRoleType==RoleType.MainPlayer?false:true));
    }
    #region 控制角色方法
    public void MoveTo(Vector3 targetPos)
    {
        if (targetPos == Vector3.zero)
        {
            return;
        }
        TargetPos = targetPos;
        curRoleFSMMgr.ChangeState(RoleState.Run);
    }
    public void ToIdle()
    {
        curRoleFSMMgr.ChangeState(RoleState.Idle);
    }
    public void ToAttack()
    {
        if (LockEnemy == null)
        {
            return;
        }
        
        curRoleFSMMgr.ChangeState(RoleState.Attack);
        //Debug.Log(curRoleInfo.NickName+"发起了攻击");
        //暂时写的
        LockEnemy.ToHurt(100, 0.5f);
    }
    public void ToDie()
    {
        curRoleFSMMgr.ChangeState(RoleState.Death);
    }
    /// <summary>
    /// 攻击
    /// </summary>
    /// <param name="attackValue">伤害</param>
    /// <param name="delay">攻击不是立马就造成伤害，比如一段动画过后再扣血</param>
    public void ToHurt(int attackValue,float delay)
    {
        StartCoroutine(ToHurtCoroutine(attackValue, delay));
    }
    private IEnumerator ToHurtCoroutine(int attackValue, float delay)
    {
        yield return new WaitForSeconds(delay);
        //计算得出伤害
        int hurt = (int)(attackValue * UnityEngine.Random.Range(0.5f, 1f));

        if (OnRoleHurt != null)
        {
            OnRoleHurt();//委托不为空就执行这个委托
        }
        //TODO:伤害文字HUD
        
        curRoleInfo.CurHP -= hurt;
        roleHeadBarCtrl.Hurt(hurt, (float)curRoleInfo.CurHP / curRoleInfo.MaxHP);
        //Debug.Log(curRoleInfo.NickName + "受到伤害");
        if (curRoleInfo.CurHP <= 0)
        {
            curRoleFSMMgr.ChangeState(RoleState.Death);
        }
        else
        {
            curRoleFSMMgr.ChangeState(RoleState.Hurt);
        }
    }
    #endregion
    private void OnDestroy()
    {
        if (m_HeadBar != null)
        {
            Destroy(m_HeadBar);
        }
    }
    private void LateUpdate()
    {
        if (curRoleType == RoleType.MainPlayer)
        {
            CameraAutoFollow();
        }
            
    }

    #region CameraAutoFollow 摄像机自动跟随
    /// <summary>
    /// 摄像机自动跟随
    /// </summary>
    void CameraAutoFollow()
    {
        if (CameraCtrl.instance == null) return;
        CameraCtrl.instance.transform.position = gameObject.transform.position;
        //CameraCtrl.instance.AutoLookAt(gameObject.transform.position);
        if (Input.GetKey(KeyCode.A))
        {
            CameraCtrl.instance.SetCameraRotate(0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CameraCtrl.instance.SetCameraRotate(1);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            CameraCtrl.instance.SetCameraUpAndDown(0);
        }else if (Input.GetKey(KeyCode.S))
        {
            CameraCtrl.instance.SetCameraUpAndDown(1);
        }else if (Input.GetKey(KeyCode.I))
        {
            CameraCtrl.instance.SetCaameraZoom(0);
        }else if (Input.GetKey(KeyCode.L))
        {
            CameraCtrl.instance.SetCaameraZoom(1);
        }
    }
    #endregion

}
