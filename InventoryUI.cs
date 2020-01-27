using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;   // reference to the inventory slots
    public GameObject inventoryUI;

    Inventory inventory;

    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Toggle Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

        // Pause game if the inventory is open
        if (inventoryUI.activeSelf == true)
        {
            Time.timeScale = 0f;
        }
        // Resume game when the player closes the inventory
        if(inventoryUI.activeSelf == false)
        {
            Time.timeScale = 1f;
        }
    }

    void UpdateUI()
    {
        // Loop through all the slots
        for(int i = 0; i< slots.Length; i++)
        {
            // There are still items to add
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            // No more items to add
            else
            {
                slots[i].ClearSlot(); 
            }
        }
        Debug.Log("Updating UI");
    }
}
