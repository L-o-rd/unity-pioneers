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

    [SerializeField]
    private List<TrapWeight> trapWeights;

    [SerializeField]
    private float safeZoneRadius = 3f;

    [SerializeField]
    private float trapSpacing = 1.5f;
    private List<Vector3> occupiedPositions = new List<Vector3>();
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

    private GameObject GetRandomTrap()
    {
        if (trapWeights == null || trapWeights.Count == 0) return null;

        float totalWeight = 0f;
        foreach (var trap in trapWeights){
            totalWeight += trap.weight;
        }
        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0f;

        foreach (var trap in trapWeights)
        {
            cumulativeWeight += trap.weight;
            if (randomValue <= cumulativeWeight){
                return trap.trapPrefab;
            }
        }

        return null;
    }

    private bool IsPositionValid(Vector3 position)
    {
        // Check if position is within the safe zone
        if (Vector3.Distance(position, transform.position) < safeZoneRadius)
        {
            return false;
        }

        // Check if position overlaps with any occupied position
        foreach (Vector3 occupied in occupiedPositions)
        {
            if (Vector3.Distance(position, occupied) < trapSpacing)
            {
                return false;
            }
        }

        return true;
    }

    private Vector3 GetRandomValidPosition()
    {
        int maxAttempts = 100; // Prevent infinite loops in case of overcrowding
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector3 position = GetRandomPositionInRoom();
            if (IsPositionValid(position))
            {
                return position;
            }
        }

        return Vector3.zero;
    }

    private Vector3 GetRandomPositionInRoom()
    {
        float x = Random.Range(-12f, 12f);
        float y = Random.Range(-4.5f, 4.5f);
        return transform.position + new Vector3(x, y, 0);
    }

    public void GenerateTraps(int trapCount)
    {
        for (int i = 0; i < trapCount; i++)
        {
            GameObject selectedTrap = GetRandomTrap();
            if (selectedTrap != null)
            {
                Vector3 position = GetRandomValidPosition();

                if (position != Vector3.zero) // If a valid position was found
                {
                    Instantiate(selectedTrap, position, Quaternion.identity, transform);
                    occupiedPositions.Add(position); // Mark position as occupied
                }

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
