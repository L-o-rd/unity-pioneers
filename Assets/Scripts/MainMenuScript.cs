using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }
    public void SwitchToScene(string test)
    {
        SceneManager.LoadScene(test);
    }
}
