using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Threading;

public class ReactionTestChallenge : MonoBehaviour
{
    bool isSquareGreen = false;

    UnityEvent ChallengeCompleted = new UnityEvent();

    IEnumerator ChangeReactionTileColor()
    {
        Debug.Log("Changing color");
        while (true)
        {
            Debug.Log("In loop");
            float waitTime = Random.Range(1f, 5f);
            float timer = 0f;
            while (timer < waitTime)
            {
                timer += Time.deltaTime;
                yield return null;  // Wait for the next frame
            }
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            isSquareGreen = true;
            Debug.Log("Color changed to green");

            timer = 0f;
            while (timer < 0.4f)
            {
                timer += Time.deltaTime;
                yield return null;  // Wait for the next frame
            }

            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            isSquareGreen = false;
            Debug.Log("Color changed to red");
        }
    }

    public UnityEvent GetChallengeCompletedEvent()
    {
        return ChallengeCompleted;
    }
    public bool IsSquareGreen()
    {
        return isSquareGreen;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isSquareGreen)
            {
                Debug.Log("Player passed the reaction test");
                ChallengeCompleted?.Invoke();  // Trigger the event
                Destroy(gameObject);  // Destroy the challenge object after success
            }
            else
            {
                Debug.Log("Player failed the reaction test");
                Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        StartCoroutine(ChangeReactionTileColor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
