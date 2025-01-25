using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagerMock : MonoBehaviour
{
    private float difficulty = 1.0f;

    public void SetDifficulty(float value) => difficulty = value;

    public float GetDifficulty() => difficulty;
}
