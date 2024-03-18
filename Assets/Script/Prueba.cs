using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Prueba : MonoBehaviour
{
    [SerializeField]
    public GameObject m_TerrainPrefab;
    public int m_Nterrain;
    public GameObject m_TerrainSpawn;

    void Start()
    {
        Object_Pool.PreLoad(m_TerrainPrefab, m_Nterrain);

    }

    public void RecycleTerrain(GameObject m_Terrain)
    {
        // Reciclar el terreno utilizando el ObjectPool
        Object_Pool.RecycleObject(m_TerrainPrefab, m_Terrain);
    }

    public void NewLevelZone()
    {
        GameObject m_Terrain = Object_Pool.GetObject(m_TerrainPrefab);
        m_Terrain.transform.position = m_TerrainSpawn.transform.position;
    }
}
