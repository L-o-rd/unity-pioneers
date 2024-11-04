using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update

    private RoomGenerator roomGenerator;
    private RoomGenerator.Directions direction;
    bool isOpen=false;

    public void Initialize(RoomGenerator generator, RoomGenerator.Directions direction)
    {
        this.roomGenerator = generator;
        this.direction = direction;

    }

    public void Open()
    {
        //TODO: change sprite to open door
        GetComponent<SpriteRenderer>().color = Color.green;
        GetComponent<BoxCollider2D>().isTrigger = true;
        isOpen=true;
        Debug.Log(direction + " door opened "); 
    }

    private void TeleportPlayer(GameObject player)
    {
        if (player==null){
            Debug.LogError("Null reference detected: The 'player' object is null.");
            return;
        }
        Debug.Log(roomGenerator);
        Debug.Log("Player exists, making teleportation calculations...");
        Vector2 roomOrigin= roomGenerator.getRoomOrigin();
        Debug.Log($"Room origin: {roomOrigin}");
        Vector2Int roomDimensions = roomGenerator.getRoomDimensions();
        Debug.Log($"Room dimensions: {roomDimensions}");
        float tileSize = roomGenerator.getTileSize();
        Debug.Log($"Tile size: {tileSize}");
        Vector2 teleportPosition = roomOrigin + direction switch
        {
            RoomGenerator.Directions.Top => Vector2.up * roomDimensions.y * tileSize,
            RoomGenerator.Directions.Bottom => Vector2.down * roomDimensions.y * tileSize,
            RoomGenerator.Directions.Left => Vector2.left * roomDimensions.x * tileSize,
            RoomGenerator.Directions.Right => Vector2.right * roomDimensions.x * tileSize,
            _ => Vector2.zero
        };
        Debug.Log($"Teleporting player to {teleportPosition}");
        if (player.TryGetComponent(out Transform playerTransform))
        {
            playerTransform.position = teleportPosition;
            Debug.Log($"Player teleported to {teleportPosition}");
        }
        else
        {
            Debug.LogError("TeleportPlayer failed: Player object does not have a Transform component.");
            return;
        }

        //player.transform.position=teleportPosition;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision with door");
        if (isOpen && other.CompareTag("Player"))
        {
            Debug.Log("Teleporting player");
            if (other.gameObject != null)
            {
                Debug.Log("Door is open and player is colliding, attempting to teleport...");
                Debug.Log(other.gameObject);
                Debug.Log(other.tag);
                TeleportPlayer(other.gameObject);
            }
            else
            {
                Debug.LogError("Null reference detected: The 'other' object is null.");
            }
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
