using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyWeapons : MonoBehaviour
{
    public weaponsScript weaponsscript;
    public GameObject text;
    public Light light;
    public float counter = 1f;
    public bool is_Smg = false;
    public bool is_shotgun = false;
    public bool is_grenade = false;
    public bool is_barricade = false;
    
    bool light_flash = true;

    string weaponBought;
    Item item;
    GameObject gunHolder;

    public void Start()
    {
        weaponsscript = FindObjectOfType<weaponsScript>();
        gunHolder = GameObject.FindGameObjectWithTag("GunHolder");
    }

    private void Update()
    {
        if(counter >= 0.9)
        {
            light_flash = false;
        }
        else if(counter <= 0.1)
        {
            light_flash = true;
        }

        if(light_flash == false)
        {
            counter = counter - Time.deltaTime;
        }
        else if(light_flash == true)
        {
            counter = counter + Time.deltaTime;
        }

        light.intensity = counter;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            text.SetActive(true);

            if (Input.GetKeyDown("e"))
            {
                Debug.Log("E was pressed");
                if(is_Smg == true)
                {
                  weaponsscript.smg_bought = true;
                    Debug.Log("Bought SMG");
                    weaponBought = "SMG";
                    
                    AddWeaponToInventory();
                }
                else if (is_shotgun == true)
                {
                    weaponsscript.shotgun_bought = true;
                    weaponBought = "Shotgun";
                    AddWeaponToInventory(); 
                }
                else if (is_grenade == true)
                {
                    weaponsscript.grenade_bought = true;
                    weaponBought = "Grenade";
                    AddWeaponToInventory();
                }
                else if (is_barricade == true)
                {
                    weaponsscript.barricade_bought = true;
                    weaponBought = "Barricade";
                    AddWeaponToInventory();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }

    // Add weapon to inventory
    void AddWeaponToInventory()
    {
        // Go through the child gameobjects of gunholder to find the correct weapon
        foreach (Transform child in gunHolder.transform)
        {
            if (child.name == weaponBought && weaponBought != null)
            {
                weaponBought = "";
                Debug.Log("Adding weapon to inventory");

                // Get the item type of that weapon to be able to add it to the inventory
                item = child.gameObject.GetComponent<WeaponType>().item;

                Inventory.instance.Add(item);
            }
        }
    }
}
