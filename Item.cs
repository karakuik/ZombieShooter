using UnityEngine;

// Blueprint for all our items - we can then set customizable properties for each item (medkit, weapons...)

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]   // Steps to create item -> Right click project folder / Inventory / Item

public class Item : ScriptableObject
{
    new public string name = "New Item"; // overwrites name property
    public Sprite icon = null;
    public bool isDefaultItem = false;

    // This method is Virtual because we can derive different objects from this Item class and 
    // overwritte the method Use() depending on the type of item
    // ex. med kit - increase player's health 
    public virtual void Use()
    {
        // Use the item
        // Something might happen
        Debug.Log("Using " + name);
    }
}
