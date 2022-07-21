using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loading场景的控制器
/// </summary>
public class LoadingSceneCtrl : MonoBehaviour
{
    /// <summary>
    /// 当前场景下的UI控制器
    /// </summary>
    [SerializeField]
    private UISceneLoadingCtrl m_uISceneLoadingCtrl;

    private AsyncOperation m_asyncOperation = null;

    private int m_CurrentProgress = 0;
    // Start is called before the first frame update
    void Start()
    {
        //m_uISceneLoadingCtrl.SetSliderValue(0);
        StartCoroutine(LoadingScene());
    }
    private IEnumerator LoadingScene()
    {
        string strSceneName = string.Empty;
        switch (SceneMgr.Instance.CurrentSceneType)
        {
            case SceneType.Login:
                strSceneName = "Scene_Login";
                break;
            case SceneType.City:
                strSceneName = "GameScene_Cemetery";
                break;
        }
        m_asyncOperation = SceneManager.LoadSceneAsync(strSceneName);
        m_asyncOperation.allowSceneActivation = false;
        yield return m_asyncOperation;
    }
    // Update is called once per frame
    void Update()
    {
        int toProgress = 0;
        if (m_asyncOperation.progress < 0.9f)
        {
            toProgress = (int)m_asyncOperation.progress * 100;//百分比显示
        }
        else
        {
            toProgress = 100;
        }
        if (m_CurrentProgress < toProgress)
        {
            m_CurrentProgress++;
        }
        else
        {
            m_asyncOperation.allowSceneActivation = true;
        }
        m_uISceneLoadingCtrl.SetSliderValue(m_CurrentProgress * 0.01f);
    }
}
