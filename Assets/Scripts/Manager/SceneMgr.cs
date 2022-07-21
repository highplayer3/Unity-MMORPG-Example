using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 全局场景管理器
/// </summary>
public class SceneMgr : Singleton<SceneMgr>
{
    public SceneType CurrentSceneType
    {
        get;
        private set;
    }

    public void LoadToLogIn()
    {
        CurrentSceneType = SceneType.Login;
        SceneManager.LoadScene("Scene_Loading");
        //SceneManager.LoadScene("Login");
    }
    /// <summary>
    /// 去主场景
    /// </summary>
    public void LoadToCity()
    {
        CurrentSceneType = SceneType.City;
        SceneManager.LoadScene("Scene_Loading");
        //Application.LoadLevel("Free_Character_Demo_Skeleton");
        //SceneManager.LoadScene("Free_Character_Demo_Skeleton");
    }
}
