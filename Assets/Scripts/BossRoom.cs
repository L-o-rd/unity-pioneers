using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    // Start is called before the first frame update


    // Future logic for spawning boss and defeating the boss
    
    void ActivateDoor(GameObject door){
        door.SetActive(true);
        SpriteRenderer[] childRenderers = door.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in childRenderers)
        {
            renderer.color = Color.black;
        }
        door.tag = "FinalDoor";
    }
    void ActivateFinalDoor(){
        if (!topDoor.activeInHierarchy){
            ActivateDoor(topDoor);
            return;
        }

        if (!bottomDoor.activeInHierarchy){
            ActivateDoor(bottomDoor);
            return;
        }

        if (!leftDoor.activeInHierarchy){
            ActivateDoor(leftDoor);
            return;
        }

        if (!rightDoor.activeInHierarchy){
            ActivateDoor(rightDoor);
            return;
        }
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
