﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    private Item_Master itemMaster;

    public Vector3 pickUpPos;

    private void OnEnable()
    {
        SetInitialReferences();
        itemMaster.EventPickupAction += CarryOutPickupActions;
    }

    private void OnDisable()
    {
        itemMaster.EventPickupAction -= CarryOutPickupActions;
    }

    void SetInitialReferences()
    {
        itemMaster = GetComponent<Item_Master>();
    }

    void CarryOutPickupActions(Transform tParent)
    {
        transform.SetParent(tParent);
        itemMaster.CallEventObjectPickup();
        transform.gameObject.SetActive(false);
    }
}
