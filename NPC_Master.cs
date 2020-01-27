using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Master : MonoBehaviour
{
    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventNpcDie;

    public void CallEventNpcDie()
    {
        if(EventNpcDie != null)
        {
            EventNpcDie();
        }
    }
}
