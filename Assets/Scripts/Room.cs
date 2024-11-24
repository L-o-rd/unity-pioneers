using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
     public GameObject topDoor;

    [SerializeField]
    public GameObject bottomDoor;

    [SerializeField]
    public GameObject leftDoor;

    [SerializeField]
    public GameObject rightDoor;

    public Vector2Int RoomIndex;

    public void OpenDoor(Vector2Int direction)
    {
        if (direction==Vector2Int.up){
            topDoor.SetActive(true);
        }
        else if (direction==Vector2Int.down){
            bottomDoor.SetActive(true);
        }
        else if (direction==Vector2Int.left){
            leftDoor.SetActive(true);
        }
        else if (direction==Vector2Int.right){
            rightDoor.SetActive(true);
        }
    }

    public void CloseDoor(Vector2Int direction)
    {
        if (direction==Vector2Int.up){
            topDoor.SetActive(false);
        }
        else if (direction==Vector2Int.down){
            bottomDoor.SetActive(false);
        }
        else if (direction==Vector2Int.left){
            leftDoor.SetActive(false);
        }
        else if (direction==Vector2Int.right){
            rightDoor.SetActive(false);
        }
    }

    public void ColorDoor(Vector2Int direction, Color color){
        if (direction==Vector2Int.up){
            topDoor.GetComponent<SpriteRenderer>().color = color;
        }
        else if (direction==Vector2Int.down){
            bottomDoor.GetComponent<SpriteRenderer>().color = color;
        }
        else if (direction==Vector2Int.left){
            leftDoor.GetComponent<SpriteRenderer>().color = color;
        }
        else if (direction==Vector2Int.right){
            rightDoor.GetComponent<SpriteRenderer>().color = color;
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
