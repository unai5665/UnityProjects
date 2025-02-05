using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TagContainer tagContainer;

    // Prefabs y referencias a objetos en la escena
    public GameObject balloonPrefab; // Prefab del globo
    public Transform balloonParent;  // Lugar donde se generan los globos
    public Transform dropZone; // Zona donde se ordenan las etiquetas

    // Variables del juego
    public List<string> currentTags = new List<string>();
    private List<string> playerOrder = new List<string>();

    private List<List<string>> levels = new List<List<string>>(){
        new List<string> { "<a>", "<p>", "</p>", "</a>" },         // Nivel 1
        new List<string> { "<div>", "<b>", "</b>", "</div>" },     // Nivel 2
        new List<string> { "<section>", "<i>", "</i>", "</section>" } // Nivel 3
    };




    private float timeLeft = 60f;
    private int score = 0;

    private int currentLevel = 0;
    

    void Awake()
    {
        Instance = this;
    }

    // Se llama cuando el jugador hace clic en "Iniciar Juego"
    public void StartGame()
    {  
         StopAllCoroutines();

        timeLeft = 60f;

        score = 0;

        currentLevel = 0;

        scoreText.text = " " + score;

        playerOrder.Clear();

        currentTags = levels[currentLevel];

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
            GameObject newBalloon = Instantiate(balloonPrefab, balloonParent);

            newBalloon.GetComponent<Balloon>().htmlTag = tag;

            newBalloon.transform.position = new Vector3(Random.Range(-2f, 3f), Random.Range(-3f, -6f), -5.7f);

            newBalloon.transform.localScale = Vector3.one; // Asegúrate de que la escala sea normal

        }
    }

    // Función para que el jugador capture un globo
    public void CatchBalloon(string tag)
    {
        playerOrder.Add(tag);
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
    if (currentLevel < levels.Count - 1)
    {
        currentLevel++;
        score++;
        scoreText.text = " " + score;
        timeLeft = 60f;
        currentTags = levels[currentLevel];

        // Verificar que currentTags tiene el orden correcto
        Debug.Log("Nivel " + currentLevel + " - Orden de etiquetas actual: " + string.Join(", ", currentTags));

        if (tagContainer != null)
        {
            // Asegurarse de que se llama a UpdateExpectedOrder correctamente
            Debug.Log("Llamando a UpdateExpectedOrder con el nuevo orden.");
            tagContainer.UpdateExpectedOrder(currentTags);  // Aquí se llama a la actualización
        }

        Debug.Log("Avanzando al siguiente nivel.");
        playerOrder.Clear();
        SpawnBalloons();
    }
    else
    {
        Debug.Log("No hay más niveles. Fin del juego.");
        GameOver();
    }
}



    // Fin del juego, se muestra la pantalla de Game Over
    void GameOver()
    {

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

        Debug.Log("Etiquetas actuales para el nivel " + currentLevel + ": " + string.Join(", ", levels[currentLevel]));

        currentTags = levels[currentLevel]; // Resetear a nivel 1
        tagContainer.UpdateExpectedOrder(currentTags);

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