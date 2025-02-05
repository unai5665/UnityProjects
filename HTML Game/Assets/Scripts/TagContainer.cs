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
        InitializeSlots();
    }

    public void UpdateExpectedOrder(List<string> newOrder)
{
    // Verificar el contenido antes de la actualización
    Debug.Log("Antes de la actualización, expectedOrder: " + string.Join(", ", expectedOrder));

    // Limpiar el expectedOrder y agregar las nuevas etiquetas
    expectedOrder.Clear();
    expectedOrder.AddRange(newOrder);

    // Imprimir el nuevo orden para ver si la actualización funcionó
    Debug.Log("Nuevo orden de etiquetas: " + string.Join(", ", expectedOrder));

    // Limpiar los slots y reiniciar
    ClearSlots();
    InitializeSlots();
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
        
            ResetTags();
        }
    }

    public bool AllSlotsFilled()
    {
        foreach (Slot tag in tagSlots)
        {
            if (tag == null) // Si hay un slot sin etiqueta, aún no está completo
            {
                return false;
            }
        }
        return true;
    }

    public void ClearSlots()
    {
        foreach (Slot slot in tagSlots)
        {
            DraggableTag draggableTag = slot.GetComponentInChildren<DraggableTag>();
            if (draggableTag != null)
            {
                Destroy(draggableTag.gameObject); // Eliminar la etiqueta dentro del slot
            }
        }
    }

    public void InitializeSlots()
    {
        for (int i = 0; i < tagSlots.Count; i++)
        {
            if (i < expectedOrder.Count)  // Asegura que no haya error si hay más slots que etiquetas
            {
                tagSlots[i].expectedTag = expectedOrder[i]; // Asigna la etiqueta esperada al slot
            
            }
            else
            {
                tagSlots[i].expectedTag = null;  // Si hay más slots que etiquetas, vacíalos
            }
        }
    }







    public void ResetTags()
    {
        foreach (Slot slot in tagSlots)
        {
            DraggableTag draggableTag = slot.GetComponentInChildren<DraggableTag>(); // Obtener la etiqueta dentro del slot

            if (draggableTag != null) 
            {
                draggableTag.ResetPosition(); // Devolver solo las etiquetas que estén en un slot
            }
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
