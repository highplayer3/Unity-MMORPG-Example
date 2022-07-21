using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public static CameraCtrl instance;
    [SerializeField]
    private Transform m_cameraUpAndDown;//摄像机上下移动
    [SerializeField]
    private Transform m_cameraZoomContainer;//摄像机远离和靠近
    [SerializeField]
    private Transform m_cameraContainer;//摄像机容器
    [SerializeField]
    private float m_rotateSpeed = 20f;//相机旋转速度
    [SerializeField]
    private float m_upanddownSpeed = 15f;//相机上下旋转速度
    [SerializeField]
    private float m_zoomCoefficient = 10f;//相机缩放系数

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
            

    }
    void Start()
    {
        
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        m_cameraUpAndDown.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_cameraUpAndDown.transform.localEulerAngles.z, 10, 80));
    }
    /// <summary>
    /// 设置摄像机旋转
    /// </summary>
    /// <param name="type">0=左，1=右</param>
    public void SetCameraRotate(int type)
    {
        transform.Rotate(0, m_rotateSpeed * Time.deltaTime*(type==0?1:-1), 0);
    }
    /// <summary>
    /// 设置摄像机上下
    /// </summary>
    /// <param name="type">0=上，1=下</param>
    public void SetCameraUpAndDown(int type)
    {
        m_cameraUpAndDown.transform.Rotate(0, 0, m_upanddownSpeed * Time.deltaTime * (type == 0 ? 1 :-1));
        m_cameraUpAndDown.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_cameraUpAndDown.transform.localEulerAngles.z, 10, 80));
    }
    /// <summary>
    /// 设置摄像机缩放
    /// </summary>
    /// <param name="type">0=拉近，1=拉远</param>
    public void SetCaameraZoom(int type)
    {
        m_cameraContainer.Translate(Vector3.right * Time.deltaTime * m_zoomCoefficient * (type == 0 ? 1 : -1));
        m_cameraContainer.localPosition = new Vector3(Mathf.Clamp(m_cameraContainer.localPosition.x,-6f,1f), 0, 0);
    }
    /// <summary>
    /// 实时看向角色
    /// </summary>
    /// <param name="pos"></param>
    public void AutoLookAt(Vector3 pos)
    {
        m_cameraZoomContainer.LookAt(pos);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 15f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 14f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 12f);
    }
}
