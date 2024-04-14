using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycle_Prop_Pool : MonoBehaviour
{
    public Spawn_Props rp_SpawnProps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prop"))
        {
            // Si el objeto que entra en el otro collider es un "prop", añadirlo a la lista de prefabs a spawnear
            rp_SpawnProps.inactiveObjects.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
