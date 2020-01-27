using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    // Singleton - used to create reference to inventory 
    // That way we don't have to use FindObjectOfType in other classes to reference it
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    // Event used to update the UI
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(item);

            // Trigger the event - We want the UI to update
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove (Item item)
    {
        // Remove item from the inventory list
        items.Remove(item);
        
        // Trigger the event - We want the UI to update
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
