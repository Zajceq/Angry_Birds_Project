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
    [UnityTest]
    public IEnumerator SpawnerTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        var gameObject = new GameObject();
        //var PrefabSpawner = gameObject.AddComponent<TestDrivenPrefabSpawner>();
        yield return null;
    }
}
