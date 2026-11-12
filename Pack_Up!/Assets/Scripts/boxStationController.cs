using UnityEngine;
using UnityEngine.UI;

public class boxStationController : MonoBehaviour
{
    [Header("order data")]
    [SerializeField] private orderRequirements orderRequirements;
    
    [Header("station panels")]
    [SerializeField] private GameObject boxStationPanel;
    [SerializeField] private GameObject protectionStationPanel;
    
    [Header("station buttons")]
    [SerializeField] private Button nextStationButton;

    [Header("result popup")]
    [SerializeField] private GameObject boxResultPopup;
    [SerializeField] private TMPro.TMP_Text resultText;
    
    [Header("station results")] public bool boxStationCorrect;

    private string selectedBoxSize;

    private void Awake()
    {
        boxResultPopup.SetActive(false);
        
        // player cant continue until they place a box
        nextStationButton.gameObject.SetActive(false);
        
        // connect next button to station transition
        nextStationButton.onClick.AddListener(goToNextStation);
    }

    public void beginBoxDrag()
    {
        // clear previous selection when player tries a diff box
        selectedBoxSize = null;
        nextStationButton.gameObject.SetActive(false);
    }

    public void placeBox(string boxSize)
    {
        // remember what box the player placed
        selectedBoxSize = boxSize;
        
        // allow player to continue
        nextStationButton.gameObject.SetActive(true);
    }

    public void cancelBoxPlacement()
    {
        // no valid box is placed
        selectedBoxSize = null;
        nextStationButton.gameObject.SetActive(false);
    }

    public void goToNextStation()
    {
        Handheld.Vibrate();
        // dont allow progression without a box
        if (string.IsNullOrEmpty(selectedBoxSize))
        {
            return;
            Debug.Log("NO BOX");
            Handheld.Vibrate();
        }
        
        // check selected box against order
        switch (selectedBoxSize)
        {
            case "small":
                boxStationCorrect = orderRequirements.box.smallBox;
                break;
            
            case "medium":
                boxStationCorrect = orderRequirements.box.mediumBox;
                break;
            
            case "large":
                boxStationCorrect = orderRequirements.box.largeBox;
                break;
            
            case "extraLarge":
                boxStationCorrect = orderRequirements.box.extraLargeBox;
                break;
            
            default:
                boxStationCorrect = false;
                break;
        }
        
        Debug.Log("Box station correct" + boxStationCorrect);

        if (boxStationCorrect)
        {
            resultText.text = "SUCCESS!";
        }
        else
        {
            resultText.text = "FAIL!";
        }

        boxResultPopup.SetActive(true);
        
        // move from box station to protection station
        boxStationPanel.SetActive(false);
        protectionStationPanel.SetActive(true);
    }
}
