using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public ObjectPool TargetPool = new ObjectPool();
    [SerializeField] private GameObject SlingShot;
    [SerializeField] private float SpawnHeight;
    [SerializeField] private float MaxXSpawnValue = 10.0f;

    private void Start()
    {
        TargetPool.Init(GameplayManager.Instance.GameDatabase.TargetPrefab, 0);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            var randomPos = new Vector3(Random.Range(SlingShot.transform.position.x, MaxXSpawnValue), SpawnHeight, 0.0f);
            GameObject obj = TargetPool.SpawnObject(randomPos, Quaternion.identity);
        }
    }
}
