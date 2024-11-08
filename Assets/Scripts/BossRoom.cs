using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    // Start is called before the first frame update


    // Future logic for spawning boss and defeating the boss
    
    void ActivateFinalDoor(){
        if (!topDoor.activeInHierarchy){
            topDoor.SetActive(true);
            topDoor.GetComponent<SpriteRenderer>().color = Color.white;
            topDoor.tag = "FinalDoor";
            //roomManager.SetLevelComplete(true);
            return;
        }
        if (!bottomDoor.activeInHierarchy){
            bottomDoor.SetActive(true);
            bottomDoor.GetComponent<SpriteRenderer>().color = Color.white;
            bottomDoor.tag = "FinalDoor";
            //roomManager.SetLevelComplete(true);
            return;
        }
        if (!leftDoor.activeInHierarchy){
            leftDoor.SetActive(true);
            leftDoor.GetComponent<SpriteRenderer>().color = Color.white;
            leftDoor.tag = "FinalDoor";
            //roomManager.SetLevelComplete(true);
            return;
        }
        if (!rightDoor.activeInHierarchy){
            rightDoor.SetActive(true);
            rightDoor.GetComponent<SpriteRenderer>().color = Color.white;
            rightDoor.tag = "FinalDoor";
           //roomManager.SetLevelComplete(true);
            return;
        }
        //TODO: Add logic to spawn some other door if all doors are active
        
    }


    void Start()
    {
        //no boss yet, just open the door instantly
        ActivateFinalDoor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
