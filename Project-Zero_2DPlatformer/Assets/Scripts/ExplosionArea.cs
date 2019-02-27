﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") && !collision.CompareTag("Player"))
        {
            if (collision.CompareTag("Enemy"))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(100);
            }
            if (collision.CompareTag("CorpsePlayer"))
            {
                
            }

           // Destroy(collision.gameObject);
        }
    }
}
