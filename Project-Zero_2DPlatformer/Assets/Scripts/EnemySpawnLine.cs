using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnLine : MonoBehaviour
{
    private EnemySpawnPoint spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint.GetComponents<EnemySpawnPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {
            spawnPoint.TimeSpawnEnemy();
        }
    }
}
