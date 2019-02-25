using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDestroyer : MonoBehaviour
{
    
    private int randomDestroyer;
    private ParticleSystem bloodParticleSystem;
    private int bodyPartHitPoints = 10;
    public int maxRandomRangeRifle = 0;
    public int maxRandomRangeShotGun = 0;
    public int maxRandomRangeMissile = 0;

    public GameObject bloodExplosion;

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
 
        bloodParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update(){

        if (bloodParticleSystem)
        {
            if (!bloodParticleSystem.IsAlive())
            {
                Debug.Log("Blood particle has destroyed");
                Destroy(bloodParticleSystem);
            }
        }
    }

    private void Destroy(){

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision){
 
            bodyPartHitPoints--;
       
        if (bodyPartHitPoints <= 0){
            Instantiate(bloodExplosion, transform.position, transform.rotation);
            Destroy(gameObject);    
        }
    }
}
