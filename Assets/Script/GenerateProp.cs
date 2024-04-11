using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateProp : MonoBehaviour
{
    public Spawn_Props gp_SpawnProps;
    public Collider m_Trigger; // El trigger donde se detectarán las salidas del objeto con el tag "prop"

    private void Start()
    {
        gp_SpawnProps.SpawnRandomPrefab();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Prop"))
        {
            // Si el objeto que sale del trigger es un "prop" y es el mismo trigger seleccionado, generar un nuevo objeto
            gp_SpawnProps.SpawnRandomPrefab();
        }
    }
}
