using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class boxDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Box Settings")]
    public string boxSize;
    
    [Header("UI References")]
    public Canvas canvas;
    public RectTransform workTable;
    public RectTransform boxPlacementArea;
    public Image placedBox;

    private RectTransform canvasRect;

    private void Awake()
    {
        canvasRect = canvas.GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        placedBox.gameObject.SetActive(true);
        
        // move box on main canvas so can follow finger even if it moves off worktable
        placedBox.transform.SetParent(canvas.transform, true);

        RectTransform boxRect = placedBox.rectTransform;
        boxRect.anchorMin = new Vector2(0.5f, 0.5f);
        boxRect.anchorMax = new Vector2(0.5f, 0.5f);
        boxRect.pivot = new Vector2(0.5f, 0.5f);
        
        placedBox.transform.SetAsLastSibling();

        placedBox.raycastTarget = false;
        
        moveBox(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        moveBox(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // check if finger was released in box placement area
        bool validDrop = RectTransformUtility.RectangleContainsScreenPoint(
            boxPlacementArea,
            eventData.position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera
        );

        if (validDrop)
        {
            // put box under worktable and snap into place
            placedBox.transform.SetParent(workTable, false);

            RectTransform boxRect = placedBox.rectTransform;
            boxRect.anchorMin = new Vector2(0.5f, 0.5f);
            boxRect.anchorMax = new Vector2(0.5f, 0.5f);
            boxRect.pivot = new Vector2(0.5f, 0.5f);
            boxRect.anchoredPosition = boxPlacementArea.anchoredPosition;

            Debug.Log("Placed " + boxSize + " box.");
        }
        else
        {
            // if the player misses the target
            placedBox.gameObject.SetActive(false);
        }

        placedBox.raycastTarget = true;
    }

    private void moveBox(PointerEventData eventData)
    {
        // convert fingers screen pos to canvas coordinates
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera, out Vector2 localPoint);
        
        placedBox.rectTransform.anchoredPosition = localPoint;
    }
}
