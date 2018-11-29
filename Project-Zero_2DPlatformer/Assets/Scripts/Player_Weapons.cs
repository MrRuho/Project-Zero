using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapons : MonoBehaviour {

    private float reloadingTime = 0;

    private int clipSize = 0;
    private int ammoCounter = 0;
    private int weaponSwitch = 0;
    
    private bool pistol = false;
    private bool assaultRifle = false;
    private bool shotgun = false;
    private bool missile = false;
    

    public Transform firepoint;
    public GameObject bulletPrefab;

    private void Start()
    {
        WeaponSwitcher(0);
    }

    void Update ()
    {
        Debug.Log(ammoCounter);
        

        // Kaantaa ampumapistetta siihen suntaan mihin hahmo katsoo. Estaa nain ampumasta itseaan.
        if (Input.GetKeyDown("left"))
        {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            
        }

        if (Input.GetKeyDown("right"))
        {
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
           
        }
 
        if (Input.GetButtonDown("Fire2")&& ammoCounter >0)
        {
            Shoot();
            ammoCounter -= 1;
        }

        if (ammoCounter <= 0)
        {
            Debug.Log("Reloading");
            StartCoroutine(Reloading());
        }

    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloadingTime);
        ammoCounter = clipSize;
        Debug.Log("Reloated");
        yield return clipSize;
    }

    void WeaponSwitcher(int CurrentWeapon)
    {
        weaponSwitch = CurrentWeapon;

        switch (weaponSwitch)
        {
            case 1:
                Debug.Log("AssaultRifle");
                clipSize = 30;
                ammoCounter = clipSize;
                reloadingTime = 10.2f;
                break;

            case 2:
                Debug.Log("Shotgun");
                clipSize = 6;
                ammoCounter = clipSize;
                reloadingTime = 10.5f;
                break;

            case 3:
                Debug.Log("Missile Laucher");
                clipSize = 3;
                ammoCounter = clipSize;
                reloadingTime = 10.8f;
                break;

            default:
                Debug.Log("Pistol");
                clipSize = 6;
                ammoCounter = clipSize;
                reloadingTime = 10.0f;
                break;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }

}
