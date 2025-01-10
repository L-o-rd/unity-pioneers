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

    GameObject objectToSpawn = poolDictionary[tag].Dequeue();

    // Reset obiectul (starea specifică trebuie să fie restaurată)
    RicochetBullet ricochetBullet = objectToSpawn.GetComponent<RicochetBullet>();
    if (ricochetBullet != null)
    {
        ricochetBullet.ResetBullet(resetVelocity: true, resetRicochetCount: true); // Asigură-te că bullet-ul este complet resetat
    }

    // Reutilizare (activează și adaugă înapoi în coadă)
    objectToSpawn.SetActive(true);
    poolDictionary[tag].Enqueue(objectToSpawn);

    return objectToSpawn;
}


    public void ReturnPooledObject(GameObject obj)
{
    if (obj != null && poolDictionary.ContainsKey(obj.tag))
    {
        obj.SetActive(false);

        // Reset Bullet state when returned to the pool
        RicochetBullet bullet = obj.GetComponent<RicochetBullet>();
        if (bullet != null)
        {
            bullet.ResetBullet(resetVelocity: true, resetRicochetCount: true); // Reset ricochet count and velocity
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
