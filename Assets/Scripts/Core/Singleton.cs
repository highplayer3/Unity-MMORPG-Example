using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : IDisposable where T:new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    public virtual void Dispose()//执行与释放或重置非托管资源相关的应用程序定义的任务
    {
        
    }
}
