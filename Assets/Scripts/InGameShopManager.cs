using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Vector3[] itemPositions = new Vector3[]
    {
        new Vector3(-3f, 0f, 0f),
        Vector3.zero,
        new Vector3(3f, 0f, 0f)
    };
    private GameObject[] spawnedItems;
    void Start()
    {
        spawnedItems = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            GameObject itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
            GameObject spawnedItem = Instantiate(itemPrefab, transform.position + itemPositions[i], Quaternion.identity, transform);
            var powerUp = spawnedItem.GetComponent<PowerupManager>();
            if (powerUp != null){
                powerUp.isPurchasable = true;
            }
            spawnedItems[i] = spawnedItem;
        }
    }
}
