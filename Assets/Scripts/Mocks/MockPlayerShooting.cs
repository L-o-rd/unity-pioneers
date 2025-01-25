using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockPlayerShooting : MonoBehaviour
{
    public float fireRateMultiplier {get;private set;}

    public float setFireRateMultiplier (float fireRate) => fireRateMultiplier = fireRate;
}
