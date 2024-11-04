using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;
    [SerializeField] private int maxRooms=15;
    [SerializeField] private int minRooms=10;

    int roomWidth=28;
    int roomHeight=17;

    int gridSizeX=10;
    int gridSizeY=10;

    private bool generationComplete=false;

    private List<GameObject> rooms = new List<GameObject>();

    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    private int roomCount;


    // Start is called before the first frame update
    void Start()
    {
        roomGrid=new int[gridSizeX,gridSizeY];
        roomQueue = new Queue<Vector2Int>();
        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX/2,gridSizeY/2);
        StartRoomGeneration(initialRoomIndex);
        //OpenDoors(initialRoom, x, y);
    }

    // Update is called once per frame

    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridX=gridIndex.x;
        int gridY=gridIndex.y;
        return new Vector3(roomWidth*(gridX-gridSizeX/2),roomHeight*(gridY-gridSizeY/2));
    }

    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0,1,1,0.05f);
        Gizmos.color=gizmoColor;
        for (int x=0;x<gridSizeX;x++){
            for (int y=0;y<=gridSizeY;y++){
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x,y));
                Gizmos.DrawWireCube(new Vector3(position.x,position.y), new Vector3(roomWidth,roomHeight,1));
            }
        }
    }

    private void StartRoomGeneration(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        //Debug.Log("Starting room index: "+roomIndex);
        roomGrid[x,y]=1;
        roomCount++;
        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex),Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex=roomIndex;
        rooms.Add(initialRoom);
    }

    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (x>=gridSizeX || y>=gridSizeY || x<0 || y<0)
        {
            return false;
        }

        if (roomCount>=maxRooms)
        {
            return false;
        }

        if (Random.value < 0.5f && roomIndex !=Vector2Int.zero)
        {
            return false;
        }
        if (CountAdjacentRooms(roomIndex)>1)
        {
            return false;
        }

        roomQueue.Enqueue(roomIndex);
        roomGrid[x,y] = 1;
        roomCount++;

        var newRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex),Quaternion.identity);
        newRoom.GetComponent<Room>().RoomIndex=roomIndex;
        newRoom.name=$"Room-{roomCount}";
        rooms.Add(newRoom);
        OpenDoors(newRoom,x,y);
        return true;

        
    }

    private void RegenerateRooms()
    {
        rooms.ForEach(room=>Destroy(room));
        rooms.Clear();
        roomGrid = new int[gridSizeX,gridSizeY];
        roomQueue.Clear();
        roomCount=0;
        generationComplete=false;
        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX/2,gridSizeY/2);
        StartRoomGeneration(initialRoomIndex);
    }

    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0 ;

        if (x>0 && roomGrid[x-1,y]!=0)
        {
            count++;
        }
        if (x<gridSizeX-1 && roomGrid[x+1,y]!=0)
        {
            count++;
        }
        if (y>0 && roomGrid[x,y-1]!=0)
        {
            count++;
        }
        if (y<gridSizeY-1 && roomGrid[x,y+1]!=0)
        {
            count++;
        }

        return count;
    }

    void OpenDoors(GameObject room, int x, int y)
    {
        Room roomScript = room.GetComponent<Room>();
        Room leftRoom = GetRoomScriptAt(new Vector2Int(x-1,y));
        Room rightRoom = GetRoomScriptAt(new Vector2Int(x+1,y));
        Room topRoom = GetRoomScriptAt(new Vector2Int(x,y+1));
        Room bottomRoom = GetRoomScriptAt(new Vector2Int(x,y-1));
        //Debug.Log(roomScript + " with its top room: " +topRoom);
        //Debug.Log(roomScript+ " with its bottom room: "+ bottomRoom);  

        if (x>0 && roomGrid[x-1,y]!=0 && leftRoom!=null)
        {
            roomScript.OpenDoor(Vector2Int.left);
            leftRoom.OpenDoor(Vector2Int.right);
        }
        if (x<gridSizeX-1 && roomGrid[x+1,y]!=0 && rightRoom!=null)
        {
            roomScript.OpenDoor(Vector2Int.right);
            rightRoom.OpenDoor(Vector2Int.left);
        }
        if (y>0 && roomGrid[x,y-1]!=0 && bottomRoom!=null)
        {
            roomScript.OpenDoor(Vector2Int.down);
            bottomRoom.OpenDoor(Vector2Int.up);
        }
        if (y<gridSizeY-1 && roomGrid[x,y+1]!=0 && topRoom!=null)
        {
            roomScript.OpenDoor(Vector2Int.up);
            topRoom.OpenDoor(Vector2Int.down);
        }
    }

    public Room GetRoomScriptAt(Vector2Int index)
    {
        GameObject room = rooms.Find(r => r.GetComponent<Room>().RoomIndex == index);
        //Debug.Log("Room at index: "+index+" is "+room);
        if (room != null)
        {
            return room.GetComponent<Room>();
        }
        return null;
    }
    void Update()
    {
        if (roomQueue.Count>0 && roomCount<maxRooms && !generationComplete)
        {
            Vector2Int roomIndex=roomQueue.Dequeue();
            int gridX=roomIndex.x;
            int gridY=roomIndex.y;

            TryGenerateRoom(new Vector2Int(gridX+1,gridY));
            TryGenerateRoom(new Vector2Int(gridX-1,gridY));
            TryGenerateRoom(new Vector2Int(gridX,gridY+1));
            TryGenerateRoom(new Vector2Int(gridX,gridY-1));
        }
        else if (roomCount<minRooms)
        {
            Debug.Log("Generation Failed, Regenerating Rooms");
            RegenerateRooms();
        }
        else if (!generationComplete)
        {
            generationComplete=true;
            Debug.Log("Generation Complete");
        }
    }
}
