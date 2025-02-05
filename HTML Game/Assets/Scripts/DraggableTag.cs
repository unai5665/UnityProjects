using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Animations;

public class DraggableTag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private Canvas canvas;  // Necesario para la conversión de coordenadas

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)

            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        originalPosition = rectTransform.anchoredPosition;
        
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Al empezar a arrastrar, hacer el objeto un poco transparente y permitir arrastrarlo
        canvasGroup.alpha = 0.6f;

        canvasGroup.blocksRaycasts = false;

        transform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(

            canvas.GetComponent<RectTransform>(), 

            eventData.position, 

            eventData.pressEventCamera, 
            
            out Vector2 localPointerPosition)) {
            rectTransform.anchoredPosition = localPointerPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        
        canvasGroup.blocksRaycasts = true;

        if (transform.parent == canvas.transform) // Si no está en un slot válido
        {
            ResetPosition();
        }
    }

    // Resetea la posición original si la etiqueta no se suelta en el lugar correcto
    public void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
    }

    public string GetTag()
    {
        return GetComponent<TMP_Text>().text;
    }
}
