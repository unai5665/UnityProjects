using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // Prefabs de los coches.
    public Transform[] spawnPoints; // Puntos de aparición.
    public float spawnInterval = 2f; // Tiempo entre spawns.

    void Start()
    {
        InvokeRepeating("SpawnCar", 0f, spawnInterval);
    }

    void SpawnCar()
    {
        if (carPrefabs.Length == 0 || spawnPoints.Length == 0)
        { 
            Debug.LogError("No se han asignado prefabs o puntos de spawn.");
            return;
        }

        // Selecciona un prefab aleatorio.
        int randomCarIndex = Random.Range(0, carPrefabs.Length);
        GameObject carPrefab = carPrefabs[randomCarIndex];

        // Selecciona un punto de spawn aleatorio.
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnIndex];

        // Instancia el coche en el punto seleccionado.
        GameObject car = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);

        // Asegúrate de que se instanció correctamente.
        if (car == null)
        {
            Debug.LogError("El coche no se pudo instanciar correctamente.");
        }
    }
}
