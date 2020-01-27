using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    //Ok SO. This is a big script, and will have to be modified to suit other weapons later on, so it may be scrapped entirely or just modified through other scripts.

    Vector3 fwd;
    GameObject Zombie;
    GameObject Devil;
    RaycastHit hit;
    //public ZombieSpawner Zoomzoom;
    float distance = 10f;
    int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Checks to see if Zombies are Spawning. TO BE DELETED LATER
        //InvokeRepeating("ZombieFinder", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //Checks the forward Facing Direction & raycasts
        fwd = transform.TransformDirection(Vector3.forward * 10);
        Ray DetectionRay = new Ray(transform.position, fwd);

        //Fire by MB1 or Spacebar
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            //Ray Debugger
            Debug.DrawRay(transform.position, fwd, Color.cyan);

            if (Physics.Raycast(DetectionRay, out hit, distance))
            {        
                if (hit.collider.tag == "Zombie")
                {
                    //If Zombie is hit, destroy that specific GameObject, and decrease # of zombies
                    Debug.Log("Hit!");

                    //Checks the zombie health script.
                    ZombieAI health = hit.collider.GetComponent<ZombieAI>();

                    //this sets how much damage a zombie will take. See why this may not work if its attached to every gun? 1 damage isnt going to cut it if we're using miniguns or shotguns, etc.
                    health.TakeDamage(damage, hit.point);
                    if (health != null)
                    {
                        health.TakeDamage(0, hit.point);
                        Debug.Log("Zombie Health: " + health.currentHealth);
                    }
                    //Need health.takedamage(1,hit.point)
                }

                //A repeat of above, except with EL DIABLO!!
                if (hit.collider.tag == "Devil")
                {
                    DevilScript health = hit.collider.GetComponent<DevilScript>();
                    //If Zombie is hit, destroy that specific GameObject, and decrease # of zombies
                    Debug.Log("Hit!");
                    if(health !=null)
                    {
                        health.TakeDamage(damage, hit.point);
                        Debug.Log("Devil health = " + health.currentHealth);
                    }
                    
                }

            }
        }
    }

    //Checks to see if zombies are around. TBD
    void ZombieFinder()
    {
        Zombie = GameObject.FindGameObjectWithTag("Zombie");
        if (!Zombie)
            Debug.Log("No zombies here");
        else if (Zombie)
            Debug.Log("Zombie found!");
    }
}
