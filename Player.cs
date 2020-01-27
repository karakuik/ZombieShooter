using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //Player movement. Read as you go.
    public float speed = 6.0f;
    public float maxHealth = 50f;
    public float currentHealth;
    public Text healthText;
    
    // Reference to enemies
    GameObject devilGO;
    GameObject zombieGO;

    DevilScript devil;
    ZombieAI zombie;
    
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (GameObject.FindGameObjectWithTag("HealthBar") != null)
            healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
    }


    private void Update()
    {
        //healthText.text = "Health: " + health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Fixed update do to "jittering effect" of colliders when moving directly into a wall. Not a perfect fix, but it works really well.
        ControllPlayer();
    }

    void ControllPlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        //Debug.Log(moveHorizontal + " " + moveVertical);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (movement != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(movement);


        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    // This may need to be moved onto something else later. Perhaps a damager script?
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            devilGO = GameObject.FindGameObjectWithTag("Devil");
            devil  = devilGO.GetComponent<DevilScript>();

            Debug.Log("FUEGO!!!");
            Destroy(collision.gameObject);
            TakeDamage(devil.fireballDamage);
        }
        else if(collision.gameObject.tag == "Zombie")
        {
            zombieGO = GameObject.FindGameObjectWithTag("Zombie");
            zombie = zombieGO.GetComponent<ZombieAI>();

            TakeDamage(zombie.zombieDamage);
        }
    }

    void TakeDamage(int enemyDamage)
    {
        int damageAmt = enemyDamage;

        if (currentHealth > 0)
        {
            currentHealth -= damageAmt;
        }
        else if (currentHealth == 0)
        {
            Debug.Log("Player is dead. :(");
        }

        // Update player health bar
        healthBar.fillAmount = ( ((currentHealth - damageAmt) * 100) / maxHealth ) / 100;
    }
}
