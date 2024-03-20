using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Input_Terrain : MonoBehaviour
{
    [SerializeField] GameObject i_TerrainPrefab;
    public int i_NumTerrain;
    [SerializeField] GameObject i_TerrainSpawn;
    [SerializeField] GameObject i_InitialSpawn;
    [SerializeField] GameObject i_ProceduralSpawn;
    [SerializeField] GameObject[] i_InitialTerrain;
    [SerializeField] GameObject[] i_ProceduralTerrain;

    void Start()
    {
        int i_RandomTerrain = Random.Range(0, i_InitialTerrain.Length);
        Object_Pool.PreLoad(i_TerrainPrefab, i_NumTerrain);
        i_InitialTerrain[i_RandomTerrain].transform.position = i_InitialSpawn.transform.position;
        i_ProceduralTerrain[i_RandomTerrain].transform.position = i_ProceduralSpawn.transform.position;
    }

    public void RecycleTerrain(GameObject i_Terrain)
    {
        // Reciclar el terreno utilizando el ObjectPool
        Object_Pool.RecycleObject(i_TerrainPrefab, i_Terrain);
    }

    public void NewLevelZone()
    {
        GameObject t_Terrain = Object_Pool.GetObject(i_TerrainPrefab);
        t_Terrain.transform.position = i_TerrainSpawn.transform.position;
    }
}
