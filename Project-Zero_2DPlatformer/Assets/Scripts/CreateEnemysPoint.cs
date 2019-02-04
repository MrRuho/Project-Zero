using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemysPoint : MonoBehaviour
{
    public static bool PlayerHasEnteredZone = false; //EnemySpawnPoint.cs
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.dead == true && PlayerHasEnteredZone == true)
        {
            PlayerHasEnteredZone = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerHasEnteredZone == false)
        {
            PlayerHasEnteredZone = true;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                GameObject.Destroy(enemy);
            }
            GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
            foreach (GameObject PowerUp in powerUps)
            {
                GameObject.Destroy(PowerUp);
            }
        }
    }
}
