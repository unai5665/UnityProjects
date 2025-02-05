using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TagContainer : MonoBehaviour
{
    public List<string> expectedOrder; // Lista con el orden correcto de etiquetas
    public List<Slot> tagSlots; // Slots en el UI donde las etiquetas se colocan
    public GameManager gameManager;
    public GameObject draggableTagPrefab;  // Prefab de la etiqueta arrastrable
    public Transform tagContainerPanel; 

    void Start()
    {
        if (GameManager.Instance != null)
        {
            expectedOrder = new List<string>(GameManager.Instance.currentTags); // Sincroniza con el GameManager
        }
    }

     public void UpdateExpectedOrder(List<string> newOrder)
    {
        expectedOrder = new List<string>(newOrder); // Actualizamos la lista con el nuevo orden
        Debug.Log("Nuevo orden de etiquetas actualizado.");
    }

    

    public void CheckOrder()
    {
        List<string> playerOrder = new List<string>();

        // Recorre los slots y recoge las etiquetas que el jugador ha colocado en ellos
        foreach (Slot slot in tagSlots)
        {
            DraggableTag draggableTag = slot.GetComponentInChildren<DraggableTag>();
            if (draggableTag != null)
            {
                playerOrder.Add(draggableTag.GetTag()); // Obtiene las etiquetas en los espacios
            }
        }

        // Compara el orden de las etiquetas con el esperado
        if (playerOrder.Count == expectedOrder.Count && playerOrder.SequenceEqual(expectedOrder))
        {
            gameManager.NextLevel(); // Si es correcto, pasa de nivel
        }
        else
        {
            Debug.Log("Orden incorrecto, intenta de nuevo.");
            ResetTags();
        }
    }


    public void ResetTags()
    {
        foreach (Transform child in tagContainerPanel)
        {
            Destroy(child.gameObject);  // Eliminar todos los tags previos
        }
        
    }

    public void AddTagToUI(string tag)
    {

        
        // Instanciar el prefab para la etiqueta
        GameObject newTag = Instantiate(draggableTagPrefab, tagContainerPanel);

        // Obtener el componente TMP_Text y asignar la etiqueta
        TMP_Text text = newTag.GetComponent<TMP_Text>();
        if (text != null)
        {
            text.text = tag;  // Asignar el texto de la etiqueta al nuevo objeto
        }

        // Aquí puedes agregar más lógica si quieres hacer que las etiquetas sean arrastrables
        DraggableTag draggableTag = newTag.GetComponent<DraggableTag>();
        if (draggableTag != null)
        {
            // Puedes inicializar otras configuraciones de draggableTag si es necesario
        }
    }
}
