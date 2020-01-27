using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Notes From 7/11/2019****
//Ammo needs to work properly
//Current issue is it doesn't reassign the amount
//All the info for the text is under AmmoText.cs

public class GunValueScript : MonoBehaviour
{
    //What I want to achieve with this script
    //Each gun/weapon/whatever will need its own semi-independent raycasts depending on the weapon
    //It will each have its own values for a unique gunplay experience.

    public GameObject explosiveGrenade;
    public GameObject barricade;

    //Check On later
    Text ammoText;


    //Variables carried over by the former script. Go through this later.
    Vector3 fwd;
    GameObject Zombie;
    GameObject Devil;

    public static int returnAmmo = 20000;
    int currentAmmo;
    RaycastHit hit;
    public ZombieSpawner Zoomzoom;
    public TempShotgunScript BoomStick;
    float distance = 10f;
    int damage = 2;
    ZombieAI Player;

    //Carried over from devil script as a fire rate. We need to work on this to make less "Repeat code"
    float fireRate = 0.1f;
    float nextFire = 0.2f;

    public static int[] ammoArr = new int[5];
    

   

    // Start is called before the first frame update
    void Awake()
    {
        ammoIntializer();
    }

    // Update is called once per frame
    void Update()
    {
         CheckWeapon(); //If out of update, cannot shoot.
    }

    enum ammoType
    {
        pistol = 10,
        smg = 20,
        shotgun = 30,
        grenade = 5,
        barricade = 5

    }

    static void ammoIntializer()
    {
        for(int i = 0; i < ammoArr.Length; i++)
        {
            ammoArr[i] = 100;
        }
    }

    //This is supposed to get the ammo amount and return it to ammoText. Doesn't work!!!
    public static string pleaseWork(int ammo)
    {
        Debug.Log("AMMUNATION" + ammo);
        return ammo.ToString();
    }

    //So now I'm trying with this one that SHOULD get the array of bullets (look at start)
    //And update it. The problem lies in the checkWeapon() in update.
    //If it is out of update, we get the returnAmmo (which is what Im using to display the ammo, currently set at 20000 as a placeholder)
    //But in update, it will constantly refresh the ammo to max amount. HOW DIFFICULT.
    static int ammoReturner(int ammo)
    {
        return ammo;
    }

    public void CheckWeapon()
    {

        if (this.gameObject.name == "Pistol" && this.gameObject.activeSelf)
        {
            Pistol();            
        }

        if(this.gameObject.name == "SMG" && this.gameObject.activeSelf)
        {

            SMG();
        }

        if (this.gameObject.name == "Shotgun" && this.gameObject.activeSelf)
        {
            Shotgun();
        }

        if (this.gameObject.name == "Grenade" && this.gameObject.activeSelf)
        {
            Grenade();
        }

        if(this.gameObject.name == "Barricade" && this.gameObject.activeSelf)
        {
            Barricade();
        }

    }

    public int Pistol()
    {
        
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire || Input.GetKeyDown("space"))
        {
            ammoArr[0]--;
           // returnAmmo--;
            // pleaseWork(returnAmmo);
            gotShot();
                       
        }
        returnAmmo = ammoArr[0];
        return returnAmmo;
    }

    public int SMG()
    {
        
        if (Input.GetMouseButton(0) && Time.time > nextFire  || Input.GetKey("space") && Time.time > nextFire)
        {
            //Invoke("gotShot", 1f);
            ammoArr[1]--;
            nextFire = Time.time + fireRate;
            gotShot();

            Debug.Log("held down");
        }
        returnAmmo = ammoArr[1];
        return returnAmmo;
    }

    public int Shotgun()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire || Input.GetKeyDown("space"))
        {
            ammoArr[2]--;
            Debug.Log("Running");
            BoomStick.whatAmIHolding();
        }
        returnAmmo = ammoArr[2];
        return returnAmmo;
    }

    public int Grenade()
    {
        if (Input.GetMouseButtonDown(0)|| Input.GetKeyDown("space"))
        {

            var thrownGrenade = (GameObject)Instantiate(explosiveGrenade, this.transform.position, this.transform.rotation);
            Rigidbody rb;
            rb = thrownGrenade.GetComponent<Rigidbody>();
            thrownGrenade.GetComponent<Rigidbody>().velocity = thrownGrenade.transform.forward * 10;
            rb.AddForce(transform.forward * 5);

            ammoArr[3]--;
        }
        returnAmmo = ammoArr[3];
        return returnAmmo;

    }

    public int Barricade()
    {

        Vector3 playerPos = this.transform.position;
        Vector3 playerDirection = this.transform.forward;
        Quaternion playerRotation = this.transform.rotation;
        float spawnDistance = 1.5f;

        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
       
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            var newBarricade = (GameObject)Instantiate(barricade, spawnPos, Quaternion.identity);
            ammoArr[4]--;
        }
        returnAmmo = ammoArr[4];
        return returnAmmo;
    }

    public void gotShot()
    {

        fwd = transform.TransformDirection(Vector3.forward * 10);
        Ray DetectionRay = new Ray(transform.position, fwd);

        //Physics capsule cast
        //Gotta figure this out later
        if (Physics.CapsuleCast(transform.position, fwd, 1.5f, Vector3.forward, out hit, distance))
        {
            if (hit.collider.tag == "Zombie")
                Debug.Log("Capsule hit!");
        }
        if (Physics.Raycast(DetectionRay, out hit, distance)){

                //Ray Debugger
                Debug.DrawRay(transform.position, fwd, Color.green);
                if (hit.collider.tag == "Zombie")
                {
                    //If Zombie is hit, destroy that specific GameObject, and decrease # of zombies
                    Debug.Log("Hit!");

                    //Checks the zombie health script.
                    ZombieAI health = hit.collider.GetComponent<ZombieAI>();

                    //this sets how much damage a zombie will take. See why this may not work if its attached to every gun? 1 damage isnt going to cut it if we're using miniguns or shotguns, etc.
                    health.TakeDamage(1, hit.point);
                    if (health != null)
                    {
                        health.TakeDamage(0, hit.point);
                        Debug.Log("Zombie Health: " + health.currentHealth);
                    }
                    //Need health.takedamage(1,hit.point)

                    Debug.Log(Zoomzoom.numEnemy);
                }

                //A repeat of above, except with EL DIABLO!!
                if (hit.collider.tag == "Devil")
                {
                    DevilScript health = hit.collider.GetComponent<DevilScript>();
                    //If Zombie is hit, destroy that specific GameObject, and decrease # of zombies
                    Debug.Log("Hit!");
                    if (health != null)
                    {
                        health.TakeDamage(1, hit.point);
                        Debug.Log("Devil health = " + health.currentHealth);
                    }

                }

        }

    }
}


