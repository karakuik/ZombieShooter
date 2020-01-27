using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Pretty much useless. Needs to be optimized.

public class FireBallScript : MonoBehaviour
{
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }
}
