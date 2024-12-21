using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBost : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Player"){
            other.GetComponent<PlayerMovement>().SpeedUpPlayerBy(0.9f);
            Debug.Log("Speeding up Player");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
