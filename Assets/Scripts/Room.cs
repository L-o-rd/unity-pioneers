using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [SerializeField]
    public GameObject topDoor;

    [SerializeField]
    public GameObject bottomDoor;

    [SerializeField]
    public GameObject leftDoor;

    [SerializeField]
    public GameObject rightDoor;

    [SerializeField]
    public Transform maxNorth;

    [SerializeField]
    public Transform maxSouth;

    [SerializeField]
    public Transform maxWest;

    [SerializeField]
    public Transform maxEast;

    [SerializeField]
    private List<RewardWeight> rewardWeights;
    public Vector2Int RoomIndex;

    private bool rewardSpawned = false;

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

    public void SpawnReward()
    {
        float totalWeight = 0f;
        foreach (RewardWeight reward in rewardWeights)
        {
            totalWeight += reward.weight;
        }

        float randomWeight = (float) RNGManager.Instance.rng.NextDouble() * totalWeight;
        float cumulativeWeight = 0f;

        foreach (RewardWeight reward in rewardWeights)
        {
            cumulativeWeight += reward.weight;
            if (randomWeight <= cumulativeWeight)
            {
                var spawnedReward=Instantiate(reward.rewardPrefab, transform.position + new Vector3(3,0,0), Quaternion.identity);
                spawnedReward.transform.parent = transform;
                return;
            }
        }
    }
    
    void Start()
    {
        wasOpen = new bool[4];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        bool found = false;
        foreach (Transform t in transform)
        {
            if (t.CompareTag("EnemySpawn"))
            {
                foreach (Transform e in t)
                {
                    if (e.CompareTag("Enemy"))
                    {
                        found = true; break;
                    }
                }
            } else if (t.CompareTag("Enemy"))
            {
                found = true; break;
            }
        }

        if (!found)
        {
            if (!rewardSpawned)
            {
                OpenDoors();
                SpawnReward();
                rewardSpawned = true;
            }
        }
    }

    public bool[] wasOpen;
    public virtual void OpenDoors() { }
    public virtual void CloseDoors() { }
}
