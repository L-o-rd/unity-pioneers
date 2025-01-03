using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private float bonusDifficulty = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        bonusDifficulty=GameObject.Find("RoomManager").GetComponent<RoomManager>().GetBonusDifficulty();
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Player")
        {
            if (UnityEngine.Random.value<(0.3f)*bonusDifficulty){
                Debug.Log("Player was damaged by a trap chest");
                other.gameObject.GetComponent<PlayerStats>().TakeDamage(20);
            }
            else{
                Debug.Log("Player opened a chest");
                other.gameObject.GetComponent<PlayerStats>().addCoins(60*bonusDifficulty);
            }
        }
    }
}
