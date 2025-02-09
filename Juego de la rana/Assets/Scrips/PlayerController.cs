using UnityEngine;       // For GameObject, AudioClip, Collision
using UnityEngine.UI;     // For Text
using System.Collections; // For IEnumerator

public class PlayerController : MonoBehaviour
{
    private float jumpDistance = 30f; // Distancia que avanza en cada salto.
    private float jumpCooldown = 0f; // Tiempo entre saltos.
    private bool canJump = true;

    public GameObject explosionEffect; // Efecto de explosión que se muestra al ser atropellado.

    public float victoryPoint = 200f;
    

    void Update()
    {
        // Verifica si el jugador presiona la tecla de espacio y puede saltar.
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            StartCoroutine(JumpForward());
        }

        if (transform.position.z >= victoryPoint)  // Suponiendo que el eje Z determina el progreso
        {
            GameManager gameManager = FindObjectOfType<GameManager>();  // Correcto, se obtiene la instancia del GameManager
            if (gameManager != null)
            {
                gameManager.Victory();  // Llama al método Victory() en la instancia de GameManager
            }
        }
    }

    private IEnumerator JumpForward()
    {
        canJump = false;

        // Movimiento suave hacia adelante.
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = transform.position + Vector3.forward * jumpDistance;
        float elapsedTime = 0f;
        float jumpDuration = 0.2f; // Duración del salto.

        while (elapsedTime < jumpDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegura que llegue exactamente a la posición final.
        transform.position = targetPosition;

        yield return new WaitForSeconds(jumpCooldown); // Tiempo de espera antes del próximo salto.
        canJump = true;
    }

    // Verifica si el jugador colisiona con un coche.
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            StartCoroutine(HandleDeath());
        }
    }

    private IEnumerator HandleDeath()
    {

        // Muestra el efecto de explosión si está asignado.
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(0.2f); // Espera un poco antes de destruir al ciervo.

        // Notifica al GameManager que el juego terminó.
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.GameOver();
        }

        Destroy(gameObject); // Destruye el ciervo.
    }
}
