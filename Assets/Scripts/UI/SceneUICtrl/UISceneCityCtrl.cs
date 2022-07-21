using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 主城UI控制器
/// </summary>
public class UISceneCityCtrl : UISceneBase
{
    protected override void OnBtnClick()
    {
        GameObject _click = EventSystem.current.currentSelectedGameObject;
        switch (_click.name)
        {
            case "BtnHeadImage":
                OpenRoleInfo();
                break;
        }

    }

    private void OpenRoleInfo()
    {
        WindowUIMgr.Instance.LoadWindow(WindowUIType.RoleInfo);
    }
}
