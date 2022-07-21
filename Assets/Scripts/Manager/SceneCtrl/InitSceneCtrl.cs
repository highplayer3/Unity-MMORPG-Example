using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSceneCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadInit());
    }

   private IEnumerator LoadInit()
    {
        yield return new WaitForSeconds(5f);
        SceneMgr.Instance.LoadToLogIn();
    }
}
