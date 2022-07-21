using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    private Transform createArea;//用于生成预制体的区域
    [SerializeField]
    private Transform prefabParent;//用于管理生成的预制体

    public GameObject m_prefab;//预制体

    private int m_maxCount = 10;//最大数量
    private int m_currentCount = 0;//当前数量
    private const string m_boxKey = "BoxKey";
    private int m_PrevCount;//记录上一次拾取箱子的数量

    private float m_CreateTime = 0f;//每次刷新间隔时间
    // Start is called before the first frame update
    void Start()
    {
        m_PrevCount=PlayerPrefs.GetInt(m_boxKey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currentCount < m_maxCount)
        {
            if (Time.time > m_CreateTime)
            {
                m_CreateTime = Time.time + 3f;
                #region 克隆
                GameObject objClone = Instantiate(m_prefab) as GameObject;
                objClone.transform.SetParent(prefabParent);
                objClone.transform.position = createArea.transform.TransformPoint(new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
                BoxCtrl boxCtrl = objClone.GetComponent<BoxCtrl>();
                if (boxCtrl != null)
                {
                    m_currentCount++;
                    boxCtrl.OnHit = BoxHit;//给委托赋值
                }
                
                #endregion
            }
        }
    }
    private void BoxHit(GameObject obj)
    {
        m_currentCount--;
        m_PrevCount++;
        PlayerPrefs.SetInt(m_boxKey, m_PrevCount);
        Destroy(obj);
        Debug.Log("累计拾取了" + m_PrevCount);
    }
}
