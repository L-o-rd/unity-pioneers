using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class RoomGenerator : MonoBehaviour
{

    [SerializeField] 
    private GameObject tilePrefab;

    [SerializeField] 
    private GameObject wallPrefab;
    
    [SerializeField]
    private GameObject doorPrefab;

    [SerializeField] 
    private GameObject keyPrefab;

    [SerializeField] 
    private GameObject player;
    
    [SerializeField] 
    private float tileSize = 1f;

    [SerializeField] 
    private Vector2 roomOrigin = Vector2.zero;

    [SerializeField]
    private Vector2Int roomDimensions = new Vector2Int(20, 20);

    private Vector2 offset;

    public enum Directions
    {
        Top,
        Bottom,
        Left,
        Right
    }
    private Dictionary<Directions, GameObject> doors = new Dictionary<Directions, GameObject>();

    // Start is called before the first frame update


    public Vector2 getRoomOrigin()
    {
        return roomOrigin;
    }

    public float getTileSize()
    {
        return tileSize;
    }

    public Vector2Int getRoomDimensions()
    {
        return roomDimensions;
    }

    void Start()
    {
        Debug.Log("start");
        GenerateTiles();
        GenerateWalls();
        GenerateDoors();
        GenerateKey();
    }

    private void GenerateTiles()
    {
        //Debug.Log("Generated Tiles");    
        offset = new Vector2(-(roomDimensions.x * tileSize) / 2f, -(roomDimensions.y * tileSize) / 2f);
        for (int x = 0; x < roomDimensions.x; x++)
        {
            for (int y = 0; y < roomDimensions.y; y++)
            {
                //Debug.Log(x + " " + y);
                Vector2 position = roomOrigin + offset+ new Vector2(x * tileSize, y * tileSize);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                tile.transform.localScale = Vector3.one * tileSize;
            }
        }
    }

    private void GenerateWalls()
    {
        //Vector2 offset = new Vector2(-(roomDimensions.x * tileSize) / 2f, -(roomDimensions.y * tileSize) / 2f);
        for (int x = 0; x <= roomDimensions.x; x++)
        {
            // Top Wall 
            if (x==roomDimensions.x/2)
            {
                continue;
            }
            Vector2 topWallPosition = roomOrigin + offset + new Vector2(x * tileSize, roomDimensions.y * tileSize);
            GameObject wall = Instantiate(wallPrefab, topWallPosition, Quaternion.identity, transform);
            wall.transform.localScale = Vector3.one * tileSize;

            // Bottom Wall
            Vector2 bottomWallPosition = roomOrigin + offset+  new Vector2(x * tileSize, -tileSize);
            wall = Instantiate(wallPrefab, bottomWallPosition, Quaternion.identity, transform);
            wall.transform.localScale = Vector3.one * tileSize;
        }


        for (int y = 0; y <= roomDimensions.y; y++)
        {
            if (y==roomDimensions.y/2)
            {
                continue;
            }
            // Left Wall

            Vector2 leftWallPosition = roomOrigin + offset+ new Vector2(-tileSize, y * tileSize);
            GameObject wall = Instantiate(wallPrefab, leftWallPosition, Quaternion.identity, transform);
            wall.transform.localScale = Vector3.one * tileSize;

            // Right Wall
            Vector2 rightWallPosition = roomOrigin + offset+ new Vector2(roomDimensions.x * tileSize, y * tileSize);
            wall = Instantiate(wallPrefab, rightWallPosition, Quaternion.identity, transform);
            wall.transform.localScale = Vector3.one * tileSize;
        }
        
    }

    private void GenerateDoors()
    {

        Vector2 topDoorPosition = roomOrigin + offset +  new Vector2((roomDimensions.x / 2) * tileSize, roomDimensions.y * tileSize);
        GameObject topDoor = Instantiate(doorPrefab, topDoorPosition, Quaternion.identity, transform);
        doors.Add(Directions.Top, topDoor);

        Vector2 bottomDoorPosition = roomOrigin + offset +  new Vector2((roomDimensions.x / 2) * tileSize, -tileSize);
        GameObject bottomDoor=Instantiate(doorPrefab, bottomDoorPosition, Quaternion.identity, transform);
        doors.Add(Directions.Bottom, bottomDoor);

        Vector2 leftDoorPosition = roomOrigin + offset +  new Vector2(-tileSize, (roomDimensions.y / 2) * tileSize);
        GameObject leftDoor=Instantiate(doorPrefab, leftDoorPosition, Quaternion.identity, transform);
        doors.Add(Directions.Left, leftDoor);

        Vector2 rightDoorPosition = roomOrigin + offset + new Vector2(roomDimensions.x * tileSize, (roomDimensions.y / 2) * tileSize);
        GameObject rightDoor=Instantiate(doorPrefab, rightDoorPosition, Quaternion.identity, transform);
        doors.Add(Directions.Right, rightDoor);

        foreach (var door in doors)
        {
            Debug.Log($"Door added: {door.Key} at position {door.Value.transform.position}");
        }

    }

     private void GenerateKey()
    {
        Debug.Log("Generated key");
        Vector2 randomPosition = roomOrigin + offset + new Vector2(Random.Range(1, roomDimensions.x - 1) * tileSize, Random.Range(1, roomDimensions.y - 1) * tileSize);
        GameObject key = Instantiate(keyPrefab, randomPosition, Quaternion.identity,transform);
        key.AddComponent<Key>().Initialize(this, Directions.Top);
        //do not forget to make it impossible to spawn key in the center of the room 
    }

    public void GenerateAdjacentRoom(Directions direction)
    {
        Debug.Log("GenerateAdjacentRoom called for direction: " + direction);
        roomOrigin+=new Vector2(0,20); //for testing
        
        Vector2 newRoomOrigin = direction switch
        {
            Directions.Top => roomOrigin + Vector2.up * roomDimensions.y * tileSize,
            Directions.Bottom => roomOrigin + Vector2.down * roomDimensions.y * tileSize,
            Directions.Right => roomOrigin + Vector2.right * roomDimensions.x * tileSize,
            Directions.Left => roomOrigin + Vector2.left * roomDimensions.x * tileSize,
            _ => roomOrigin
        };

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(newRoomOrigin, tileSize * 0.5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<RoomGenerator>() != null)
            {
                Debug.Log("Room already exists at " + newRoomOrigin);
                return; // Exit if a room is already there
            }
        }

        Debug.Log("Instantiating a new room.");
        RoomGenerator newRoom = Instantiate(gameObject, newRoomOrigin, Quaternion.identity).GetComponent<RoomGenerator>();
        Debug.Log("New room created.");
        newRoom.roomOrigin = newRoomOrigin;

        // Open the opposite door in the new room for easy travel
        Directions oppositeDirection = direction switch
        {
            Directions.Top => Directions.Bottom,
            Directions.Bottom => Directions.Top,
            Directions.Right => Directions.Left,
            Directions.Left => Directions.Right,
            _ => Directions.Top
        };

        OpenDoor(direction);
        newRoom.OpenDoor(oppositeDirection);
    }

    public void OpenDoor(Directions direction)
    {
        Debug.Log("Opening door in direction"+direction);
        if (doors.TryGetValue(direction, out GameObject door))
        {
            door.GetComponent<Door>().Initialize(this, direction);
            door.GetComponent<Door>().Open();
            Debug.Log("Got values for direction "+direction);
        }
        else
        {
            Debug.LogError("Door not found for direction " + direction);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
