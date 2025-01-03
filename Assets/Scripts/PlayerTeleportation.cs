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

    private Room Current;
    private void Awake()
    {
    }

    private void TeleportFromDoor(Collider2D collision){
        // Debug.Log("Door detected");
        Vector2Int direction = Vector2Int.zero;
        this.Current = roomManager.GetCurrentRoom();

        if (collision.gameObject == Current.topDoor)
        {
            direction = Vector2Int.up;
        }
        else if (collision.gameObject == Current.bottomDoor)
        {
            direction = Vector2Int.down;
        }
        else if (collision.gameObject == Current.leftDoor)
        {
            direction = Vector2Int.left;
        }
        else if (collision.gameObject == Current.rightDoor)
        {
            direction = Vector2Int.right;
        }

        if (direction != Vector2Int.zero)
        {
            // Debug.Log("Teleporting to room in direction: " + direction);
            TeleportToRoomInDirection(direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Collision detected");
        if (collision.CompareTag("Door"))
        {
            TeleportFromDoor(collision);
        }
        else if (collision.CompareTag("FinalDoor"))
        {
            roomManager.SetLevelComplete(true);
        }
    }

    private void TeleportToRoomInDirection(Vector2Int direction)
    {
        Vector2Int targetRoomIndex = Current.RoomIndex + direction;
        GameObject door = null;
        // Debug.Log("Target room index: " + targetRoomIndex);
        // Debug.Log(roomManager);
        Room targetRoom = roomManager.GetRoomAt(targetRoomIndex);
        if (direction == Vector2Int.down) door = targetRoom.topDoor;
        else if (direction == Vector2Int.up) door = targetRoom.bottomDoor;
        else if (direction == Vector2Int.left) door = targetRoom.rightDoor;
        else if (direction == Vector2Int.right) door = targetRoom.leftDoor;
        // Debug.Log(targetRoom);
        if (targetRoom != null)
        {
            Vector3 targetPosition = door.transform.position;
            Vector3 offset = new Vector3(direction.x, direction.y, 0) * teleportOffset;
            // Debug.Log("Teleporting to position: " + targetPosition + offset);
            transform.position = targetPosition + offset;
            // Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z)-offset;
            roomManager.SetCurrentRoom(targetRoomIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
