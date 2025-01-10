using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [System.Serializable]
    public class Pool
    {
        public string tag;           // Tag-ul pentru obiecte
        public GameObject prefab;    // Prefabricatul obiectului
        public int size;             // Numărul de obiecte din pool
    }

    public List<Pool> pools;        // Lista de pool-uri pentru obiecte
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
                obj.tag = pool.tag; // Asigură-te că obiectele au tag-ul corect
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist!");
            return null;
        }

        // Search for the first inactive object in the pool
        foreach (var pooledObject in poolDictionary[tag])
        {
            if (!pooledObject.activeInHierarchy)
            {
                pooledObject.SetActive(true);
                Debug.Log($"Bullet with tag {tag} activated.");
                return pooledObject;
            }
        }

        // If no inactive objects are found, create a new one
        Debug.LogWarning($"No inactive objects available in pool for tag {tag}. Increasing pool size.");
        Pool pool = pools.Find(p => p.tag == tag);
        if (pool != null)
        {
            GameObject newObj = Instantiate(pool.prefab);
            newObj.SetActive(true);
            newObj.tag = tag;
            poolDictionary[tag].Enqueue(newObj);
            Debug.Log($"New bullet created for tag {tag}.");
            return newObj;
        }

        Debug.LogError($"Failed to find or create an object for tag {tag}.");
        return null;
    }

    public void ReturnPooledObject(GameObject obj)
{
    if (obj != null && poolDictionary.ContainsKey(obj.tag))
    {
        obj.SetActive(false);

        // Reset Bullet state when returned to the pool, but avoid resetting ricochet count
        RicochetBullet bullet = obj.GetComponent<RicochetBullet>();
        if (bullet != null)
        {
            bullet.ResetBullet(resetVelocity: false, resetRicochetCount: false); // Skip resetting ricochet count
        }

        poolDictionary[obj.tag].Enqueue(obj);
        Debug.Log($"Bullet with tag {obj.tag} returned to pool.");
    }
    else
    {
        Debug.LogError($"Trying to return object {obj?.name} with an invalid or missing tag.");
    }
}

}
