using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapons : MonoBehaviour {

    private float fireRateControl = 0.0f;
    private float canFireAgain = 0.0f;
    private float reloadingTime = 0.0f;
   
    private int clipSize = 0;
    private int ammoCounter = 0;
    private int weaponSwitch = 0;
    
    private bool reloadingWait = false;
    
    public Transform firepoint;
    public GameObject bulletPrefab;
    

    private void Start()
    {
        WeaponSwitcher(1);
    }

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

        // Pistol aseen ammunta. WeaponSwitcher(0);
        if (weaponSwitch == 0)
        {
            if (Input.GetButtonDown("Fire2") && ammoCounter > 0)
            {
                Debug.Log(ammoCounter);
                Shoot();
                ammoCounter -= 1;
            }
        }

        //AssaultRifle aseen ammunta. WeaponSwitcher(1); 
        if (weaponSwitch == 1)
        { 
            if (Input.GetButton("Fire2") && ammoCounter > 0 && canFireAgain >= fireRateControl)
            {
                Debug.Log(ammoCounter);
                Shoot();
                ammoCounter -= 1;
                canFireAgain = 0.0f;
                StartCoroutine(FireRate());
            }
        }

        // aseen lataus kaynnistyy aseen ollessa tyhja.
        if (ammoCounter <= 0 && reloadingWait == false)
        {
            reloadingWait = true;
            Debug.Log("Reloading");
            StartCoroutine(Reloading());
        }
    }
    // aseiden tulinopeus kontrolli.
    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireRateControl);
        canFireAgain = fireRateControl;
        yield return canFireAgain;
    }

    //Aseiden lataus Coroutine.
    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloadingTime);
        ammoCounter = clipSize;
        Debug.Log("Reloated");
        reloadingWait = false;
        yield return clipSize;
    }

    // Maarittaa pelaajan kaytossa olevan aseen arvot.
    void WeaponSwitcher(int CurrentWeapon)
    {
        weaponSwitch = CurrentWeapon;

        switch (weaponSwitch)
        {
            case 1:
                Debug.Log("AssaultRifle");
                clipSize = 30;
                ammoCounter = clipSize;
                fireRateControl = 0.1f;
                canFireAgain = fireRateControl;
                reloadingTime = 2.0f;
                break;

            case 2:
                Debug.Log("Shotgun");
                clipSize = 6;
                ammoCounter = clipSize;
                reloadingTime = 1.5f;
                break;

            case 3:
                Debug.Log("Missile Laucher");
                clipSize = 3;
                ammoCounter = clipSize;
                reloadingTime = 1.8f;
                break;

            default:
                Debug.Log("Pistol");
                clipSize = 6;
                ammoCounter = clipSize;
                reloadingTime = 1.0f;
                break;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }
}
