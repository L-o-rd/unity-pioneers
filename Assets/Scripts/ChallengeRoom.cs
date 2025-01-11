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
 
    void Start()
    {
        SpawnChallenge();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
