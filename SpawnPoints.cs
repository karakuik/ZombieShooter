using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] spawnPoints;
    public GameObject[] enemies;
    GameObject[] ZombieCount;
    GameObject[] DevilCount;
    int closestSpawnPoint;
    public static bool spawnAllowed;
    public GameObject player;


    //From ZombieSpawner.cs
    public bool Spawn = true;
    public bool waveSpawn = true;

    //Waves & enemy amounts
    public int totalWaves = 5;
    public int currentWave = 1;
    public int totalEnemy = 2;
    public int numEnemy = 0;
    public float timeBetweenSpawns = 1f;
    public float zombieTimer = 0.0f;


    void Start()
    {
        spawnAllowed = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //enemySpawnHandler();
        enemySpawner();
    }

    //Commented this out, trying to figure it out
    /* void enemySpawnHandler()
     {
         numEnemy = arraylengthfinder();
         if (waveSpawn)
         {
             zombieTimer += Time.fixedDeltaTime;

             if (numEnemy < totalEnemy && waveSpawn)
             {
                 if (zombieTimer >= timeBetweenSpawns)
                 {
                     //Change back to SpawnZombie();
                     spawnZombie();

                     if (totalEnemy % 10 != 0)
                         spawnDevil();

                     zombieTimer = 0.0f;
                 }
             }

             else if (numEnemy == totalEnemy)
             {
                 waveSpawn = false;
             }           

         }

         waveSpawn = spawnWave(numEnemy);

         if (numEnemy == 0)
         {
             waveSpawn = spawnWave(numEnemy);
             currentWave++;
             //CHANGE THIS TO 10 IN FINAL VERSION
             totalEnemy = currentWave * 2;

         }
     }*/

    void enemySpawner()
    {
        if (waveSpawn)
        {
            zombieTimer += Time.fixedDeltaTime;

            if (numEnemy < totalEnemy && waveSpawn)
            {
                if (zombieTimer >= timeBetweenSpawns)
                {
                    //Change back to SpawnZombie();
                    spawnZombie();

                    
                    zombieTimer = 0.0f;
                }
            }
        }

        if (numEnemy == 0)
        {
            waveSpawn = true;
            currentWave++;
        }
        if(numEnemy == totalEnemy)
        {
            totalEnemy = currentWave * 2;
            waveSpawn = false;
        }
        arraylengthfinder();
    }

   /* bool spawnWave(int enemies)
    {
        if (enemies == 0)
        {
            currentWave++;
            totalEnemy = currentWave * 2;
            return true;
        }

        else return false;
            
    }*/

    void spawnZombie()
    {

         ZombieCount = GameObject.FindGameObjectsWithTag("Zombie");

            GameObject Zombitch = (GameObject)Instantiate(Resources.Load("Zombie"),
                        getClosestSpawnPoint(spawnPoints).position +
                        (new Vector3(0f, 1f, 0f)), Quaternion.identity);
            
        

    }

    void spawnDevil()
    {        
            GameObject Devil = (GameObject)Instantiate(Resources.Load("Devil"), getClosestSpawnPoint(spawnPoints).position + (new Vector3(0f, -1f, 0f)), Quaternion.identity);        
    }


    Transform getClosestSpawnPoint(Transform[] spawnPoints)
    {
        Transform closest = null;
        Vector3 playerPos = player.transform.position;
        float minDistance = Mathf.Infinity;

        foreach(Transform sPoint in spawnPoints)
        {
            float distance = Vector3.Distance(sPoint.position, playerPos);
            if(distance < minDistance)
            {
                closest = sPoint;
                minDistance = distance;
            }
        }
        return closest;
    }

    int arraylengthfinder()
    {
        //checks how many zombies & devils and totals them up so we can check how many are in the scene. Very handy!
        ZombieCount = GameObject.FindGameObjectsWithTag("Zombie");
        int zombieTotal = ZombieCount.GetLength(0);
        DevilCount = ZombieCount = GameObject.FindGameObjectsWithTag("Devil");
        int devilTotal = DevilCount.GetLength(0);

        numEnemy = zombieTotal + devilTotal;
        Debug.Log(numEnemy);
        return numEnemy;
    }



    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int waveNumber = 1;

    void Update()
    {
        if (countdown <= 0f)
        {
            spawnWave();
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator spawnWave()
    {
        waveNumber++;

        for (int i = 0; i < waveNumber; i++)
        {
            spawnZombie();
            yield return new WaitForSeconds(0.5f);
        }

    }
}
