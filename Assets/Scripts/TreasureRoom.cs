using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureRoom : Room
{

    // Future logic for spawning treasure and collecting the treasure
    // Waiting on currency system and powerups to be implemented
    
    [SerializeField] GameObject coinPrefab;
    private int coinCount;
    [SerializeField] GameObject chestPrefab;
    [SerializeField] GameObject shopPrefab;

    [SerializeField] float spreadRangeX = 4f;

    [SerializeField] float spreadRangeY = 3f;

    void GenerateChest(){
        var chest = Instantiate(chestPrefab, transform.position+new Vector3(7f,-4f,0), Quaternion.identity);
        chest.transform.parent = transform;
    }

    void GenerateShop(){
        var shop = Instantiate(shopPrefab, transform.position+new Vector3(7f,-4f,0), Quaternion.identity);
        shop.transform.parent = transform;
    }

    void GenerateCoins(){
        coinCount = UnityEngine.Random.Range(1, 5);
        for (int i = 0; i < coinCount; i++){
            var coin = Instantiate(coinPrefab, transform.position + new Vector3(UnityEngine.Random.Range(-spreadRangeX, spreadRangeX), UnityEngine.Random.Range(-spreadRangeY, spreadRangeY), 0), Quaternion.identity);
            coin.transform.parent = transform;
        }
    }

    void GenerateReward(){
        float randomValue = UnityEngine.Random.value;
        if (randomValue<0.33f){
            GenerateCoins();
            return;
        }
        if (randomValue<0.66f){
            GenerateChest();
            return;
        }
        GenerateShop();
        
    }
    void Start()
    {
        GenerateReward();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
