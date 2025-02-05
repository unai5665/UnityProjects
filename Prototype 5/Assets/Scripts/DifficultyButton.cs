using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{

    private GameManager gameManager;

    public int difficulty;

    private Button button;
    // Start is called before the first frame update
    void Start()
    {
    button = GetComponent<Button>();
    button.onClick.AddListener(SetDifficulty);
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    void SetDifficulty() {
    Debug.Log(gameObject.name +" was clicked");
    gameManager.StartGame(difficulty);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    
}
