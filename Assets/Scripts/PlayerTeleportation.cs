using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportation : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] 
    private float teleportOffset = 1.0f; // Offset to avoid teleporting the player to the door

    [SerializeField]
    private RoomManager roomManager;
    private Room currentRoom;
    void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
        if (roomManager == null)
        {
            Debug.LogError("RoomManager not found in the scene");
        }
    }

    private void TeleportToSpawn(){
        Debug.Log("Final door detected");
        Debug.Log("Level complete");
        transform.position = new Vector3(0,0,0);
        Camera.main.transform.position = new Vector3(0,0,Camera.main.transform.position.z);
    }

    private void TeleportFromDoor(Collider2D collision){
        Debug.Log("Door detected");
            Vector2Int direction = Vector2Int.zero;

            if (collision.gameObject == currentRoom.topDoor)
            {
                direction = Vector2Int.up;
            }
            else if (collision.gameObject == currentRoom.bottomDoor)
            {
                direction = Vector2Int.down;
            }
            else if (collision.gameObject == currentRoom.leftDoor)
            {
                direction = Vector2Int.left;
            }
            else if (collision.gameObject == currentRoom.rightDoor)
            {
                direction = Vector2Int.right;
            }

            if (direction != Vector2Int.zero)
            {
                Debug.Log("Teleporting to room in direction: " + direction);
                TeleportToRoomInDirection(direction);
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected");
        if (collision.CompareTag("Door") && currentRoom != null)
        {
            TeleportFromDoor(collision);
        }
        else if (collision.CompareTag("FinalDoor") && currentRoom!=null){
            roomManager.SetLevelComplete(true);
            TeleportToSpawn();
        }
        else if (currentRoom == null && collision.CompareTag("Door"))
        {
            Debug.LogWarning("CurrentRoom is null when trying to teleport");
        }
    }

    private void TeleportToRoomInDirection(Vector2Int direction)
    {
        Vector2Int targetRoomIndex = currentRoom.RoomIndex + direction;
        Debug.Log("Target room index: " + targetRoomIndex);
        Debug.Log(roomManager);
        Room targetRoom = roomManager.GetRoomScriptAt(targetRoomIndex);
        Debug.Log(targetRoom);
        if (targetRoom != null)
        {
            Vector3 targetPosition = targetRoom.transform.position;
            Vector3 offset = new Vector3(direction.x, direction.y, 0) * teleportOffset;
            Debug.Log("Teleporting to position: " + targetPosition + offset);
            transform.position = targetPosition + offset;
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z)-offset;
            currentRoom = targetRoom;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Collision detected");
        Debug.Log(collision.tag);
        if (collision.CompareTag("Room"))
        {
            currentRoom = collision.GetComponent<Room>();
        }
        Debug.Log(roomManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
