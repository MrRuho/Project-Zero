using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDestroyer : MonoBehaviour
{
    private int randomDestroyer;
    private int destroyByOverWalking = 3;
    

    public int maxRandomRangeRifle = 0;
    public int maxRandomRangeShotGun = 0;
    public int maxRandomRangeMissile = 0;
    // Start is called before the first frame update
    void Start()
    {
        

        if (Player_Weapons.weapon == 3) // missile
        {
            randomDestroyer = Random.Range(1, maxRandomRangeMissile);

        } else if(Player_Weapons.weapon == 2) //shotgun
        {
            randomDestroyer = Random.Range(1, maxRandomRangeShotGun);

        } else {
            //start Rifle
            randomDestroyer = Random.Range(1, maxRandomRangeRifle);
        }

        if (randomDestroyer == 1) {

            Destroy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Destroy()
    {
        Destroy(gameObject, 0.05f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("PlayerTrigger"))
        {
            Debug.Log("Player over walking");
            destroyByOverWalking--;

            if (destroyByOverWalking <= 0)
            {
                Destroy();
            }
        }
       
    }


}
