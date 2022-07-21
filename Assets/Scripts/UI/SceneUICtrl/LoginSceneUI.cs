using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂在MainBG上的登录场景UI
/// </summary>
public class LoginSceneUI : UISceneBase
{
    
    protected override void OnStart()
    {
        base.OnStart();
        StartCoroutine(OpenLogInWindow());
        //GameObject obj = WindowUIMgr.Instance.LoadWindow(WindowUIMgr.WindowUIType.LogIn,showStyle:WindowUIMgr.WindowShowStyle.CenterToBig);
    }
    private IEnumerator OpenLogInWindow()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject obj = WindowUIMgr.Instance.LoadWindow(WindowUIType.LogIn);
    }
    
}
