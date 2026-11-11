using UnityEngine;

public class orderRequirements : MonoBehaviour
{
    // station 1, box size
    [System.Serializable]
    public class boxRequirements
    {
        public bool smallBox;
        public bool mediumBox;
        public bool largeBox;
        public bool extraLargeBox;
    }
    
    // station 2, protection
    [System.Serializable]
    public class protectionRequirements
    {
        public bool noProtection;
        public bool packingPeanuts;
        public bool bubbleWrap;
        public bool foamProtection;
    }
    
    // station 3, tape
    [System.Serializable]
    public class tapeRequirements
    {
        public bool oneTapeStrip;
        public bool twoTapeStrip;
        public bool fourTapeStrip;
        public bool sixTapeStrip;
    }
    
    // station 4, postage/labels
    [System.Serializable]
    public class postageRequirements
    {
        public bool standardPostage;
        public bool expressPostage;

        public bool fragileLabel;
        public bool thisWayUpLabel;
    }
    
    [Header("order requirements")]
    
    public boxRequirements box = new boxRequirements();
    public protectionRequirements protection = new protectionRequirements();
    public tapeRequirements tape = new tapeRequirements();
    public postageRequirements postage = new postageRequirements();
}
