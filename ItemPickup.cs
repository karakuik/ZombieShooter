using UnityEngine;
using System.Collections;

public class ItemPickup : Interactable
{
    public Item item;
    string parentTag;

    private void Start()
    {
        StartCoroutine(DestroyItem());
    }

    public override void Interact()
    {
        // Goes back to Interactable base class 
        base.Interact();

        Pickup();
    }

    void Pickup()
    {
        Debug.Log("Picking up " + item.name);

        // We can reference the inventory like this because the Inventory class has a Singleton
        bool wasPickedUp = Inventory.instance.Add(item);   //Add item to inventory

        // Make the player the parent
        if (wasPickedUp)
        {
            transform.SetParent(player);
            transform.gameObject.SetActive(false);
        }
    }
    
    // Destroy the item after x seconds
    IEnumerator DestroyItem()
    {
        yield return new WaitForSecondsRealtime(15);
        if (this.transform.parent == null)
        {
            Destroy(this.gameObject);
        }
        
    }
}
