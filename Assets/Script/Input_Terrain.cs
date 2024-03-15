using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Terrain : MonoBehaviour
{
    Terrain_Pool t_Stack;

    [SerializeField] GameObject t_SpawnTerrain;

    bool t_SeRecicla = false;

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
            Debug.Log("Entrando");
            GameObject terreno = t_Stack.ObtenerObjeto();

            terreno.transform.position = t_SpawnTerrain.transform.position;
        }
    }

    private void OnBecameInvisible()
    {
        if(!t_SeRecicla)
        {
            Terrain_Pool.instance.DevolverObjeto(this.gameObject);
            t_SeRecicla = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
