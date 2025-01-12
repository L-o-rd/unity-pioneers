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

    bool finalActivated = false;


    void Start()
    {
        //no boss yet, just open the door instantly
        // ActivateFinalDoor();
        wasOpen = new bool[4];
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        foreach (Transform t in transform)
        {
            if (t.CompareTag("Enemy"))
            {
                return;
            }
        }

        if (!finalActivated)
        {
            OpenDoors();
            ActivateFinalDoor();
            finalActivated = true;
        }
    }

    public override void CloseDoors()
    {
        if (finalActivated) return;
        wasOpen[0] = topDoor.activeInHierarchy;
        wasOpen[1] = bottomDoor.activeInHierarchy;
        wasOpen[2] = leftDoor.activeInHierarchy;
        wasOpen[3] = rightDoor.activeInHierarchy;

        CloseDoor(Vector2Int.up);
        CloseDoor(Vector2Int.down);
        CloseDoor(Vector2Int.left);
        CloseDoor(Vector2Int.right);
    }

    public override void OpenDoors()
    {
        if (wasOpen[0]) topDoor.SetActive(true);
        if (wasOpen[1]) bottomDoor.SetActive(true);
        if (wasOpen[2]) leftDoor.SetActive(true);
        if (wasOpen[3]) rightDoor.SetActive(true);
    }
}
