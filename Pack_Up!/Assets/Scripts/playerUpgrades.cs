using UnityEngine;

public class playerUpgrades : MonoBehaviour
{
    [Header("station upgrade levels")]

    [Min(1)]
    public int boxStationLevel = 4;
    
    [Min(1)]
    public int protectionStationLevel = 4;
    
    [Min(1)]
    public int tapeStationLevel = 4;
    
    [Min(1)]
    public int postageStationLevel = 4;
}
