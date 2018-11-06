using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 100;
  
    public GameObject deathEffect;

    public void TakeDamage (int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Destroy(gameObject); // NOTE! alla oleva instantiate in tapahduttava ennen objectin tuhoamista. Tama on valiaikaisesti nyt ensin tai muuten tuhoutumista ei tapahdu. 
       // Instantiate(deathEffect, transform.position, Quaternion.identity);
       // Destroy(gameObject);
    }
}
