using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Loading场景UI控制器
/// </summary>
public class UISceneLoadingCtrl : UISceneBase
{
    /// <summary>
    /// 进度条
    /// </summary>
    [SerializeField]
    private Slider m_loadingBar;
    /// <summary>
    /// 进度条上的文本
    /// </summary>
    [SerializeField]
    private Text m_loadingText;

    /// <summary>
    /// 设置进度条的值
    /// </summary>
    /// <param name="value"></param>
    public void SetSliderValue(float value)
    {
        m_loadingBar.value = value;
        m_loadingText.text = string.Format("{0}%", (int)(value * 100));
    }
}
