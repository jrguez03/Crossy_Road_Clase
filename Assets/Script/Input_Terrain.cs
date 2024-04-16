using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Input_Terrain : MonoBehaviour
{
    [SerializeField] GameObject i_InitialSpawn;
    [SerializeField] GameObject i_Medio0;
    [SerializeField] GameObject i_Medio1;
    [SerializeField] GameObject i_Medio2;
    [SerializeField] GameObject[] i_InitialTerrain;
    [SerializeField] GameObject[] i_MediumTerrain;
    [SerializeField] GameObject[] i_MediumTerrain1;
    [SerializeField] GameObject[] i_MediumTerrain2;

    void Start()
    {
        int i_RandomTerrain = Random.Range(0, i_InitialTerrain.Length);
        i_InitialTerrain[i_RandomTerrain].transform.position = i_InitialSpawn.transform.position;
        int i_RandomMedium = Random.Range(0, i_MediumTerrain.Length);
        i_MediumTerrain[i_RandomMedium].transform.position = i_Medio0.transform.position;
        int i_RandomMedium1 = Random.Range(0, i_MediumTerrain1.Length);
        i_MediumTerrain1[i_RandomMedium1].transform.position = i_Medio1.transform.position;
        int i_RandomMedium2 = Random.Range(0, i_MediumTerrain2.Length);
        i_MediumTerrain2[i_RandomMedium2].transform.position = i_Medio2.transform.position;
    }
}
