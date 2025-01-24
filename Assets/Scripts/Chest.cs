using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private float bonusDifficulty = 1.0f;

    [SerializeField]
    protected AudioClip chestOpenSound;

    public bool testMode;

    // Test hook for controlling randomness
    public System.Func<float> RandomValueProvider = () => UnityEngine.Random.value;


    // Allow dependency injection for testing
    public void Initialize(RoomManagerMock roomManagerMock)
    {
        bonusDifficulty = roomManagerMock.GetDifficulty();
    }

    // Start is called before the first frame update
     void Start()
    {
        bonusDifficulty = GameObject.Find("RoomManager").GetComponent<RoomManager>().GetDifficulty();
    }
    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Player")
        {
            float randomValue = RandomValueProvider();
            if (randomValue<(0.3f)*bonusDifficulty){
                Debug.Log("Player was damaged by a trap chest");
                if (gameObject.name == "MockChest")
                {
                    Debug.Log("MockChest is damaging player");
                    other.gameObject.GetComponent<MockPlayerStats>().TakeDamage(20 * bonusDifficulty);
                }
                else
                {
                    other.gameObject.GetComponent<PlayerStats>().TakeDamage(20*bonusDifficulty);
                }

            }
            else{
                Debug.Log("Player opened a chest");
                if (gameObject.name == "MockChest")
                {
                    other.gameObject.GetComponent<MockPlayerStats>().addCoins(60 * bonusDifficulty);
                }
                else
                {
                    other.gameObject.GetComponent<PlayerStats>().addCoins(60*bonusDifficulty);
                }
                SoundManager.Instance.PlaySound(chestOpenSound);
            }
            Debug.Log("Chest was opened, now destroying chest");
            if (testMode)
                DestroyImmediate(gameObject); // Use immediate destruction in editor or tests
            else
                Destroy(gameObject); // Use normal destruction in runtime
        }
    }
}
