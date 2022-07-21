using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInfo : MonoBehaviour
{
    /// <summary>
    /// 昵称
    /// </summary>
    [SerializeField]
    private Text nickName;

    /// <summary>
    /// 血量
    /// </summary>
    [SerializeField]
    private Image HP;

    public static UIPlayerInfo Instance;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (GlobalInit.Instance.curPlayer != null)
        {
            GlobalInit.Instance.curPlayer.OnRoleHurt = MainPlayerHurt;
        }
    }

    private void MainPlayerHurt()
    {
        HP.fillAmount = (float)GlobalInit.Instance.curPlayer.curRoleInfo.CurHP / GlobalInit.Instance.curPlayer.curRoleInfo.MaxHP;
    }

    public void SetPlayerInfo()
    {
        nickName.text = GlobalInit.Instance.curPlayer.curRoleInfo.NickName;

    }
}
