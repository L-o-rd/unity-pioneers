using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopMenuScript : MonoBehaviour
{
    public void SwitchToScene(string test)
    {
        SceneManager.LoadScene(test);
    }
    public void UpgradeStat()
    {
        //TODO
    }
}
