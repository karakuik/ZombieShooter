using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Handles the inventory and items in player's hand
 * Commented out the code to put the item in player's hand
 * We don't need it for the ammo & keys & med kit
 * But we can use it for the weapons
 */

public class Player_Inventory : MonoBehaviour
{
    public Transform inventoryPlayerParent;
    public Transform inventoryUIParent;
    public GameObject uiButton;

    private Player_Master playerMaster;
    private GameManager_ToggleInventoryUI inventoryUIScript;
    private float timeToPlaceInHands = 0.1f;
    private Transform currentHeltItems;
    private int counter;
    private string buttonText;
    private List<Transform> listInventory = new List<Transform>();

    private HUDManager hudManager;
    
    private void OnEnable()
    {
        SetInitialReferences();
        UpdateInventoryListAndUI();
        CheckIfHandsEmpty();

        playerMaster.EventInventoryChanged += UpdateInventoryListAndUI;
        
        /*playerMaster.EventInventoryChanged += CheckIfHandsEmpty;
        playerMaster.EventHandsEmpty += ClearHands;*/
    }

    private void OnDisable()
    {
        playerMaster.EventInventoryChanged -= UpdateInventoryListAndUI;
        
        /*playerMaster.EventInventoryChanged -= CheckIfHandsEmpty;
        playerMaster.EventHandsEmpty -= ClearHands;*/
    }
    
    void SetInitialReferences()
    {
        inventoryUIScript = GameObject.Find("GameManager").GetComponent<GameManager_ToggleInventoryUI>();
        playerMaster = GetComponent<Player_Master>();
        hudManager = GetComponent<HUDManager>();
    }

    // When the game starts - the inventory list gets filled up and UI get updated
    void UpdateInventoryListAndUI()
    {
        counter = 0;
        listInventory.Clear();
        listInventory.TrimExcess();

        ClearInventoryUI();

        // Go through each item on the list
        foreach (Transform child in inventoryPlayerParent)
        {
            if (child.CompareTag("Item"))
            {
                // Add item to the list
                listInventory.Add(child);

                // Instantiate button
                GameObject go = Instantiate(uiButton) as GameObject;
                buttonText = child.name;
                go.GetComponentInChildren<Text>().text = buttonText;
                int index = counter;

                //go.GetComponent<Button>().onClick.AddListener(delegate { ActivateInventoryItem(index); });    // place item in player's hand

                go.GetComponent<Button>().onClick.AddListener(inventoryUIScript.ToggleInventoryUI);
                // Set the inventoryUI as parent
                go.transform.SetParent(inventoryUIParent, false);
                counter++;
            }
        }
    }

    #region Items that can be hold in player's hands - Weapons 

    /*   If the player does not have any items on their hands
     *   But there are items on the inventory
     *   Activate the last item on the list 
     *   Place it in the player's hands
     */
    void CheckIfHandsEmpty()
    {
        if (currentHeltItems == null && listInventory.Count > 0)
        {
            StartCoroutine(PlaceItemInHands(listInventory[listInventory.Count - 1]));
        }
    }

    void ClearHands()
    {
        currentHeltItems = null;
    }

    /*
     * Clear inventoryUI every time the player opens the inventory menu
     */
    void ClearInventoryUI()
    {
        foreach (Transform child in inventoryUIParent)
        {
            Destroy(child.gameObject); // clear buttons in inventory ui
        }
    }

    /*
     * Call function to deactivate all item
     * Activate selected item
     */
    public void ActivateInventoryItem(int inventoryIndex)
    {
        DeactivateAllInventoryItems();
        // Cause item to appear in player's hand
        StartCoroutine(PlaceItemInHands(listInventory[inventoryIndex]));
    }

    /*
     * Deactivates all items
     */
    void DeactivateAllInventoryItems()
    {
        foreach (Transform child in inventoryPlayerParent)
        {
            if (child.CompareTag("Item"))
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator PlaceItemInHands(Transform itemTransform)
    {
        yield return new WaitForSeconds(timeToPlaceInHands);
        currentHeltItems = itemTransform;
        currentHeltItems.gameObject.SetActive(true);
    }
    #endregion
}
