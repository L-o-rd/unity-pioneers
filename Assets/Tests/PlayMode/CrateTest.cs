using NUnit.Framework;
using UnityEngine;

public class CrateTests
{
    //Sound effects are added to inherited classes, so we can't test them here

    [Test]
    public void InteractWithCrate_CallsInteractMethod()
    {
        // Arrange
        GameObject crateObject = new GameObject();
        MockCrate mockCrate = crateObject.AddComponent<MockCrate>();

        // Act
        mockCrate.InteractWithCrate();

        // Assert
        Assert.IsTrue(mockCrate.wasInteractedWith);
    }

}
