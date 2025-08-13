using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Pooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public int id;
        public GameObject prefab;
        public int size;
    }

    [Inject]
    private DiContainer diContainer;

    [SerializeField]
    private List<Pool> pools;

    [SerializeField]
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = diContainer.InstantiatePrefab(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.id.ToString(), objectPool);
        }
    }

    public GameObject Spawn(string tag, Vector3 position, Vector3 rotation)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            GameObject obj = poolDictionary[tag].Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = Quaternion.Euler(rotation);
            obj.SetActive(true);
            poolDictionary[tag].Enqueue(obj);
            return obj;
        }
        return null;
    }
}
