
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    [Header("Conveyor")]
    [SerializeField] private RectTransform conveyorArea;
    [SerializeField] private Button parcelButton;
    [SerializeField] private float slideDuration = 1.5f;

    [Header("Panels")]
    [SerializeField] private GameObject orderPanel;
    [SerializeField] private GameObject packingPanel;

    [Header("Order Text")]
    [SerializeField] private TMP_Text orderTitleText;
    [SerializeField] private TMP_Text boxSizeText;
    [SerializeField] private TMP_Text protectionText;
    [SerializeField] private TMP_Text tapeText;
    [SerializeField] private TMP_Text postageText;

    // The options currently available at the start of the game.
    // Upgrades can add more entries to these arrays later.
    private readonly string[] boxSizes = { "Small" };
    private readonly string[] protectionOptions = { "None" };
    private readonly int[] tapeOptions = { 1 };
    private readonly string[] postageOptions = { "3rd Class" };

    private bool isMoving;
    private bool orderOpened;

    private void Start()
    {
        orderPanel.SetActive(false);
        packingPanel.SetActive(false);

        GenerateOrder();
        StartCoroutine(SlideParcelIntoView());
    }

    private void GenerateOrder()
    {
        string boxSize = boxSizes[Random.Range(0, boxSizes.Length)];
        string protection = protectionOptions[
            Random.Range(0, protectionOptions.Length)];
        int tape = tapeOptions[Random.Range(0, tapeOptions.Length)];
        string postage = postageOptions[
            Random.Range(0, postageOptions.Length)];

        orderTitleText.text =
            "CUSTOMER ORDER #" + Random.Range(1000, 10000);

        boxSizeText.text = "Box size: " + boxSize;
        protectionText.text = "Protection: " + protection;
        tapeText.text = "Tape strips: " + tape;
        postageText.text = "Postage: " + postage;
    }

    private IEnumerator SlideParcelIntoView()
    {
        // Wait one frame so the UI layout has a size.
        yield return null;

        RectTransform parcelRect =
            parcelButton.GetComponent<RectTransform>();

        float startX =
            -conveyorArea.rect.width / 2f - parcelRect.rect.width;

        Vector2 startPosition = new Vector2(startX, 0f);
        Vector2 endPosition = Vector2.zero;

        parcelRect.anchoredPosition = startPosition;
        isMoving = true;

        float elapsed = 0f;

        while (elapsed < slideDuration && isMoving)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / slideDuration);

            // Smoothly ease the parcel into position.
            t = t * t * (3f - 2f * t);

            parcelRect.anchoredPosition =
                Vector2.Lerp(startPosition, endPosition, t);

            yield return null;
        }

        if (isMoving)
        {
            parcelRect.anchoredPosition = endPosition;
        }

        isMoving = false;
    }

    public void OnParcelTapped()
    {
        // Stop the parcel if the player taps it while it is moving.
        isMoving = false;

        orderOpened = true;
        orderPanel.SetActive(true);
    }

    public void StartPacking()
    {
        if (!orderOpened)
        {
            return;
        }

        orderPanel.SetActive(false);
        packingPanel.SetActive(true);

        parcelButton.gameObject.SetActive(false);
    }
}
