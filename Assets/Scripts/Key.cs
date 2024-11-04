using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private RoomGenerator roomGenerator;
    private RoomGenerator.Directions doorDirection;

    bool hasTriggered = false;

    public void Initialize(RoomGenerator generator, RoomGenerator.Directions direction)
    {
        roomGenerator = generator;
        doorDirection = direction;
        Debug.Log("Initialize called  with " + generator + " and " + direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;
        Debug.Log("Checking collision with " + other.tag);
        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            Debug.Log("Key collected by " + other.name); // New Debug line
            roomGenerator.GenerateAdjacentRoom(doorDirection); // Generate a new key
            //roomGenerator.OpenDoor(doorDirection); // Open the specified door and generate a new room
            Destroy(gameObject); // Despawn the key
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
