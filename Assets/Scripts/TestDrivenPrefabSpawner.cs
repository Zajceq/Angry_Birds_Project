using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrivenPrefabSpawner : MonoBehaviour
{
    [SerializeField] private GameObject SlingShot;
    [SerializeField] private int SpawnedPrefabAmount;
    [SerializeField] private float SpawnHeight;
    [SerializeField] private float MaxXSpawnValue = 10.0f;
    [SerializeField] private string PrefabTag = "TargetFull";
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

    private void SpawnPrefabs()
    {
        foreach (var prefab in SpawnedPrefabList)
        {
            Instantiate(prefab, new Vector3(Random.Range(SlingShot.transform.position.x, MaxXSpawnValue), SpawnHeight, 0.0f), Quaternion.identity);
        }
        FunctionalityTestFunction();
    }

    private bool FunctionalityTestFunction()
    {
        bool testResult =
        CheckIfThreePrefabsAreOnTheScene();
        //CheckSpawnedPrefabListCount()
        //&& CheckIfSlingshotIsNotNull()
        //&& CheckIfSpawnedPrefabIsNotNull()
        //&& CheckIfAllPrefabsWereSpawned()
        //&& CheckIfPrefabsAreOnTheRightSideOfTheSlingshot()
        //&& CheckSpawnedPrefabsHeight();
        Debug.Log(testResult);
        return testResult;
    }

    private bool CheckSpawnedPrefabListCount()
    {
        if (SpawnedPrefabList.Count == SpawnedPrefabAmount)
        {
            return true;
        }
        else
        {
            Debug.LogWarning($"You do not have {SpawnedPrefabAmount} prefabs to spawn");
            return false;
        }
    }

    private bool CheckIfSlingshotIsNotNull()
    {
        if (SlingShot != null)
        {
            return true;
        }
        else
        {
            Debug.LogError("Couldn't find Slingshot GameObject, please select it in the inspector");
            return false;
        }
    }

    private bool CheckIfSpawnedPrefabIsNotNull()
    {
        if (GameDatabase.TargetPrefab != null)
        {
            return true;
        }
        else
        {
            Debug.LogError("Couldn't find SpawnedPrefab GameObject, please select it in the inspector");
            return false;
        }
    }

    private bool CheckIfPrefabsAreOnTheRightSideOfTheSlingshot()
    {
        var spawnedGameObjects = GameObject.FindGameObjectsWithTag(PrefabTag);
        int SpawnedPrefabCounter = 0;
        foreach (var prefab in spawnedGameObjects)
        {
            if (prefab.gameObject.transform.position.x > SlingShot.transform.position.x)
            {
                SpawnedPrefabCounter += 1;
            }
        }
        if (SpawnedPrefabCounter == SpawnedPrefabAmount)
        {
            return true;
        }
        else
        {
            Debug.LogWarning("Some prefabs are spawned on the left side of the Slingshot, please check spawners");
            return false;
        }
    }

    private bool CheckIfAllPrefabsWereSpawned()
    {
        int numberOfPrefabsOnTheScene = GameObject.FindGameObjectsWithTag(PrefabTag).Length;
        if (numberOfPrefabsOnTheScene == SpawnedPrefabAmount)
        {
            return true;
        }
        else
        {
            Debug.LogWarning("Some prefabs were not spawned properly");
            return false;
        }
    }

    private bool CheckSpawnedPrefabsHeight()
    {
        var spawnedGameObjects = GameObject.FindGameObjectsWithTag(PrefabTag);
        int SpawnedPrefabCounter = 0;
        foreach (var prefab in spawnedGameObjects)
        {
            if (Mathf.Abs(prefab.gameObject.transform.position.y - SpawnHeight) < 0.5f)
            {
                SpawnedPrefabCounter += 1;
            }
        }
        if (SpawnedPrefabCounter == SpawnedPrefabAmount)
        {
            return true;
        }
        else
        {
            Debug.LogWarning("Some prefabs have wrong y position");
            return false;
        }
    }    

    private bool CheckIfThreePrefabsAreOnTheScene()
    {
        var spawnedGameObjects = GameObject.FindGameObjectsWithTag(PrefabTag);
        int SpawnedPrefabCounter = 0;
        foreach (var prefab in spawnedGameObjects)
        {
                SpawnedPrefabCounter += 1;
        }
        if (SpawnedPrefabCounter == 3)
        {
            return true;
        }
        else
        {
            Debug.LogWarning("There are more or less than 3 prefabs on the Scene");
            return false;
        }
    }    
}
