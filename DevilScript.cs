using UnityEngine;
using UnityEngine.UI;

public class DevilScript : MonoBehaviour
{
    public GameObject Fireball;
    float distance;
    float fireRate = 2f;
    float nextFire = 2f;
    Collision coll;
    Transform Devil;
    Transform Player;
    float speed;
    float health = 10f;
    public float currentHealth;
    bool isDead;

    public int fireballDamage = 5;

    [Header("Unity Stuff")]
    public Image healthBar;

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
    void Start()
    {
        Devil = GameObject.FindGameObjectWithTag("Devil").transform;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = (Random.Range(1f, 1.5f));

        //Setting current health when the enemy first spawns
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        //Looks at the player. Not sure why I have this or if it even works.
        Vector3 lookAtGoal = new Vector3(Player.position.x, this.transform.position.y, Player.position.z);

        Vector3 direction = lookAtGoal - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 2f); //Complex stuff that deals with rotation.


        distance = Vector3.Distance(Player.position, this.transform.position);


        //If hes close enough and can fire his fire balls, he does so.
        if (Vector3.Distance(Player.position, this.transform.position) < 10 && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            ShootFire();
        }
        //If he is close, but cannot fire his fireball, he stands still.
        else if (Vector3.Distance(Player.position, this.transform.position) < 10)
        {
            transform.Translate(Vector3.zero);
        }
        //Else he just moves on the 8 way directional method
        else
            MathMove();
    }

    //Fireball shooting script. Creates a new variable and adds velocity to the fireball (This also knocks back the player)
    void ShootFire()
    {
        var FuegoBol = (GameObject)Instantiate(Fireball, this.transform.position, this.transform.rotation);
        FuegoBol.GetComponent<Rigidbody>().velocity = FuegoBol.transform.forward * 10;
        Destroy(FuegoBol, 2f);
    }

    public void MathMove()
    {
        //PvED Means Player versus Enemy Direction.
        float PvEDx = 0;
        float PvEDz = 0;
        PvEDx = Player.position.x - this.transform.position.x;
        PvEDz = Player.position.z - this.transform.position.z;

        if (PvEDz < -1 && PvEDx < -1)
        {
            transform.Translate(new Vector3(-1, 0, -1) * Time.deltaTime * speed, Space.World);

            // OG transform.Translate(new Vector3(-1, 0, -1) * Time.deltaTime);


            //transform.eulerAngles = new Vector3(0, -225, 0);
            //Quaternion target = Quaternion.Euler(0, 45, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2f);
        }

        else if (PvEDz < -1 && PvEDx > 1)
        {

            transform.Translate(new Vector3(1, 0, -1) * Time.deltaTime * speed, Space.World);
        }

        else if (PvEDz > 1 && PvEDx < -1)
        {

            transform.Translate(new Vector3(-1, 0, 1) * Time.deltaTime * speed, Space.World);
        }

        else if (PvEDz > 1 && PvEDx > 1)
        {

            transform.Translate(new Vector3(1, 0, 1) * Time.deltaTime * speed, Space.World);
        }

        else if (PvEDx < -1)
        {

            transform.Translate(-Vector3.right * Time.deltaTime * speed, Space.World);
        }

        else if (PvEDz > 1)
        {

            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);

        }

        else if (PvEDz < -1)
        {

            transform.Translate(-Vector3.forward * Time.deltaTime * speed, Space.World);

        }

        else if (PvEDx > 1)
        {

            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        }
    }

    //This handles his damage and checks his health. Also handles the healthbar
    public void TakeDamage(int amount, Vector3 hitpoint)
    {
        if (isDead)
            return;

        this.currentHealth -= amount;

        healthBar.fillAmount = this.currentHealth / health;

        if (this.currentHealth <= 0)
        {
            Death();
        }
    }

    //Deletes gameobject, and adds a score.
    void Death()
    {
        ScoreScript.scoreAmount += 1000;
        isDead = true;
        npcMaster.CallEventNpcDie();
        // Wait random seconds to destroy gameobject
        Destroy(this.gameObject, Random.Range(2, 5));
        Debug.Log("Ding dong devil is dead!");
    }
}
