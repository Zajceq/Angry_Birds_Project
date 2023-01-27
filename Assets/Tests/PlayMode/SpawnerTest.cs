using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SpawnerTest
{
    [OneTimeSetUp]
    public void Init()
    {
        SceneManager.LoadScene("SampleScene");
    }

    [UnityTest]
    public IEnumerator CheckTagObjects()
    {
        var prefabSpawner = GameObject.FindObjectOfType<TestDrivenPrefabSpawner>();

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(prefabSpawner.PrefabTag);

        int expectedAmount = 3;
        int actualAmount = objectsWithTag.Length;

        Assert.AreEqual(expectedAmount, actualAmount);

        yield return null;
    }

}
