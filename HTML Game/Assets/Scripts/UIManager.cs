using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject startScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreTextFinal; // Texto para mostrar la puntuación final

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ShowStartScreen();  // Iniciar en la pantalla de inicio
    }

    // Muestra la pantalla de inicio y oculta la de GameOver
    public void ShowStartScreen()
    {
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    // Llama a GameManager para iniciar el juego
    private void StartGame()
    {
        startScreen.SetActive(false);  // Oculta la pantalla de inicio
        GameManager.Instance.StartGame();  // Llama al método StartGame en GameManager
    }

    // Muestra la pantalla de GameOver y muestra la puntuación final
    public void ShowGameOverScreen(int score)
    {
        gameOverScreen.SetActive(true);
        scoreTextFinal.text = " " + score;
    }

    // Regresa a la pantalla de inicio y reinicia el juego
    public void ReturnToStart()
    {
        gameOverScreen.SetActive(false);
        ShowStartScreen();
        GameManager.Instance.ResetGame();  // Reinicia el juego desde GameManager
    }
}
