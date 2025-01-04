using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private float heal=20f;
    void Start()
    {
        heal /= GameObject.Find("RoomManager").GetComponent<RoomManager>().GetDifficulty();
    }

        void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Player"){
            other.GetComponent<PlayerStats>().Heal(heal);
            FindObjectOfType<InGameTextUI>().ShowWorldFeedback("+"+heal+"HP");
            Debug.Log("Player was healed for "+heal+" health");
            Destroy(gameObject);
        }
    }

}
