using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Throw : MonoBehaviour
{
    private Item_Master itemMaster;
    private Transform myTransform;
    private Rigidbody myRigidbody;
    private Vector3 throwDirection;

    public bool canBeThrown;
    public string throwButtonName;
    public float throwForce;

    // Start is called before the first frame update
    void Start()
    {
        SetInitialReferences();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForThrowInput();
    }

    void SetInitialReferences()
    {
        itemMaster = GetComponent<Item_Master>();
        myTransform = transform;
        myRigidbody = GetComponent<Rigidbody>();
    }

    void CheckForThrowInput()
    {
        if(throwButtonName != null)
        {
            // My transform.root checks that the parent of the item is the player 
            if(Input.GetButtonDown(throwButtonName) && Time.timeScale > 0 && canBeThrown &&
                myTransform.root.CompareTag(GameManager_References._playerTag))
            {
                CarryOutThrowActions();
            }
        }
    }

    void CarryOutThrowActions()
    {
        // Throw it in the direction of the player
        throwDirection = myTransform.parent.forward;
        // The item was thrown - it is in the hierarchy - the player is not the parent anymore
        myTransform.parent = null;
        // Call event to throw object
        itemMaster.CallEventObjectThrow();
        HurlItem();
    }

    void HurlItem()
    {
        myRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }
}
