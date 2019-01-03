using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Bullet_Missile : MonoBehaviour
{
    public GameObject impactEffect;
    Tilemap tilemap;
    GameObject tilemapGameObject;

    // Use this for initialization
    void Start ()
    {
        tilemapGameObject = GameObject.FindGameObjectWithTag("Ground");

        if (tilemapGameObject != null)
        {
            tilemap = tilemapGameObject.GetComponent<Tilemap>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
 
        if (collision.gameObject.tag == "Ground")
        {
            Vector3 hitPosition = Vector3.zero;
            if (tilemapGameObject == collision.gameObject)
            {
                foreach (ContactPoint2D hit in collision.contacts)
                {
                    hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                    tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                    Instantiate(impactEffect, transform.position, transform.rotation);
                }
            }
        }  
    }
}
