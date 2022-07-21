using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInit : MonoBehaviour
{
    #region 常量
    public const string MMO_NICKNAME = "MMO_NICKNAME";
    public const string MMO_PWD = "MMO_PWD";
    #endregion

    public static GlobalInit Instance;
    [HideInInspector]
    public string curRoleNickName;//玩家注册时的名字，在注册控制器中赋值
    [HideInInspector]
    public RoleCtrl curPlayer;//当前玩家

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
