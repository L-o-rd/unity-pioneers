using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene("RoomPrefab");
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
