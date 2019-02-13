using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDestroyer : MonoBehaviour
{
    int randomDestroyer;
    // Start is called before the first frame update
    void Start()
    {
        if (Player_Weapons.weapon == 3)
        {
            randomDestroyer = Random.Range(1, 3);
        } else {
            randomDestroyer = Random.Range(1, 10);
        }

        if (randomDestroyer == 1) {
            Destroy(gameObject, 0.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
