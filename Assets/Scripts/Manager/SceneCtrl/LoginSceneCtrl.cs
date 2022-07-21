using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登录的场景控制器
/// </summary>
public class LoginSceneCtrl : MonoBehaviour
{
    GameObject obj;
    private void Awake()
    {
        //obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourcesType.UIScenes,"MainBG1",cache:true);
        //直接加载特别麻烦，所以直接抽象出一个类,然后只需要写一句即可完成功能
        SceneUIMgr.Instance.LoadSceneUI(SceneUIMgr.SceneUIType.LogIn);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
