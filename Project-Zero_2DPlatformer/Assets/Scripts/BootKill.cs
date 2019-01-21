using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootKill : MonoBehaviour
{
    
    private Player player;

    private void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy" && Player.sliding == true)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(100);
        }      
    }
}
