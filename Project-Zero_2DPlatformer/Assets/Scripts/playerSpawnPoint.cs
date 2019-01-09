using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawnPoint : MonoBehaviour
{
    public static bool spawNewSoldier = false; //Player.cs kertoo milloin tata tarvittee kayttaa.
    public GameObject nextSoldier;
    public Transform spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (spawNewSoldier == true)
        {
            Instantiate(nextSoldier, spawnpoint.position, spawnpoint.rotation);
            spawNewSoldier = false;
        }

    }
}
