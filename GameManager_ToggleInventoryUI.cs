using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script responsible for calling the event for toggling the Inventory UI

public class GameManager_ToggleInventoryUI : MonoBehaviour
{
    // Check if there is an inventory
    [Tooltip("Does this game mode have an inventory? Set to true if that is the case")]
    public bool hasInventory;
    public GameObject inventoryUI;
    public string toggleInventoryButton;
    private GameManager_Master gameManagerMaster;

    // Start is called before the first frame update
    void Start()
    {
        SetInitialReferences();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInventoryUIToggleRequest();
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();

        if(toggleInventoryButton == "")
        {
            Debug.LogWarning("Please type in the name of the button used to toggle the inventory " + 
                "GameManager_ToglleInventoryUI");
            this.enabled = false; // The update method won't run
        }
    }

    void CheckForInventoryUIToggleRequest()
    {
        // Open the inventory
        if(Input.GetButtonUp(toggleInventoryButton) && !gameManagerMaster.isMenuOn && 
            !gameManagerMaster.isGameOver && hasInventory)
        {
            ToggleInventoryUI();
        }
    }

    // When you select something the inventory ui will automatically close
    public void ToggleInventoryUI()
    {
        if(inventoryUI != null)
        {
            // Set it to the opposite of its currect active state
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            // Update its state in the gameManagerMaster 
            gameManagerMaster.isInventoryUIOn = !gameManagerMaster.isInventoryUIOn;
            gameManagerMaster.CallInventoryUIToggle();
        }
    }
}
