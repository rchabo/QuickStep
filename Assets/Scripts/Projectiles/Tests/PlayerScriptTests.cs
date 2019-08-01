using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor;

public class PlayerScriptTests {

    [Test]
    public void PlayerScriptTestsSimplePasses() {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator InstantiateProjectileFromPrefab() {

        var playerProjectilePrefab = Resources.Load("Tests/ProjectileTwo");

        yield return null;

        GameObject spawnedProjectile = GameObject.FindWithTag("PlayerProjectile");
        var prefabOfPlayerProjectile = PrefabUtility.GetCorrespondingObjectFromSource(spawnedProjectile);

        Assert.AreEqual(playerProjectilePrefab, prefabOfPlayerProjectile);
    }

    //part of the NUnit library, is run after every test.
    [TearDown]
    public void AfterEveryTest() {
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("PlayerProjectile")) {
            Object.Destroy(gameObject);
        }
    }
}
