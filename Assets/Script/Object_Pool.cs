using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Object_Pool : MonoBehaviour
{
    [System.Serializable]
    public struct m_PrefabData
    {
        [SerializeField]
        public GameObject m_Prefab;

        public int m_PreloadObjects;
    }

    public m_PrefabData[] m_PrefabsToPool;
    public Transform m_SpawnPoint;

    private Dictionary<int, Queue<GameObject>> PrefabsDictionary = new Dictionary<int, Queue<GameObject>>();

    void Start()
    {
        PreLoadPrefabs();
    }

    void PreLoadPrefabs()
    {
        foreach (m_PrefabData m_PrefabData in m_PrefabsToPool)
        {
            int id = m_PrefabData.m_Prefab.GetInstanceID();

            if (!PrefabsDictionary.ContainsKey(id))
            {
                PrefabsDictionary.Add(id, new Queue<GameObject>());
            }

            for (int i = 0; i < m_PrefabData.m_PreloadObjects; i++)
            {
                GameObject obj = Instantiate(m_PrefabData.m_Prefab);
                obj.SetActive(false);
                PrefabsDictionary[id].Enqueue(obj);
            }
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        int id = prefab.GetInstanceID();

        if (PrefabsDictionary.ContainsKey(id) && PrefabsDictionary[id].Count > 0)
        {
            GameObject m_PrefabCopy = PrefabsDictionary[id].Dequeue();
            m_PrefabCopy.transform.position = m_SpawnPoint.position;
            m_PrefabCopy.SetActive(true);
            return m_PrefabCopy;
        }

        return null;
    }

    public void RecycleObject(GameObject m_PrefabCopy)
    {
        m_PrefabCopy.SetActive(false);
        int id = m_PrefabCopy.GetInstanceID();
        PrefabsDictionary[id].Enqueue(m_PrefabCopy);
    }
}
