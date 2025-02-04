using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject startScreen;  
    public GameObject gameOverScreen;
    public GameObject victoryScreen;  
    public GameObject scoreScreen;    

    private bool isGameOver = false; 

    void Start()
    {
        Time.timeScale = 0; 
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        victoryScreen.SetActive(false);
        scoreScreen.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startScreen.SetActive(false);
        scoreScreen.SetActive(true);
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }
    }

    public void Victory()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Time.timeScale = 0;
            victoryScreen.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToStartScreen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene"); 
    }

    public bool IsGameStarted()  // <-- AQUÍ ESTÁ EL MÉTODO QUE FALTABA
    {
        return Time.timeScale > 0;
    }
}
