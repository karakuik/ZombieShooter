using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject Player;
    public float pullback;
    public float xBounds = 13.5f;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(Player.transform.position.x, -xBounds, xBounds), pullback, 
                                         Mathf.Clamp(Player.transform.position.z, -xBounds, xBounds));
    }
}
