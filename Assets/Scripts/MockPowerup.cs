using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MockPowerup : PowerupManager
{
    public bool powerupActivated {get;private set;} = false;

    public override void ActivatePowerUp()
    {
        powerupActivated = true;
    }
}
