using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawn_Props : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn;
    public Transform spawnPoint;
    public List<GameObject> activeInstances = new List<GameObject>();

    public void SpawnRandomPrefab()
    {
        // Verificar si hay al menos un prefab en la lista
        if (prefabsToSpawn.Count == 0)
        {
            Debug.LogWarning("No hay prefabs en la lista para spawnear.");
            return;
        }

        // Generar un índice aleatorio dentro del rango de la lista de prefabs
        int randomIndex = Random.Range(0, prefabsToSpawn.Count);

        // Obtener el prefab aleatorio
        GameObject randomPrefab = prefabsToSpawn[randomIndex];

        // Verificar si ya hay una instancia activa del prefab seleccionado
        if (activeInstances.Contains(randomPrefab))
        {
            // Si ya hay una instancia activa, crear una nueva instancia
            GameObject newPrefabInstance = Instantiate(randomPrefab, spawnPoint.position, Quaternion.identity);
            activeInstances.Add(newPrefabInstance);
        }
        else
        {
            // Si no hay una instancia activa, simplemente activarla
            randomPrefab.SetActive(true);
            randomPrefab.transform.position = spawnPoint.position;
            activeInstances.Add(randomPrefab);
        }
    }
}
