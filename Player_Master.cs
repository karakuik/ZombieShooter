using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Master : MonoBehaviour
{
    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventInventoryChanged;
    public event GeneralEventHandler EventHandsEmpty;
    public event GeneralEventHandler EventItemChanged;
    
    public delegate void ItemPickupEventHandler(string itemName, int quantity);
    public event ItemPickupEventHandler EventPickUpItem;
    /*
    public delegate void PlayerHealthEventHandler(int healthChange);
    public event PlayerHealthEventHandler EventPlayerHealthDeduction;
    public event PlayerHealthEventHandler EventPlayerHealthIncrease;*/

    public void CallEventInventoryChanged()
    {
        if(EventInventoryChanged != null)
        {
            EventInventoryChanged();
        }
    }

    public void CallEventHandsEmpty()
    {
        if(EventHandsEmpty != null)
        {
            EventHandsEmpty();
        }
    }

    public void CallEventItemChanged()
    {
        if (EventItemChanged != null)
        {
            EventItemChanged();
        }
    }
    
    public void CallEventPickedUpItem(string itemName, int quantity)
    {
        if(EventPickUpItem != null)
        {
            EventPickUpItem(itemName, quantity);
        }
    }
    /*
    public void CallEventPlayerHealthDeduction(int dmg)
    {
        if(EventPlayerHealthDeduction != null)
        {
            EventPlayerHealthDeduction(dmg);
        }
    }

    // Never used
    public void CallEventPlayerHealthIncrease(int increase)
    {
        if(EventPlayerHealthIncrease != null)
        {
            EventPlayerHealthIncrease(increase);
        }
    }*/
}
