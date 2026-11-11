using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class orderScreenController : MonoBehaviour
{
    [Header("Order Data")]
    [SerializeField] private orderRequirements orderRequirements;
    
    [Header("order screen UI")]
    [SerializeField] private GameObject orderPanel;
    [SerializeField] private TMP_Text requirementsText;
    [SerializeField] private Button beginPackingButton;

    [Header("packing screen UI")]
    [SerializeField] private GameObject packingStationPanel;
    [SerializeField] private Button orderTicketButton;

    private void Start()
    {
        DisplayRequirements();

        beginPackingButton.onClick.AddListener(beginPacking);
        orderTicketButton.onClick.AddListener(ToggleOrderTicket);

        orderTicketButton.gameObject.SetActive(false);
        packingStationPanel.SetActive(false);
    }

    private void DisplayRequirements()
    {
        string labels = "";

        if (orderRequirements.postage.fragileLabel)
        {
            labels += "Fragile";
        }

        if (orderRequirements.postage.thisWayUpLabel)
        {
            if (labels != "")
            {
                labels += ", ";
            }

            labels += "This Way Up";
        }

        if (labels == "")
        {
            labels = "No labels";
        }
        
        requirementsText.text = 
            "BOX SIZE: " + getBoxChoice() + "\n\n" + 
            "PROTECTION: " + getProtectionChoice() + "\n\n" + 
            "TAPE: " + getTapeChoice() + "\n\n" + 
            "POSTAGE: " + getPostageChoice() + "\n\n" + 
            "LABELS: " + labels;
    }

    private string getBoxChoice()
    {
        if (orderRequirements.box.smallBox) return "Small";
        if (orderRequirements.box.mediumBox)  return "Medium";
        if (orderRequirements.box.largeBox)  return "Large";
        if (orderRequirements.box.extraLargeBox)  return "ExtraLarge";

        return "Not selected";
    }

    private string getProtectionChoice()
    {
        if (orderRequirements.protection.noProtection) return "None";
        if (orderRequirements.protection.packingPeanuts) return "Packing Peanuts";
        if (orderRequirements.protection.bubbleWrap) return "Bubble Wrap";
        if (orderRequirements.protection.foamProtection) return "Foam Protection";
        
        return "Not selected";
    }

    private string getTapeChoice()
    {
        if (orderRequirements.tape.oneTapeStrip) return "1 Strip";
        if (orderRequirements.tape.twoTapeStrips)  return "2 Strips";
        if (orderRequirements.tape.fourTapeStrips)  return "4 Strips";
        if (orderRequirements.tape.sixTapeStrips)  return "6 Strips";
        
        return "Not selected";
    }

    private string getPostageChoice()
    {
        if (orderRequirements.postage.standardPostage) return "Standard";
        if (orderRequirements.postage.expressPostage) return "Express";
        
        return "Not selected";
    }

    private void beginPacking()
    {
        orderPanel.SetActive(false);
        beginPackingButton.gameObject.SetActive(false);
        orderTicketButton.gameObject.SetActive(true);
        packingStationPanel.SetActive(true);
    }

    private void ToggleOrderTicket()
    {
        orderPanel.SetActive(!orderPanel.activeSelf);
    }
}
