using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Terrain : MonoBehaviour
{
    Terrain_Pool t_Stack;

    [SerializeField] GameObject t_SpawnTerrain;

    // Start is called before the first frame update
    void Awake()
    {
        t_Stack = GetComponent<Terrain_Pool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject terreno = t_Stack.ObtenerObjeto();

            terreno.transform.position = t_SpawnTerrain.transform.position;
        }
    }

    private void OnBecameVisible()
    {
        
    }
}
