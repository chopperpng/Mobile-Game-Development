using UnityEngine;

public class orderGenerator : MonoBehaviour
{
    [Header("required data")]
    [SerializeField] private playerUpgrades playerUpgrades; // lets script see station levels
    [SerializeField] private orderRequirements orderRequirements; // lets script write item requirements

    private void Awake()
    {
        generateOrder(); // to stop orders clashing
    }

    public void generateOrder()
    {
        resetRequirements();

        generateBoxChoice();
        generateProtectionChoice();
        generateTapeChoice();
        generatePostageChoice();
        generateLabels();
    }

    private void resetRequirements()
    {
        // reset box choices
        orderRequirements.box.smallBox = false;
        orderRequirements.box.mediumBox = false;
        orderRequirements.box.largeBox = false;
        orderRequirements.box.extraLargeBox = false;
        
        // reset protection choices
        orderRequirements.protection.noProtection = false;
        orderRequirements.protection.packingPeanuts = false;
        orderRequirements.protection.bubbleWrap = false;
        orderRequirements.protection.foamProtection = false;
        
        // reset tape choices
        orderRequirements.tape.oneTapeStrip = false;
        orderRequirements.tape.twoTapeStrips = false;
        orderRequirements.tape.fourTapeStrips = false;
        orderRequirements.tape.sixTapeStrips  = false;
        
        // reset postage and labels
        orderRequirements.postage.standardPostage  = false;
        orderRequirements.postage.expressPostage   = false;
        orderRequirements.postage.fragileLabel =  false;
        orderRequirements.postage.thisWayUpLabel = false;
    }

    private void generateBoxChoice()
    {
        int availableOptions = Mathf.Clamp(playerUpgrades.boxStationLevel, 1, 4); // keeps available option value between 1 and 4

        int selectedOption = Random.Range(0, availableOptions);

        switch (selectedOption) // switch statement to check the random number to the requirements
        {
            case 0:
                orderRequirements.box.smallBox = true;
                break;
            
            case 1:
                orderRequirements.box.mediumBox = true;
                break;
            
            case 2:
                orderRequirements.box.largeBox = true;
                break;
            
            case 3:
                orderRequirements.box.extraLargeBox = true;
                break;
        }
    }

    private void generateProtectionChoice()
    {
        int availableOptions = Mathf.Clamp(playerUpgrades.protectionStationLevel, 1, 4); // // keeps available option value between 1 and 4
        
        int selectedOption = Random.Range(0, availableOptions); // switch statement to check the random number to the requirements

        switch (selectedOption)
        {
            case 0:
                orderRequirements.protection.noProtection = true;
                break;
            
            case 1:
                orderRequirements.protection.packingPeanuts = true;
                break;
            
            case 2:
                orderRequirements.protection.bubbleWrap = true;
                break;
            
            case 3:
                orderRequirements.protection.foamProtection = true;
                break;
        }
    }

    private void generateTapeChoice()
    {
        int availableOptions = Mathf.Clamp(playerUpgrades.tapeStationLevel, 1, 4); // // keeps available option value between 1 and 4
        
        int selectedOption = Random.Range(0, availableOptions); // switch statement to check the random number to the requirements

        switch (selectedOption)
        {
            case 0:
                orderRequirements.tape.oneTapeStrip = true;
                break;
            
            case 1:
                orderRequirements.tape.twoTapeStrips = true;
                break;
            
            case 2:
                orderRequirements.tape.fourTapeStrips = true;
                break;
            
            case 3:
                orderRequirements.tape.sixTapeStrips = true;
                break;
                
        }
    }

    private void generatePostageChoice()
    {
        // only two postage options right now
        int availableOptions = Mathf.Clamp(playerUpgrades.postageStationLevel, 1, 2);
        
        int selectedOption = Random.Range(0, availableOptions);

        switch (selectedOption)
        {
            case 0:
                orderRequirements.postage.standardPostage = true;
                break;
            
            case 1:
                orderRequirements.postage.expressPostage = true;
                break;
        }
    }

    private void generateLabels()
    {
        // each label has a 50/50 chance
        orderRequirements.postage.fragileLabel = Random.value < 0.5f;
        orderRequirements.postage.thisWayUpLabel = Random.value < 0.5f;
    }
}
