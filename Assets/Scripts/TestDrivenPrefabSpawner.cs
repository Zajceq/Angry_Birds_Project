using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrivenPrefabSpawner : MonoBehaviour
{
    [SerializeField] private int SpawnedPrefabAmount;
    [SerializeField] private GameObject SlingShot;
    [SerializeField] private float SpawnHeight;
    [SerializeField] private float MaxXSpawnValue = 10.0f;
    [SerializeField] public string PrefabTag = "TargetFull";
    public GameSettingsDatabase GameDatabase;
    private List<GameObject> SpawnedPrefabList;


    private void Start()
    {
        SpawnedPrefabList = new List<GameObject>();

        for (int i = 0; i < SpawnedPrefabAmount; i++)
        {
            SpawnedPrefabList.Add(GameDatabase.TargetPrefab);
        }
        SpawnPrefabs();
    }

    public void SpawnPrefabs()
    {
        foreach (var prefab in SpawnedPrefabList)
        {
            Instantiate(prefab, new Vector3(Random.Range(SlingShot.transform.position.x, MaxXSpawnValue), SpawnHeight, 0.0f), Quaternion.identity);
        }
    }

}
