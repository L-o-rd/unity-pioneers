using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [System.Serializable]
    public class Pool
    {
        public string tag; // A tag to identify the bullet type
        public GameObject prefab; // The prefab for this bullet type
        public int size; // Initial size of the pool
    }

    public List<Pool> pools; // List of bullet pools
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetPooledObject(string tag)
{
    if (!poolDictionary.ContainsKey(tag))
    {
        Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
        return null;
    }

    GameObject objectToSpawn = poolDictionary[tag].Dequeue();

    objectToSpawn.SetActive(true);

    // Ensure that any bullet-specific state (e.g., ricochet count) is reset
    RicochetBullet ricochetBullet = objectToSpawn.GetComponent<RicochetBullet>();
    if (ricochetBullet != null)
    {
        ricochetBullet.ResetBullet(); // Reset the ricochet bullet state
    }

    poolDictionary[tag].Enqueue(objectToSpawn);

    return objectToSpawn;
}

}
