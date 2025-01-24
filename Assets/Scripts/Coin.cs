using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int value;

    [SerializeField]
    private AudioClip coinSound;
    public bool testMode;
    public void Start()
    {
        value=UnityEngine.Random.Range(10, 25);
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Player"){
            SoundManager.Instance.PlaySound(coinSound);
            if (gameObject.name == "MockCoin")
            {
                other.GetComponent<MockPlayerStats>().addCoins(value);
            }
            else
            {
                other.GetComponent<PlayerStats>().addCoins(value);
            }
            Debug.Log("Player picked up "+value+" coins");
            if (!testMode)
                Destroy(gameObject);
            else
                DestroyImmediate(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
