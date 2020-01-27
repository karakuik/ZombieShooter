using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempShotgunScript : MonoBehaviour
{
    int amountToShoot = 4;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
            whatAmIHolding();
    }

    public void whatAmIHolding()
    {
        
        for (int i = 0; i < amountToShoot; i++)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;

        float distance = 5f;

        Vector3 direction = transform.forward;
        Vector3 spread = new Vector3();


        spread = spread + (transform.right * Random.Range(-1f, 1f));
        direction += spread.normalized * Random.Range(0f, 0.2f);

        //Vector3 fwd = transform.TransformDirection(Vector3.forward * 10);
        Ray DetectionRay = new Ray(transform.position, direction);



            if (Physics.Raycast(DetectionRay, out hit, distance))
            {
                Debug.DrawLine(transform.position, hit.point, Color.green, 1f);

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
            else
                Debug.DrawRay(this.transform.position, direction, Color.red, 0.2f);
        



    }
}
