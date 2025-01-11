using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int value;
    void Start()
    {
        value=UnityEngine.Random.Range(10, 25);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Player"){
            other.GetComponent<PlayerStats>().addCoins(value);
            Debug.Log("Player picked up "+value+" coins");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
