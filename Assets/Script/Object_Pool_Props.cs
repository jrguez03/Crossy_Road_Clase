using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Object_Pool_Props : MonoBehaviour
{
    public List<GameObject> o_PooledObjects;
    public List<GameObject> o_PropPrefabs;
    public int o_CantidadObjects; 

    void Start()
    {
        o_PooledObjects = new List<GameObject>();
        for (int i = 0; i < o_CantidadObjects; i++)
        {
            GameObject obj = Instantiate(o_PropPrefabs[Random.Range(0, o_PropPrefabs.Count)]);
            obj.SetActive(false);
            o_PooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < o_PooledObjects.Count; i++)
        {
            if (!o_PooledObjects[i].activeInHierarchy)
            {
                return o_PooledObjects[i];
            }
        }

        // Si no hay objetos disponibles, creamos uno nuevo y lo añadimos a la pool
        GameObject obj = Instantiate(o_PropPrefabs[Random.Range(0, o_PropPrefabs.Count)]);
        obj.SetActive(false);
        o_PooledObjects.Add(obj);
        return obj;
    }
}
