using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawn_Props : MonoBehaviour
{
    public List<GameObject> objectsList;
    public List<GameObject> inactiveObjects = new List<GameObject>();
    [SerializeField] GameObject spawnPoint;
    private GameObject activeObject;

    private void Start()
    {
        foreach (GameObject prefab in objectsList)
        {
            prefab.SetActive(false);
            inactiveObjects.Add(prefab);
        }

        SpawnRandomPrefab();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == activeObject)
        {
            SpawnRandomPrefab();
        }
    }

    public void SpawnRandomPrefab()
    {
        if (inactiveObjects.Count > 0)
        {
            int randomIndex = Random.Range(0, inactiveObjects.Count);

            activeObject = inactiveObjects[randomIndex];
            activeObject.SetActive(true);

            activeObject.transform.position = spawnPoint.transform.position;

            inactiveObjects.RemoveAt(randomIndex);
        }

        /*// Verificar si hay al menos un prefab en la lista
        if (objectsList.Count == 0)
        {
            Debug.LogWarning("No hay prefabs en la lista para spawnear.");
            return;
        }

        // Generar un índice aleatorio dentro del rango de la lista de prefabs
        int randomIndex = Random.Range(0, objectsList.Count);

        // Obtener el prefab aleatorio
        GameObject randomPrefab = objectsList[randomIndex];

        // Verificar si ya hay una instancia activa del prefab seleccionado
        if (inactiveObjects.Contains(randomPrefab))
        {
            // Si ya hay una instancia activa, crear una nueva instancia
            GameObject newPrefabInstance = Instantiate(randomPrefab, spawnPoint.transform.position, Quaternion.identity);
            inactiveObjects.Add(newPrefabInstance);
        }
        else
        {
            // Si no hay una instancia activa, simplemente activarla
            randomPrefab.SetActive(true);
            randomPrefab.transform.position = spawnPoint.transform.position;
            inactiveObjects.Add(randomPrefab);
        }*/
    }
}
