using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieAI : MonoBehaviour
{
    public float speed = 0;
    float health = 5f;
    public float currentHealth = 5f;
    bool isDead;
    public int zombieDamage = 5;

    //sets the healthbar
    [Header("Unity Stuff")]
    public Image healthBar;

    private GameObject Player;
    NPC_Master npcMaster;

    private void OnEnable()
    {
        SetInititalReferences();
    }

    void SetInititalReferences()
    {
        npcMaster = GetComponent<NPC_Master>();
    }

    // Start is called before the first frame update
    /*void Start()
    {
        //Player = GameObject.FindWithTag("Player").transform;
        Player = FindObjectOfType<Player>().gameObject;
        speed = (Random.Range(1f, 2.5f));
        currentHealth = health;
    }*/

    public void Start()
    {
        Player = GameObject.FindWithTag("Player");
        //Player = CharacterSpawner.player;
        //StartCoroutine(GetPlayerReference());
        speed = (Random.Range(1f, 2.5f));
        currentHealth = health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player != null)
        {
            MathMove();
        }
        else
        {
            Debug.Log("Player is null");
        }
    }

    //8 way directional movement and rotation, by yours truly after nearly 5 hours of BULL.
    public void MathMove()
    {
        //PvED Means Player versus Enemy Direction.
        float PvEDx = 0;
        float PvEDz = 0;
        PvEDx = Player.transform.position.x - this.transform.position.x;
        PvEDz = Player.transform.position.z - this.transform.position.z;
        
        
        if (PvEDz < -1 && PvEDx < -1)
        {
            transform.eulerAngles = new Vector3(0, -45, 0);
            transform.Translate(new Vector3(-1, 0, -1) * Time.deltaTime * speed, Space.World);

            // OG transform.Translate(new Vector3(-1, 0, -1) * Time.deltaTime);


            //transform.eulerAngles = new Vector3(0, -225, 0);
            //Quaternion target = Quaternion.Euler(0, 45, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2f);
        }

        else if (PvEDz < -1 && PvEDx > 1)
        {
            transform.eulerAngles = new Vector3(0, -115, 0);
            transform.Translate(new Vector3(1, 0, -1) * Time.deltaTime * speed, Space.World);
        }
            
         else if(PvEDz > 1 && PvEDx < -1)
         {
            transform.eulerAngles = new Vector3(0, -300, 0);
            transform.Translate(new Vector3(-1, 0, 1) * Time.deltaTime * speed, Space.World);
         }
            
        else if (PvEDz > 1 && PvEDx > 1)
        {
            transform.eulerAngles = new Vector3(0, -205, 0);
            transform.Translate(new Vector3(1, 0, 1) * Time.deltaTime * speed, Space.World);
        }          

        else if (PvEDx < -1)
        {
            transform.eulerAngles = new Vector3(0, 5, 0);
            transform.Translate(-Vector3.right * Time.deltaTime * speed, Space.World);
        }
           
        else if (PvEDz > 1)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        }
            
        else if (PvEDz < -1)
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
            transform.Translate(-Vector3.forward * Time.deltaTime * speed, Space.World);
        }
            
        else if (PvEDx > 1)
        {
            transform.eulerAngles = new Vector3(0, -185, 0);
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        }   
    }

    //take damage script. Returns the amount of damage and if it was hit by the raycast.
    public void TakeDamage(int amount, Vector3 hitpoint)
    {
       // if (isDead)
       //     return;

        this.currentHealth -= amount;
        //Take this to Devil later TBC
        healthBar.fillAmount = this.currentHealth / health;

        if (this.currentHealth <= 0)
        {
            Death();
        }
    }

    //Checks if zombie is dead & adds a score of 100. We can change the score later, maybe add some multipliers, who knows?
    void Death()
    {
        ScoreScript.scoreAmount += 100;
        isDead = true;
        npcMaster.CallEventNpcDie();
        // Wait random seconds to destroy gameobject
        Destroy(this.gameObject, Random.Range(0.5f, 1f));
        Debug.Log("Ding dong Zombie is dead!");
    }

    IEnumerator GetPlayerReference()
    {
        yield return new WaitUntil(isPlayerReady);
        Player = FindObjectOfType<Player>().gameObject;
    }

    bool isPlayerReady()
    {
        if (CharacterSpawner.isPlayerReady == true)
            return true;
        else
            return false;
    }
}
