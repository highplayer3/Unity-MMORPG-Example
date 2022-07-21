 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 角色信息窗口控制器
/// </summary>
public class UIRoleInfoCtrl : UIWindowBase
{
    protected override void OnBtnClick()
    {
        GameObject _click = EventSystem.current.currentSelectedGameObject;
        switch (_click.name)
        {
            case "BtnClose":
                base.Close();
                break;
        }
    }
}
