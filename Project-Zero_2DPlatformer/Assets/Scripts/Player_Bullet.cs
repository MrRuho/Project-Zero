using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Bullet : MonoBehaviour {

    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb2d;
    public GameObject impactEffect;
    public GameObject blood;

    public GameObject tilemapGameObject;
    Player_Bullet bullet;
    Tilemap tilemap;

    // Use this for initialization
    void Start ()
    {
        rb2d.velocity = transform.right * speed;
        if (tilemapGameObject != null)
        {
            tilemap = tilemapGameObject.GetComponent<Tilemap>();
        }
    }

    private void Update()
    {
        
    }

    /*  void OnTriggerEnter2D( Collider2D collision)
      {

          if (collision.isTrigger == false)
          {
              Enemy enemy = collision.GetComponent<Enemy>();

              if (enemy != null)
              {
                  Instantiate(blood, transform.position, transform.rotation); // NOTE. veriroiskahduksen lopullinen kulma asennetaan BloodSplachConrol.cs:ssa.
                  enemy.TakeDamage(damage);  
              }
              Destroy(gameObject);
              Instantiate(impactEffect, transform.position, transform.rotation);

          }

      }*/

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero; 
        Debug.Log("BUllet hits");

        if (tilemapGameObject == collision.gameObject)
        {
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                Destroy(gameObject);
            }
        }

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
