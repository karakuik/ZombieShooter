using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Ryan, Please Don't forget to clean this up Later, kthnxbai
//Also figure out mathformula: for every 10 zombies, spawn 1 Devil
//
// The meat of the project. Deals with everything involving spawning. Should be modified to fit other levels as I will probably want to change that later.
//
public class ZombieSpawner : MonoBehaviour
{
    //Gets the zombie spawn, may want to introduce spawnpoints instead.
    GameObject zombieSpawn;
    //Our arrays (Used for checking how many zombies there are by checking the length of these arrays! Very useful!
    GameObject[] ZombieCount;
    GameObject[] DevilCount;
    
    public bool Spawn = true;
    public bool waveSpawn = true;

    //Waves & enemy amounts
    public int totalWaves = 5;
    public int currentWave = 0;
    public int totalEnemy = 10;
    public int numEnemy = 0;
    public float timeBetweenSpawns = 1f;
    public float zombieTimer = 0.0f;

    //Movement of Eye
    public float speed = 5f;
    public float leftAndRightEdge = 15f;
    public float topAndBottomEdge = 15f;
    public float chanceToChangeDirections = 0.01f;

    public int chosenEnemy; // enemy who is going to drop the key item

    // Start is called before the first frame update
    void Start()
    {
        zombieSpawn = GameObject.Find("ZombieSpawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawn)
        {
            zombieTimer += Time.fixedDeltaTime;

            if (numEnemy < totalEnemy && waveSpawn)
            {
                if (zombieTimer >= timeBetweenSpawns)
                {
                    //Change back to SpawnZombie();
                    spawnZombie();
                    
                    if(totalEnemy%10 == 0)
                        spawnDevil();
                    
                    zombieTimer = 0.0f;
                }
            }

            else if (numEnemy == totalEnemy)
            {
                waveSpawn = false;
            }

            else if (numEnemy == 0)
            {
                waveSpawn = true;
                currentWave++;
                //CHANGE THIS TO 10 IN FINAL VERSION
                totalEnemy = currentWave*2;
            }
        }
    }

    //Moves the eye.
    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        pos.z += speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); // Move right
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); // Move left
        }

        else if (pos.z < -topAndBottomEdge)
            speed = Mathf.Abs(speed);
        else if (pos.z > topAndBottomEdge)
            speed = -Mathf.Abs(speed);

        else if (Random.value < chanceToChangeDirections)
        {
            speed *= -1; // Change direction
            pos.x -= speed * Time.deltaTime*20;
            pos.z -= speed * Time.deltaTime*20;
        }

        //Array length checker, in fixed update so less checks.
        arraylengthfinder();
    }
    
    void spawnZombie()
    {
        GameObject Zombitch = (GameObject)Instantiate(Resources.Load("Zombie"), zombieSpawn.transform.position + (new Vector3(0f, -1f, 0f)), Quaternion.identity);
        
        //numEnemy++
        // Instantiate(Resources.Load("Zombie"), zombieSpawn.transform.position + (new Vector3(0f, -1f, 0f)), Quaternion.identity);
    }

    void spawnDevil()
    {
        GameObject Devil = (GameObject)Instantiate(Resources.Load("Devil"), zombieSpawn.transform.position + (new Vector3(0f, -1f, 0f)), Quaternion.identity);
        //numEnemy++
    }

    void arraylengthfinder()
    {
        //checks how many zombies & devils and totals them up so we can check how many are in the scene. Very handy!
        ZombieCount = GameObject.FindGameObjectsWithTag("Zombie");
        int zombieTotal = ZombieCount.GetLength(0);
        DevilCount = ZombieCount = GameObject.FindGameObjectsWithTag("Devil");
        int devilTotal = ZombieCount.GetLength(0);

        numEnemy = zombieTotal + devilTotal;
        //Debug.Log(numEnemy);
    }
}
