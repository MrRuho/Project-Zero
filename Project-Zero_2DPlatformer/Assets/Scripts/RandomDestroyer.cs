﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDestroyer : MonoBehaviour
{
    
    private int randomDestroyer;

    private int bodyPartHitPoints = 15;
    public int maxRandomRangeRifle = 0;
    public int maxRandomRangeShotGun = 0;
    public int maxRandomRangeMissile = 0;

    // Start is called before the first frame update
    void Start() {
        //missile
        if (Player_Weapons.weapon == 3) {
            randomDestroyer = Random.Range(1, maxRandomRangeMissile);

        }  //shotgun
        else if(Player_Weapons.weapon == 2) {
            randomDestroyer = Random.Range(1, maxRandomRangeShotGun);

        } else {
        //start Rifle
            randomDestroyer = Random.Range(1, maxRandomRangeRifle);
        }

        if (randomDestroyer == 1) {

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void Destroy(){
        Destroy(gameObject, 0.05f);
    }

    private void OnCollisionEnter2D(Collision2D collision){
 
            bodyPartHitPoints--;
            Debug.Log("BodyPartHP" + bodyPartHitPoints);
        
        if (bodyPartHitPoints <= 0){
            Destroy(gameObject);
            Debug.Log("BodyPart destroy");
        }
    }
}
