using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    private bool zombieHasCreated = false;

    public Transform zombieSpawnPoint;
    public GameObject spawnEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CreateEnemysPoint.PlayerHasEnteredZone == true && zombieHasCreated == false)
        {

            Instantiate(spawnEnemy, zombieSpawnPoint.position, zombieSpawnPoint.rotation);
            zombieHasCreated = true;
        }
        if (Player.dead == true && zombieHasCreated == true)
        {
            zombieHasCreated = false;
        }
    }
}
