using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que atraviesa el trigger tiene el tag "Player".
        if (other.CompareTag("Player"))
        {
            // Llama al método AddScore del GameManager.
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.AddScore(1); // Incrementa el puntaje en 1.
            }
            Debug.Log("Carretera pasada. Puntos sumados.");
        }
    }
}

