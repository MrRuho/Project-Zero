using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDestroyer : MonoBehaviour
{ 

    private int randomDestroyer;
    private int destroyByOverWalking = 1;
    
    public int maxRandomRangeRifle = 0;
    public int maxRandomRangeShotGun = 0;
    public int maxRandomRangeMissile = 0;
 
    void Start() {
    if (Player_Weapons.weapon == 3) {

        randomDestroyer = Random.Range(1, 3);
    } else {

        randomDestroyer = Random.Range(1, 10);
    }
    if (randomDestroyer == 1){

        Destroy(gameObject, 0.05f);
        }
    }


    void Update(){

    }


    private void Destroy(){

        Destroy(gameObject, 0.05f);
    }

    void OnTriggerEnter2D(){

        destroyByOverWalking++;

        if (destroyByOverWalking < 0){
            Debug.Log("ruumis tuhoutuu");
            Destroy(gameObject);
        }

    }   
}