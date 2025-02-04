using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Balloon : MonoBehaviour
{
    public string htmlTag; // La etiqueta HTML que contiene este globo
    public TMP_Text textMeshPro;

    private float speed = 0.5f; // Velocidad de subida

void Start()
{
    textMeshPro = GetComponentInChildren<TMP_Text>();
    if (textMeshPro != null && !string.IsNullOrEmpty(htmlTag))
    {
        textMeshPro.text = htmlTag;
    }

    // Posición inicial directamente sin necesidad de SetRandomPosition()
    transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-6f, -3f), -5.7f);
}





    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);  // El globo sube

        // Reaparece si se sale de la pantalla
        if (transform.position.y > 5f)
        {
            Respawn();
        }
    }

   

    void Respawn()
    {
        // Cuando el globo sale de la pantalla, regresa a la parte inferior con una posición aleatoria en el eje X
        transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range( -3f, -6f), -5.7f);
    }

    // Detecta clic en el globo
    private void OnMouseDown()
{
    Debug.Log("Globo clickeado: " + htmlTag);  // Esto debería aparecer en la consola cuando haces clic
    if (GameManager.Instance != null)
    {
        GameManager.Instance.CatchBalloon(htmlTag); // Enviar la etiqueta al GameManager
        Destroy(gameObject);  // Elimina el globo correctamente después de hacer clic

        // Agregar la etiqueta al UI como un objeto arrastrable
        TagContainer tagContainer = FindObjectOfType<TagContainer>();  // Obtén el TagContainer en la escena
        if (tagContainer != null)
        {
            tagContainer.AddTagToUI(htmlTag);  // Llamar al método para agregar la etiqueta en la UI
        }
    }
}

}
