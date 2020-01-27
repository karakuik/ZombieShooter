using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Invoke("grenadeThrown", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void grenadeThrown()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        Destroy(this.gameObject, 0.5f);
    }

}
