using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour
{
    int ammo;
    Text ammoText;

    // Start is called before the first frame update
    void Start()
    {
        ammoText = GetComponent<Text>();
       ammoText.text = GunValueScript.pleaseWork(ammo);
    }

    // Update is called once per frame
    void Update()
    {
        //ammo = 
        //Debug.Log("Hi From AT " + ammo);
        //ammoText.text = "Hello " + GunValueScript.pleaseWork(ammo);
        //ammoText.text = GunValueScript.ammoArr[0].ToString();
       // Debug.Log(GunValueScript.pleaseWork(ammo) + "From ammo Text!");
        ammoText.text = GunValueScript.returnAmmo.ToString();
    }
    
}
