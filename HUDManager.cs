using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Text medKitText;
    public Text keysText;

    int medKitAmt = 0;
    int keysAmt = 0;
    
    public void UpdateStat(Transform item)
    {
        if (item.name == "MedKit")
        {
            medKitAmt++;
            medKitText.text = medKitAmt.ToString();
        }
        if(item.name == "Key")
        {
            keysAmt++;
            keysText.text = keysAmt.ToString();
        }
    }
}
