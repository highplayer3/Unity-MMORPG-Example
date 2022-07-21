using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 注册界面
/// </summary>
public class UIRegisterCtrl : UIWindowBase
{
    #region 面板信息
    [SerializeField]
    private InputField m_inputNickName;
    [SerializeField]
    private InputField m_inputPwd;
    [SerializeField]
    private InputField m_inputPwd2;
    [SerializeField]
    private Text remindText;
    #endregion

    #region 重写基类OnBtnClick
    protected override void OnBtnClick()
    {
        GameObject _click = EventSystem.current.currentSelectedGameObject;
        //Debug.Log(_click.name);
        switch (_click.name)
        {
            case "Login"://返回登录界面
                BtnToLogin();
                break;
            case "Register"://注册
                Reg();
                break;
        }
    }
    #endregion
    /// <summary>
    /// 跳转至登录窗口
    /// </summary>
    private void BtnToLogin()
    {
        Close();
        NextOpenWindow = WindowUIType.LogIn;
    }
    /// <summary>
    /// 注册
    /// </summary>
    private void Reg()
    {
        string nickName = m_inputNickName.text.Trim();
        string pwd = m_inputPwd.text.Trim();
        string pwd2 = m_inputPwd2.text.Trim();
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
        if (string.IsNullOrEmpty(pwd2))
        {
            this.remindText.text = "请确认输入密码";
            return;
        }
        if (pwd != pwd2)
        {
            this.remindText.text = "两次输入密码不一致";
            return;
        }
        
        PlayerPrefs.SetString(GlobalInit.MMO_NICKNAME, nickName);
        PlayerPrefs.SetString(GlobalInit.MMO_PWD, pwd);
        GlobalInit.Instance.curRoleNickName = nickName;

        SceneMgr.Instance.LoadToCity();
    }
}
