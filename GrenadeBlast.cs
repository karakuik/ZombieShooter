using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBlast : MonoBehaviour
{
    ZombieAI enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AreaDamageEnemies(Vector3 location, float radius, float damage)
    {
        Collider[] objectsInRange = Physics.OverlapSphere(location, radius);
        foreach (Collider col in objectsInRange)
        {
            ZombieAI enemy = col.GetComponent<ZombieAI>();
            if (enemy != null)
            {
                // linear falloff of effect
                //float proximity = (location - enemy.transform.position).magnitude;
               // float effect = 1 - (proximity / radius);


                enemy.currentHealth -=1;
            }
        }

    }

   void OnCollisionEnter(Collision collision)
   {
        if(collision.gameObject.tag == "Zombie")
        {
            ZombieAI enemy = collision.gameObject.GetComponent<ZombieAI>();
            Debug.Log("Contact");
            enemy.TakeDamage(3, this.transform.position);
            Debug.Log(enemy.currentHealth);
        }
   }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Nahh");
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("yey");
    }
}
