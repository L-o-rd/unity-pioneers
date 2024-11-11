using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
  public static ObjectPool instance;

  [SerializeField] private GameObject clonePrefab; // Replace with your player clone prefab
  [SerializeField] private Transform parentTransform; 
  private List<GameObject> pooledObjects = new List<GameObject>();
  [SerializeField] private int amountToPool = 20;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  void Start()
  {
    // Initialize the pool with inactive player clone instances
    for (int i = 0; i < amountToPool; i++)
    {
      GameObject obj = Instantiate(clonePrefab, parentTransform);
      obj.SetActive(false); // Deactivate the clone initially
      pooledObjects.Add(obj); // Add to the pool
    }
  }

  public GameObject GetPooledObject()
  {
    // Find an inactive object in the pool and return it
    foreach (GameObject obj in pooledObjects)
    {
      if (!obj.activeInHierarchy)
      {
        return obj; // Return inactive object
      }
    }

    // Optional: Expand the pool if all objects are active
    GameObject newObj = Instantiate(clonePrefab, parentTransform);
    newObj.SetActive(false);
    pooledObjects.Add(newObj); // Add the new clone to the pool
    return newObj; // Return the new object
  }
}