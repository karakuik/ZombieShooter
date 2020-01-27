using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TestWaves : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] spawnPoints;
    GameObject[] ZombieCount;
    GameObject[] DevilCount;
    int closestSpawnPoint;
    public GameObject player;


    //Waves & enemy amounts
    private int totalEnemy;
    public int numEnemy = 0;


    //From the video
    public float timeBetweenWaves = 5f;
    private float countdown = 5f;
    private int waveNumber = 0;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f)
        {
            Debug.Log("Inside countdown");
            if(numEnemy <= waveNumber)
            StartCoroutine(SpawnWave());
            //spawnZombie();
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        numEnemy = arraylengthfinder();
    }

    IEnumerator SpawnWave()
    {
        Debug.Log("Inside SpawnWave");
        waveNumber++;
        totalEnemy = waveNumber * 2;
        
        for (int i = 0; i < waveNumber; i++)
        {
            Debug.Log("In the Loop!");
            spawnZombie();
           yield return new WaitForSeconds(0.5f);
        }
    }


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

        foreach (Transform sPoint in spawnPoints)
        {
            float distance = Vector3.Distance(sPoint.position, playerPos);
            if (distance < minDistance)
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


}
