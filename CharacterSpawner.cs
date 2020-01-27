using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] characters;
    public Transform spawnPoint;

    public static GameObject player;

    public static bool isPlayerReady = false; // the player has been spawned 

    // Start is called before the first frame update
    void Awake()
    {
        player = Instantiate(characters[CharacterSelection.index], spawnPoint.position, spawnPoint.rotation);
        isPlayerReady = true;
    }
}
