using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleHeadBarCtrl : MonoBehaviour
{
    [SerializeField]
    private Text textNickNmae;
    [SerializeField]
    private Image pbHP;
    /// <summary>
    /// 对齐的目标点
    /// </summary>
    private Transform m_taregt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null || m_taregt == null)
        {
            return;
        }
        //Camera.main.WorldToViewportPoint(m_taregt.position);
        gameObject.transform.position = Camera.main.WorldToScreenPoint(m_taregt.transform.position);
        
    }
    /// <summary>
    /// 初始化角色头顶UI
    /// </summary>
    /// <param name="target">显示的地方</param>
    /// <param name="nickName">昵称</param>
    /// <param name="isShow">是否显示,默认为不显示</param>
    public void Init(Transform target,string nickName,bool isShowHPBar=false)
    {
        m_taregt = target;
        textNickNmae.text = nickName;
        pbHP.gameObject.SetActive(isShowHPBar);
    }
    public void Hurt(int hurtValue,float pbHPValue=0)
    {
        pbHP.transform.Find("HP").GetComponent<Image>().fillAmount = pbHPValue;
    }
}
