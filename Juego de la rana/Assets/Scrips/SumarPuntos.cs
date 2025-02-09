using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Necesario para manipular TextMeshProUGUI

public class SpaceBarCounter : MonoBehaviour
{
    public TMP_Text scoreText; // Referencia al componente TextMeshProUGUI donde se mostrará el contador
    private int spaceCount = 0; // Contador que se incrementará cada vez que se presione la barra espaciadora

    private string baseText = ""; // Para almacenar el texto base (sin el número)

    void Start()
    {
        // Asegúrate de que el contador de espacios comience en cero
        if (scoreText != null)
        {
            baseText = scoreText.text; // Guardar el texto base (por ejemplo, "Puntos: ")
        }
        UpdateScoreText();
    }

    void Update()
    {
        // Verifica si la barra espaciadora ha sido presionada
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceCount++; // Incrementa el contador cada vez que presionas la barra espaciadora
            UpdateScoreText(); // Actualiza el texto en pantalla con el nuevo valor
        }
    }

    // Función para actualizar el texto en la UI
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = baseText + spaceCount; // Actualiza solo el número, manteniendo el texto base
        }
    }
}
