using System.Collections;
using FarmerDemo;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BerryBushTests
{
    [Test]
    public void BerryBushTestssSimplePasses()
    {
        Assert.True(true);
    }

    [UnityTest]
    public IEnumerator BerryBushTestssWithEnumeratorPasses()
    {
        yield return null;
        Assert.True(true);
    }

    //[Test]
    //public void BerryBush_InitialBerryCount_IsZero()
    //{
    //    var bush = new GameObject().AddComponent<BerryBushScript>();

    //    Assert.AreEqual(0f, bush.BerryCount);
    //}
}
