using System.Collections;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform target; // El objeto a seguir (el ciervo)
    private Vector3 offset = new Vector3(0, 40, 0); // Posición relativa de la cámara
    public float followSpeed = 5f; // Velocidad de seguimiento

    private bool isGameStarted = false; // Indica si el juego ha comenzado

    void Start()
    {
        // Verifica si el target está asignado antes de hacer algo
        if (target != null)
        {
            // Obtén el estado del juego desde el GameManager
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                isGameStarted = gameManager.IsGameStarted();
            }

            // Asegúrate de que la cámara esté inicialmente colocada en una posición correcta
            transform.position = target.position + offset; // Coloca la cámara en la posición inicial correcta
        }
        else
        {
            Debug.LogWarning("El target de la cámara no está asignado.");
        }
    }

    void LateUpdate()
    {
        // Si el juego no ha comenzado o el target es nulo, no hacemos nada
        if (!isGameStarted || target == null)
        {
            return;
        }

        // Calcula la nueva posición deseada con el offset
        Vector3 targetPosition = target.position + offset;

        // Suaviza el movimiento hacia la posición deseada
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Asegúrate de que la cámara siempre mire al objetivo
        transform.LookAt(target);
    }

    // Método para activar el seguimiento de la cámara cuando el juego comienza
    public void StartFollowing()
    {
        isGameStarted = true;
    }
}

