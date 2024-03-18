using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Object_Pool : MonoBehaviour
{
    private static Object_Pool instance;

    private static Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();
    private static Dictionary<int, GameObject> parents = new Dictionary<int, GameObject>();
    private static HashSet<int> preloadedPrefabs = new HashSet<int>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Preload objects into the ObjectPool
    public static void PreLoad(GameObject objectToPool, int amount)
    {
        int id = objectToPool.GetInstanceID();

        if (preloadedPrefabs.Contains(id))
        {
            Debug.LogWarning("Prefab already preloaded into the ObjectPool.");
            return;
        }

        GameObject parent = new GameObject();
        parent.name = objectToPool.name + " Pool";
        parents.Add(id, parent);

        pool.Add(id, new Queue<GameObject>());

        for (int i = 0; i < amount; i++)
        {
            CreateObject(objectToPool);
        }

        preloadedPrefabs.Add(id);
    }

    // Create an object in the ObjectPool
    private static void CreateObject(GameObject objectToPool)
    {
        int id = objectToPool.GetInstanceID();
        GameObject go = Instantiate(objectToPool) as GameObject;
        go.transform.SetParent(GetParent(id).transform);
        go.SetActive(false);
        pool[id].Enqueue(go);
    }

    // Retrieve an object from the ObjectPool
    public static GameObject GetObject(GameObject objectToPool)
    {
        int id = objectToPool.GetInstanceID();

        if (pool[id].Count == 0)
        {
            CreateObject(objectToPool);
        }

        GameObject go = pool[id].Dequeue();
        go.SetActive(true);

        return go;
    }

    // Recycle an object into the ObjectPool
    public static void RecycleObject(GameObject objectToPool, GameObject objectToRecycle)
    {
        int id = objectToPool.GetInstanceID();
        pool[id].Enqueue(objectToRecycle);
        objectToRecycle.SetActive(false);
    }

    // Clear all objects from the ObjectPool
    public static void ClearPool()
    {
        foreach (var queue in pool.Values)
        {
            foreach (GameObject obj in queue)
            {
                Destroy(obj);
            }
            queue.Clear();
        }

        pool.Clear();

        foreach (var parent in parents.Values)
        {
            Destroy(parent);
        }

        parents.Clear();
        preloadedPrefabs.Clear();
    }

    // Retrieve the parent object for a given prefab ID
    private static GameObject GetParent(int parentID)
    {
        GameObject parent;
        parents.TryGetValue(parentID, out parent);
        return parent;
    }

}
