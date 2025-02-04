using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Prefabs y referencias a objetos en la escena
    public GameObject balloonPrefab; // Prefab del globo
    public Transform balloonParent;  // Lugar donde se generan los globos
    public Transform dropZone; // Zona donde se ordenan las etiquetas

    // Variables del juego
    public List<string> currentTags = new List<string> { "<a>", "</a>", "<p>", "</p>" };
    private List<string> playerOrder = new List<string>();

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TagContainer tagContainer;

    private float timeLeft = 60f;
    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    // Se llama cuando el jugador hace clic en "Iniciar Juego"
    public void StartGame()
    {   StopAllCoroutines();
        timeLeft = 60f;
        score = 0;
        scoreText.text = " " + score;
        playerOrder.Clear();
        currentTags = new List<string> { "<a>", "</a>", "<p>", "</p>" }; // Inicia con el nivel 1
        SpawnBalloons();  // Solo se llama aquí
        StartTimer();  // Comienza el contador de tiempo
    }

    // Aquí es donde el temporizador empieza a descontar
    void StartTimer()
    {
        // Inicia el tiempo solo cuando realmente se empieza el juego
        StartCoroutine(TimerCountdown());
    }

    IEnumerator TimerCountdown()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Tiempo: " + Mathf.Ceil(timeLeft); // Actualiza el texto
            yield return null; // Espera un frame
        }

        timerText.text = "Tiempo: 0"; // Asegura que el contador se quede en 0 cuando termine
        GameOver(); // Llama al GameOver cuando termine el tiempo
    }

    // Función para generar los globos
    void SpawnBalloons()
    {
        // Limpiar globos anteriores antes de crear nuevos
        foreach (Transform child in balloonParent)
        {
            Destroy(child.gameObject);
        }

        // Generar nuevos globos
        foreach (string tag in currentTags)
        {
            Debug.Log("Asignando etiqueta al globo: " + tag);
            GameObject newBalloon = Instantiate(balloonPrefab, balloonParent);
            newBalloon.GetComponent<Balloon>().htmlTag = tag;
            newBalloon.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, -6f), -5.7f);
            newBalloon.transform.localScale = Vector3.one; // Asegúrate de que la escala sea normal

        }
    }

    // Función para que el jugador capture un globo
    public void CatchBalloon(string tag)
    {
        playerOrder.Add(tag);
        CheckOrder(); // Comprobar si el orden es correcto cada vez que se captura un globo
    }

    // Verifica si el orden de las etiquetas es correcto
    void CheckOrder()
    {
        if (playerOrder.SequenceEqual(currentTags))
        {
            score++;
            scoreText.text = " " + score;
            NextLevel(); // Avanzar al siguiente nivel si el orden es correcto
        }
        else
        {
            playerOrder.Clear(); // Reiniciar si el orden es incorrecto
        }
    }

    // Avanza al siguiente nivel
    public void NextLevel()
    {
        // Aquí definimos las nuevas etiquetas del siguiente nivel
        currentTags = new List<string> { "<div>", "</div>", "<h1>", "</h1>" };
        playerOrder.Clear();
        SpawnBalloons();
    }

    // Fin del juego, se muestra la pantalla de Game Over
    void GameOver()
{
    Debug.Log("Fin del juego. Puntuación: " + score);
    UIManager.Instance.ShowGameOverScreen(score); // Mostrar pantalla de Game Over

    // Asegurar que la pantalla de Game Over está activa antes de limpiar los tags
    if (UIManager.Instance.gameOverScreen.activeSelf)
    {
        CleanDraggableTags();
    }
}


    // Reinicia el juego (se puede usar tanto al inicio como al volver al inicio)
  public void ResetGame()
{
    timeLeft = 60f;
    score = 0;
    scoreText.text = " " + score;
    timerText.text = "Tiempo: 60"; // Asegurar que se muestre 60 antes de iniciar

    playerOrder.Clear();
    currentTags = new List<string> { "<a>", "</a>", "<p>", "</p>" }; // Resetear a nivel 1

    CleanDraggableTags(); // Llamamos a la función que borra todos los tags antes de reiniciar

    UIManager.Instance.ShowStartScreen();
}


// Función para limpiar todos los DraggableTags en el contenedor
void CleanDraggableTags()
{
    if (tagContainer != null)
    {
        foreach (Transform child in tagContainer.tagContainerPanel)
        {
            Destroy(child.gameObject); // Eliminar todos los DraggableTags previos
        }
    }
}
}