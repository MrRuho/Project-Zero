using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootKill : MonoBehaviour
{
    void Start()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.speed--;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.tag == "Enemy" && Player.sliding == true)
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.speed = player.speed -1.2f;
            
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(100);
        }      
    }
}
