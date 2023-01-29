using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject m_prefab;
    private List<GameObject> m_pool = new List<GameObject>();

    public void Init(GameObject prefab, int iCnt)
    {
        m_prefab = prefab;
        for (int i = 0; i < iCnt; ++i)
            m_pool.Add(GameObject.Instantiate(m_prefab));
    }

    public GameObject SpawnObject(Vector3 iPosition, Quaternion iRotation)
    {
        for (int i = 0; i < m_pool.Count; ++i)
        {
            if (!m_pool[i].activeInHierarchy)
            {
                m_pool[i].transform.position = iPosition;
                m_pool[i].transform.rotation = iRotation;
                m_pool[i].SetActive(true);
                return m_pool[i];
            }
        }
        var newObj = GameObject.Instantiate(m_prefab);
        m_pool.Add(newObj);
        return newObj;
    }

    public void ReturnToPool(GameObject iObject)
    {
        iObject.SetActive(false);
    }
}
