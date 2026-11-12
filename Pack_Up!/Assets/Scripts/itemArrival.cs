using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class itemArrival : MonoBehaviour
{
    [Header("arrival settings")]
    [SerializeField] private GameObject orderPanel;
    
    [SerializeField] private Vector2 startingPosition = new Vector2(-800f, 0f);
    [SerializeField] private Vector2 targetPosition = Vector2.zero;
    [SerializeField] private float slideDuration = 0.8f;

    private RectTransform itemTransform; // size of element
    private Button itemButton; // says if button can be clicked

    private void Awake()
    {
        itemTransform = GetComponent<RectTransform>();
        itemButton = GetComponent<Button>();
    }

    private void Start()
    {
        orderPanel.SetActive(false); // hiding the order panel
        itemTransform.anchoredPosition = startingPosition; // makes the item start off screen by accessing the Vector2 i set
        itemButton.interactable = false;  // making button not work
        itemButton.onClick.AddListener(SelectItem);
        StartCoroutine(slideItemIn()); // starts the coroutine to make item slide
    }

    private IEnumerator slideItemIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime; // increase the elapsedTime by seeing how long as passed each frame
            
            float progress = Mathf.Clamp01(elapsedTime / slideDuration); // gives a value from 0 to 1 depending on how much of journey is done
            
            itemTransform.anchoredPosition = Vector2.Lerp(startingPosition, targetPosition, progress); // uses the progress lerp to figure out the items position
            
            yield return null; // this makes it pause the coroutine for a frame so the item can move on screen
        }
        itemTransform.anchoredPosition =  targetPosition;
        itemButton.interactable = true;
    }

    private void SelectItem()
    {
        orderPanel.SetActive(true); // selecting the item, is a work in progress
    }
}
