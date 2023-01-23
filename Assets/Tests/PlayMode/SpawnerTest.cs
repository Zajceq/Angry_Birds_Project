using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SpawnerTest
{
    //// A Test behaves as an ordinary method
    //[Test]
    //public void SpawnerTestSimplePasses()
    //{
    //    // Use the Assert class to test conditions
    //}

    //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    //// `yield return null;` to skip a frame.
    //[UnityTest]
    //public IEnumerator SpawnerTestWithEnumeratorPasses()
    //{
    //    // Use the Assert class to test conditions.
    //    // Use yield to skip a frame.
    //    var gameObject = new GameObject();
    //    var prefabSpawner = gameObject.AddComponent<TestDrivenPrefabSpawner>();
    //    prefabSpawner.SpawnPrefabs();
    //    var spawnedGameObjects = GameObject.FindGameObjectsWithTag(prefabSpawner.PrefabTag);
    //    Assert.AreEqual(3, spawnedGameObjects.Length);
    //    yield return null;

    //}
    //[Test]
    //public void TestSceneHasThreeObjectsWithSameTag()
    //{
    //    var objectsWithTag = GameObject.FindGameObjectsWithTag("TargetFull");
    //    Assert.AreEqual(3, objectsWithTag.Length, "There should be exactly 3 objects with tag 'TargetFull' on the scene.");
    //}

    //[UnityTest]
    //public IEnumerator CheckTagObjects()
    //{
    //    var gameObject = new GameObject();
    //    var prefabSpawner = gameObject.AddComponent<TestDrivenPrefabSpawner>();

    //    GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(prefabSpawner.PrefabTag);

    //    int expectedAmount = 3;
    //    int actualAmount = objectsWithTag.Length;

    //    Assert.AreEqual(expectedAmount, actualAmount);

    //    yield return null;
    //}

    [UnityTest]
    public IEnumerator TestSpawnPrefabs()
    {
        // Find the TestDrivenPrefabSpawner script
        var spawner = GameObject.FindObjectOfType<TestDrivenPrefabSpawner>();

        // Call the SpawnPrefabs function
        spawner.SpawnPrefabs();

        // Wait for one frame
        yield return null;

        // Check that there are exactly 3 objects with the same tag on the scene
        var objectsWithTag = GameObject.FindGameObjectsWithTag("TargetFull");
        Assert.AreEqual(3, objectsWithTag.Length, "There should be exactly 3 objects with tag 'SameTag' on the scene.");
    }
}
