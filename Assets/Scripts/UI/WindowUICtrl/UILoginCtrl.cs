using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 登录窗口UI控制器
/// </summary>
public class UILoginCtrl : UIWindowBase
{
    /// <summary>
    /// 登录界面的信息
    /// </summary>
    [SerializeField]
    private InputField m_inputNickName;
    [SerializeField]
    private InputField m_inputPwd;
    [SerializeField]
    private Text remindText;

    #region OnBtnClick 重写基类方法
    protected override void OnBtnClick()
    {
        GameObject _click = EventSystem.current.currentSelectedGameObject;
        //Debug.Log(_click.name);
        switch (_click.name)
        {
            case "Login":
                BtnToLogin();
                break;
            case "Register":
                BtnToReg();
                break;
        }
    }

    private void BtnToLogin()
    {
        string nickName = this.m_inputNickName.text.Trim();
        string pwd = this.m_inputPwd.text.Trim();

        if (string.IsNullOrEmpty(nickName))
        {
            this.remindText.text = "请输入昵称";
            return;
        }
        if (string.IsNullOrEmpty(pwd))
        {
            this.remindText.text = "请输入密码";
            return;
        }
        //Debug.Log(nickName);
        //Debug.Log(pwd);
        string oldNickname=PlayerPrefs.GetString(GlobalInit.MMO_NICKNAME);
        string oldPwd = PlayerPrefs.GetString(GlobalInit.MMO_PWD);
        if (oldNickname != nickName || oldPwd != pwd)
        {
            remindText.text = "您输入的密码或昵称错误!";
            return;
        }
        GlobalInit.Instance.curRoleNickName = nickName;
        

        SceneMgr.Instance.LoadToCity();
    }
    #endregion

    /// <summary>
    /// 打开注册界面
    /// </summary>
    private void BtnToReg()
    {
        //Debug.Log("销毁上一个界面");
        //Destroy(gameObject);
        //GameObject obj=WindowUIMgr.Instance.LoadWindow(WindowUIType.Register);
        this.Close();
        NextOpenWindow = WindowUIType.Register;
    }
    
}
