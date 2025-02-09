using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private float spawnRate = 1.0f;
    public TextMeshProUGUI gameOverText;

    public bool isGameActive;
    public List<GameObject> targets;
    private int score;
    public TextMeshProUGUI scoreText;

    public Button restartButton;

    public GameObject titleScreen;

    
    // Start is called before the first frame update
    void Start()
    {   
    }

    public void StartGame(int difficulty) {
    isGameActive =true;
    score = 0;
    StartCoroutine(SpawnTarget());
    UpdateScore(0);
    titleScreen.gameObject.SetActive(false);
    spawnRate /= difficulty;
    }

    public void GameOver() {

    gameOverText.gameObject.SetActive(true);

    isGameActive =false;

    restartButton.gameObject.SetActive(true);
    
    }
    IEnumerator SpawnTarget() {

    while(isGameActive) {
        yield return new WaitForSeconds(spawnRate);
        int index = Random.Range(0, targets.Count);
        Instantiate(targets[index]); } 
                
        }

       public void UpdateScore(int scoreToAdd) {

       score += scoreToAdd;

       scoreText.text ="Score: "+ score;
       
       }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);}
}
