using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPooler : MonoBehaviour
{
    private GameObject m_prefab;
    public int poolSize = 10;
    private List<GameObject> objects;

    [SerializeField] private GameObject SlingShot;
    [SerializeField] private float SpawnHeight = -1.0f;
    [SerializeField] private float MaxXSpawnValue = 50.0f;

    void Start()
    {
        m_prefab = GameplayManager.Instance.GameDatabase.TargetPrefab;
        objects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(m_prefab);
            obj.SetActive(false);
            objects.Add(obj);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject obj = GetPooledObject();
            var randomPos = new Vector3(Random.Range(SlingShot.transform.position.x, MaxXSpawnValue), SpawnHeight, 0.0f);
            if (obj == null) return;
            obj.transform.position = randomPos;
            obj.SetActive(true);
        }
    }

    GameObject GetPooledObject()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].activeInHierarchy)
            {
                return objects[i];
            }
        }
        var newObj = GameObject.Instantiate(m_prefab);
        objects.Add(newObj);
        return newObj;
    }
}
