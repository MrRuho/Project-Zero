using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 100;
    private bool dead = false; 
    public GameObject deathEffect;
    public Animator animator;

    public GameObject spawnCorpse;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage (int damage)
    {
        health -= damage;

        if(health <= 0 && !dead)
        {
            dead = true;
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            Instantiate(spawnCorpse, transform.position, transform.rotation);
            animator.SetBool("Dying", true);         
            // Jokaisella vihollisella tulee olla Dying (tasmalleen samalla nimella) animaatio jonka viimeisessa freimissa on -- animation -> add evet Die();--
        }
    }
    // Aktivoituu kuolinanimaation loputtua.
    void Die()
    {
        Destroy(gameObject);
    }
}
