using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 100;
  
    public GameObject deathEffect;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void TakeDamage (int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
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
