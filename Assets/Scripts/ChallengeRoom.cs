using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChallengeRoom : Room
{
    // Start is called before the first frame update

    [SerializeField] GameObject chestPrefab;

    [SerializeField] GameObject reactionTestPrefab;

    [SerializeField] float delay=2f;
    [SerializeField] GameObject enemyPrefab;

    GameObject challenge;

    private void SpawnReactionTest()
    {
        GameObject challenge = Instantiate(reactionTestPrefab, transform.position+new Vector3(-7f,3f,0f), Quaternion.identity);
        ReactionTestChallenge reactionTest = challenge.GetComponent<ReactionTestChallenge>();
        if (reactionTest != null){
            reactionTest.GetChallengeCompletedEvent().AddListener(OnChallengeComplete);
        }
    }
    
    private void OnChallengeComplete()
    {
        Invoke("SpawnChest", delay);
    }
    private void SpawnChallenge()
    {
        //future logic for spawning challenge
        Debug.Log("Challenge spawned");
        SpawnReactionTest();
        
    }

    private void SpawnChest()
    {
        Instantiate(chestPrefab, transform.position+new Vector3(UnityEngine.Random.Range(-8f,8f),UnityEngine.Random.Range(-5f,5f),0), Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            SpawnChallenge();
        }
    }

    int Enemies { get; set; } = 0;
 
    void Start()
    {
        SpawnChallenge();
        GameObject[] points = GameObject.FindGameObjectsWithTag("EnemySpawn");
        int elems = RNGManager.Instance.rng.Next(1, points.Length + 1);
        int n = points.Length;
        while (n > 1)
        {
            n--;
            int k = RNGManager.Instance.rng.Next(n + 1);
            GameObject value = points[k];
            points[k] = points[n];
            points[n] = value;
        }

        Enemies = elems;
        for (int i = 0; i < elems; ++i)
        {
            Instantiate(enemyPrefab, points[i].transform);
        }

        wasOpen = new bool[4];
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OpenDoors()
    {
        if (wasOpen[0]) topDoor.SetActive(true);
        if (wasOpen[1]) bottomDoor.SetActive(true);
        if (wasOpen[2]) leftDoor.SetActive(true);
        if (wasOpen[3]) rightDoor.SetActive(true);
    }

    public override void CloseDoors() {
        wasOpen[0] = topDoor.activeInHierarchy;
        wasOpen[1] = bottomDoor.activeInHierarchy;
        wasOpen[2] = leftDoor.activeInHierarchy;
        wasOpen[3] = rightDoor.activeInHierarchy;

        if (Enemies > 0)
        {
            CloseDoor(Vector2Int.up);
            CloseDoor(Vector2Int.down);
            CloseDoor(Vector2Int.left);
            CloseDoor(Vector2Int.right);
        }
    }
}
