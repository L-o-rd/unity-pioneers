using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockPlayerMovement : MonoBehaviour
{
    public bool speedIncreased {get; private set;}
    public bool speedDecreased {get; set;}
    public bool dashActive {get;private set;}
    public float maxSpeed {get;set;}

    // Simulates SpeedUpPlayerBy from PlayerMovement and ScaleSpeed from Player stats

    public float ScaleSpeed(float percentage) => maxSpeed = maxSpeed * percentage;
    public float SpeedUpPlayerBy(float percentage)
    {
        speedIncreased = true;
        return ScaleSpeed(1.0f / percentage);
    }

    public bool ActivateDashPower() => dashActive = true;
 
}
