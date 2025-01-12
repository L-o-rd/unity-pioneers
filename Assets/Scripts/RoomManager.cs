using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [SerializeField] new Camera camera;
    [SerializeField] List<GameObject> roomPrefabs;
    [SerializeField] GameObject bossRoomPrefab;
    [SerializeField] GameObject treasureRoomPrefab;
    [SerializeField] List<GameObject> challengeRoomPrefab;

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
        TryMakeRoom<Room>(roomPrefabs[0], WalkerPosition, "Room", null, null);
    }

    private Vector2Int LastWalkerPosition = new Vector2Int(0, 0);
    private Vector2Int WalkerPosition = new Vector2Int(0, 0);
    private Vector2Int CurrentRoom = new Vector2Int(0, 0);
    private readonly int WalkerSteps = 20;

    private int CurrentLevel = 0;

    [SerializeField]
    private int LastLevel = 3;

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

    private GameObject GetRandomRoomPrefab()
    {
        return roomPrefabs[RNGManager.Instance.rng.Next(0, roomPrefabs.Count)];
    }

    private GameObject GetRandomChallengeRoomPrefab()
    {
        return challengeRoomPrefab[RNGManager.Instance.rng.Next(0, challengeRoomPrefab.Count)];
    }

    private void GenerateRooms()
    {
        while (true)
        {
            for (int i = 0; i < WalkerSteps; ++i)
            {
                if (roomCount >= maxRooms)
                    goto Done;

                WalkerStep(RNGManager.Instance.rng.Next(0, 4));
                if (TryMakeRoom<TreasureRoom>(treasureRoomPrefab, WalkerPosition, "TreasureRoom", RNGManager.Instance.rng, TreasureCondition)) continue;
                if (TryMakeRoom<ChallengeRoom>(GetRandomChallengeRoomPrefab(), WalkerPosition, "ChallengeRoom", RNGManager.Instance.rng, ChallengeCondition)) continue;
                TryMakeRoom<Room>(GetRandomRoomPrefab(), WalkerPosition, "Room", null, null);
                // if (TryMakeRoom<Room>(GetRandomRoomPrefab(), WalkerPosition, "Room", null, null))
                // {
                //     difficultyMultiplier += 0.1f;
                //     if (difficultyMultiplier >= difficultyMultiplierCap)
                //     {
                //         difficultyMultiplier = difficultyMultiplierCap;
                //     }
                // }

                //Debug.Log($"({WalkerPosition.x}, {WalkerPosition.y})");
            }

            if (roomCount >= minRooms) goto Done;
        }

    Done:;
        while (!TryMakeRoom<BossRoom>(bossRoomPrefab, WalkerPosition, "BossRoom", RNGManager.Instance.rng, null))
        {
            WalkerStep(RNGManager.Instance.rng.Next(0, 4));
            // Debug.Log($"Boss: ({WalkerPosition.x}, {WalkerPosition.y})");
        }
    }


    private void Start()
    {
        RNGManager.Instance.diamonds = 0;
        RNGManager.Instance.Make();
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
        this.GetCurrentRoom().CloseDoors();
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

    private void FinishRun()
    {
        var pstats = FindObjectOfType<PlayerStats>();
        pstats.status.permanentCoins += CurrentLevel;
        pstats.SaveStats();
        SceneManager.LoadScene("FinishRun");
    }

    public void CreateLevel()
    {
        RNGManager.Instance.diamonds = CurrentLevel + 1;
        if (++CurrentLevel >= LastLevel)
        {
            FinishRun();
            return;
        }

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
            SetDifficulty(Math.Min(GetDifficulty() + 0.1f, difficultyMultiplierCap));
            SetMinRooms(minRooms + 1);
            SetMaxRooms(maxRooms + 2);
        }
        Debug.Log("Creating level");
        TryMakeRoom<Room>(roomPrefabs[0], WalkerPosition, "Room", null, null);
        GenerateRooms();
        SetBounds();
    }

    void Update()
    {

    }
}
