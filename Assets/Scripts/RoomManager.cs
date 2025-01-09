using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] new Camera camera;
    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject bossRoomPrefab;
    [SerializeField] GameObject treasureRoomPrefab;
    [SerializeField] GameObject challengeRoomPrefab;

    [SerializeField] private int maxRooms = 4, minRooms = 2;
    [SerializeField] private float difficultyMultiplier = 1.0f;
    [SerializeField] private float difficultyMultiplierCap = 2.0f;

    private readonly int roomHeight = 17;
    private readonly int roomWidth = 32;
    private bool levelComplete = false;

    private Dictionary<Vector2Int, int> roomDict = new Dictionary<Vector2Int, int>();
    private List<GameObject> rooms = new List<GameObject>();

    private int roomCount;

    void Awake()
    {
        TryMakeRoom<Room>(roomPrefab, WalkerPosition, "Room", null, null);
    }

    private Vector2Int LastWalkerPosition = new Vector2Int(0, 0);
    private Vector2Int WalkerPosition = new Vector2Int(0, 0);
    private Vector2Int CurrentRoom = new Vector2Int(0, 0);
    private readonly int WalkerSteps = 20;
    [SerializeField]
    private int seed = 0;
    private System.Random rng;

    private void WalkerStep(int choice)
    {
        LastWalkerPosition.x = WalkerPosition.x;
        LastWalkerPosition.y = WalkerPosition.y;

        if (choice == 0)
        {
            WalkerPosition.x++;
        }
        else if (choice == 1)
        {
            WalkerPosition.x--;
        }
        else if (choice == 2)
        {
            WalkerPosition.y++;
        }
        else if (choice == 3)
        {
            WalkerPosition.y--;
        }
    }

    public void MakeRNG(int seed)
    {
        this.rng = new System.Random(seed);
    }

    private void GenerateRooms()
    {
        while (true)
        {
            for (int i = 0; i < WalkerSteps; ++i)
            {
                if (roomCount >= maxRooms)
                    goto Done;

                WalkerStep(rng.Next(0, 4));
                if (TryMakeRoom<TreasureRoom>(treasureRoomPrefab, WalkerPosition, "TreasureRoom", rng, TreasureCondition)) continue;
                if (TryMakeRoom<ChallengeRoom>(challengeRoomPrefab, WalkerPosition, "ChallengeRoom", rng, ChallengeCondition)) continue;
                if (TryMakeRoom<Room>(roomPrefab, WalkerPosition, "Room", null, null))
                {
                    // difficultyMultiplier += 0.1f;
                    // if (difficultyMultiplier >= difficultyMultiplierCap)
                    // {
                    //     difficultyMultiplier = difficultyMultiplierCap;
                    // }
                }

                //Debug.Log($"({WalkerPosition.x}, {WalkerPosition.y})");
            }

            if (roomCount >= minRooms) goto Done;
        }

    Done:;
        while (!TryMakeRoom<BossRoom>(bossRoomPrefab, WalkerPosition, "BossRoom", rng, null))
        {
            WalkerStep(rng.Next(0, 4));
            // Debug.Log($"Boss: ({WalkerPosition.x}, {WalkerPosition.y})");
        }

        foreach (var room in rooms)
        {

        }
    }


    private void Start()
    {
        MakeRNG(this.seed);
        GenerateRooms();
        SetBounds();
    }

    private void SetBounds()
    {
        Room room = rooms[roomDict[CurrentRoom]].GetComponent<Room>();
        camera.GetComponent<CameraMovement>().SetEast(room.maxEast.position);
        camera.GetComponent<CameraMovement>().SetWest(room.maxWest.position);
        camera.GetComponent<CameraMovement>().SetNorth(room.maxNorth.position);
        camera.GetComponent<CameraMovement>().SetSouth(room.maxSouth.position);
    }

    public Room GetCurrentRoom()
    {
        return rooms[roomDict[CurrentRoom]].GetComponent<Room>();
    }

    public void SetLevelComplete(bool value)
    {
        levelComplete = value;
    }

    public float GetDifficulty()
    {
        return difficultyMultiplier;
    }
    public float SetDifficulty(float difficulty)
    {
        return this.difficultyMultiplier = difficulty;
    }

    private Vector3 GetPositionFromGridIndex(Vector2Int index)
    {
        return new Vector3(roomWidth * index.x, roomHeight * index.y);
    }

    private delegate bool RoomCondition(System.Random rng);

    private bool TreasureCondition(System.Random rng)
    {
        return rng.Next(0, 100) < 10;
    }

    private bool ChallengeCondition(System.Random rng)
    {
        return rng.Next(0, 100) * 10 < (int) (50.0f * difficultyMultiplier);
    }

    private bool TryMakeRoom<T>(GameObject prefab, Vector2Int index, String name, System.Random rng, RoomCondition condition) where T : Room
    {
        if (condition is not null)
        {
            if (!condition(rng)) return false;
        }

        if (roomDict.ContainsKey(index)) return false;

        var room = Instantiate(prefab, GetPositionFromGridIndex(index), Quaternion.identity);
        if (LastWalkerPosition != WalkerPosition)
        {
            rooms[roomDict[LastWalkerPosition]].GetComponent<Room>().OpenDoor(WalkerPosition - LastWalkerPosition);
            room.GetComponent<Room>().OpenDoor(LastWalkerPosition - WalkerPosition);
        }

        room.GetComponent<T>().RoomIndex = index;
        room.name = $"{name}-{roomCount}";
        roomDict.Add(index, roomCount);
        rooms.Add(room);
        roomCount++;
        return true;
    }

    public void DeleteAllRemainingObjects()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.tag != "Player" && obj.tag != "MainCamera" && obj.tag != "UI" && obj.tag != "GameController" && obj.tag != "Untagged") //delete untagged at final build
            {
                Destroy(obj);
            }
        }
    }

    public void SetCurrentRoom(Vector2Int index)
    {
        this.CurrentRoom = index;
        this.SetBounds();
    }

    public Room GetRoomAt(Vector2Int index)
    {
        if (!roomDict.ContainsKey(index)) return null;
        return rooms[roomDict[index]].GetComponent<Room>();
    }

    public int SetMinRooms(int minRooms)
    {
        return this.minRooms = minRooms;
    }
    public int SetMaxRooms(int maxRooms)
    {
        return this.maxRooms = maxRooms;
    }

    public void createLevel(){
        if (roomDict.Count > 0)
        {
            DeleteAllRemainingObjects();
            roomDict.Clear();
            rooms.Clear();
            roomCount = 0;
            WalkerPosition = new Vector2Int(0, 0);
            LastWalkerPosition = new Vector2Int(0, 0);
            CurrentRoom = new Vector2Int(0, 0);
        }
        if (levelComplete){
            levelComplete = false;
            SetDifficulty(GetDifficulty() + 0.1f);
            SetMinRooms(minRooms + 1);
            SetMaxRooms(maxRooms + 2);
        }
        Debug.Log("Creating level");
        TryMakeRoom<Room>(roomPrefab, WalkerPosition, "Room", null, null);
        GenerateRooms();
        SetBounds();

    }

    void Update()
    {
        
    }
}
