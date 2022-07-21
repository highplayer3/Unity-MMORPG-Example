using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 所有UI的总基类
/// </summary>
public class UIBase : MonoBehaviour
{
    private void Awake()
    {
        OnAwake();
    }


    void Start()
    {
        Button[] btnArr = GetComponentsInChildren<Button>();
        for (int i = 0; i < btnArr.Length; i++)
        {
            btnArr[i].onClick.AddListener(BtnClick);
        }
        OnAwake();
        OnStart();
    }
    private void OnDestroy()
    {
        BeforeOnDestory();
    }
    private void BtnClick()
    {
        OnBtnClick();
    }
    protected virtual void OnAwake()
    {

    }
    protected virtual void OnStart()
    {

    }
    protected virtual void BeforeOnDestory()
    {

    }
    protected virtual void OnBtnClick()
    {

    }
}
