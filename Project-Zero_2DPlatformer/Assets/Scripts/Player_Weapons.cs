using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapons : MonoBehaviour {

    public Transform firepoint;
    public GameObject bulletPrefab;

    void Update ()
    {

        // Kaantaa ampumapistetta siihen suntaan mihin hahmo katsoo. Estaa nain ampumasta itseaan.
        if (Input.GetKeyDown("left"))
        {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            
        }

        if (Input.GetKeyDown("right"))
        {
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
           
        }
 
        if (Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }

}
