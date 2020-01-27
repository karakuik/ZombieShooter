using UnityEngine;

public class Player_DetectItem : MonoBehaviour
{
    [Tooltip("What layers is being used for items.")]
    public LayerMask layerToDetect;
    [Tooltip("what transform will the ray be fired from?")]
    public Transform rayTransformPivot;
    [Tooltip("the editor input button that will be used for picking items")]
    public string buttonPickUp;

    private Transform itemAvailableForPickup;
    private RaycastHit hit;
    private float detectRange = 3;
    private float DetectRadius = 0.7f;
    private bool itemInRange;

    private float labelWidth = 200;
    private float labelHeight = 50;

    private HUDManager hudManager;

    private void OnEnable()
    {
        hudManager = FindObjectOfType<HUDManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CastRayForDetectingItems();
        CheckForItemPickupAttempt();
    }

    // Detects item using sphere cast
    void CastRayForDetectingItems()
    {
        if (Physics.SphereCast(rayTransformPivot.position, DetectRadius, rayTransformPivot.forward, out hit, detectRange, layerToDetect))
        {
            itemAvailableForPickup = hit.transform;
            itemInRange = true;
        }
        else
        {
            itemInRange = false;
        }
    }

    void CheckForItemPickupAttempt()
    {
        // Check if button was pressed - game is not paused - item in range - the player is not the parent
        if (Input.GetButtonDown(buttonPickUp) && Time.timeScale > 0 && itemInRange && itemAvailableForPickup.root.tag != GameManager_References._playerTag)
        {
            Debug.Log("Pickup attempted");
            // The event "CallEventPickupAction" is called
            // It goes to the ItemPickup script 
            // Run the methods subscribed to it
            itemAvailableForPickup.GetComponent<Item_Master>().CallEventPickupAction(rayTransformPivot);
            hudManager.UpdateStat(itemAvailableForPickup);
        }
    }

    void OnGUI()
    {
        // Display name of the item
        if (itemInRange && itemAvailableForPickup != null && itemAvailableForPickup.root.tag != GameManager_References._playerTag)
        {
            GUI.Label(new Rect(Screen.width / 2 - labelWidth / 2, Screen.height / 2, labelWidth, labelHeight), itemAvailableForPickup.name);
        }
    }
}