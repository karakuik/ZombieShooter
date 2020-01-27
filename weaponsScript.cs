using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponsScript : MonoBehaviour
{
    //These are our gun variables
    public int currentWeapon;
    //Holds the transform of the weapons in an array
    public Transform[] weapons;

    // check if weapons are bought
    public bool smg_bought = false;
    public bool shotgun_bought = false;
    public bool grenade_bought = false;
    public bool barricade_bought = false;


    //NOTE: The gameobjects must be active and currently on the player for this to work!
    //Further note: You *CURRENTLY* need to have the items dragged onto GunHolder, THEN put in the
    //transform in order for it to register!! OR ELSE IT WILL NOT WORK.

    // Start is called before the first frame update
    void Start()
    {
        changeWeapon(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //puts the weapon of choice in your hands, Alpha1 = number 1 on keyboard and soforth.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeWeapon(0);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) && smg_bought == true)
        {
            changeWeapon(1);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) && shotgun_bought == true)
        {
            changeWeapon(2);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) && grenade_bought == true)
        {
            changeWeapon(3);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5) && barricade_bought == true)
        {
            changeWeapon(4);
        }
    }

    //This hold the array of weapons and gets the input for update.
    public void changeWeapon(int num)
    {

        //This is going to need the CheckGunValues Script with the SetWeaponStats script. MAKE IT WORK.

        Debug.Log("Changing weapon");
        //checks the current number
        currentWeapon = num;

        //goes through the length of the array and activates whichever weapon is selected.
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == num)
                weapons[i].gameObject.SetActive(true);
            else
                weapons[i].gameObject.SetActive(false);
        }
    }

}
