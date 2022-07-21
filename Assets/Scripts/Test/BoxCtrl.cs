 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 用于控制所有可以被点击拾取的物体
/// </summary>
public class BoxCtrl : MonoBehaviour
{
    public Action<GameObject> OnHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hit()
    {
        if (OnHit != null)
        {
            Debug.Log("触发委托");
            OnHit(gameObject);
            
        }
    }
}
