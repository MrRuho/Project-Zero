using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapons : MonoBehaviour {

    private float fireRateControl = 0.0f;
    private float canFireAgain = 0.0f;
    private float reloadingTime = 0.0f;
    private float facingDirection = 0f;
    
    private int clipSize = 0;
    private int ammoCounter = 0;
    private int weaponSwitch = 0;
    private int weaponSynck = 0; // mikäli tämä arvo on eri kuin weapon niin käynnistää aseenvaihto koodin  ** void WeaponSwitcher(int CurrentWeapon) **
    public static int weapon = 0; //PowerUps.cs asettaa tamaan arvon joka maarittaa mitä asetta käytetään.
   

    private bool reloadingWait = false;
    private bool automatic = false;
    
    public Transform firepoint;
    GameObject bulletPrefab;
    public GameObject shootBarrelFireEffect;
    

    private void Start()
    {
        weapon = 0; // asettaa pelin alussa aseeen nollaksi.
        weaponSynck = 0; // asettaa pelin alussa synkin nollaksi.
        WeaponSwitcher(0); // asettaa pelaajan aloitus aseen.
    }

    void Update ()
    {
        if (weaponSynck != weapon)
        {
            weaponSynck = weapon;
            Debug.Log("NewPowerUp");
            WeaponSwitcher(weapon); 
        }

        // Ei automaattiset aseet.
        if (automatic == false && weaponSwitch != 2)
        {         
            if (Input.GetButtonDown("Fire2") && ammoCounter > 0 && canFireAgain >= fireRateControl)
            {
                Debug.Log(ammoCounter);
                Shoot();
                ammoCounter -= 1;
                canFireAgain = 0.0f;
                StartCoroutine(FireRate());
                Instantiate(shootBarrelFireEffect, firepoint.position, firepoint.rotation);
            }
        }

        //Automaatti aseet.
        if (automatic == true)
        {    
            if (Input.GetButton("Fire2") && ammoCounter > 0 && canFireAgain >= fireRateControl)
            {
                Debug.Log(ammoCounter);
                Shoot();
                ammoCounter -= 1;
                canFireAgain = 0.0f;
                StartCoroutine(FireRate());
                Instantiate(shootBarrelFireEffect, firepoint.position, firepoint.rotation);
            }
        }

        //shotgun
        if (automatic == false && weaponSwitch == 2)
        {  
            if (Input.GetButtonDown("Fire2") && ammoCounter > 0 && canFireAgain >= fireRateControl)
            {
                Debug.Log(ammoCounter);
                StartCoroutine(ShotGunShoot(10));
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

    // Maarittaa pelaajan kaytossa olevan aseen arvot, sekä maarittaa luoti tyypin.
    void WeaponSwitcher(int CurrentWeapon)
    {
        weaponSwitch = CurrentWeapon;

        switch (weaponSwitch)
        {
            case 1:
                Debug.Log("AssaultRifle");
                clipSize = 15;
                fireRateControl = 0.15f;
                reloadingTime = 2.0f;
                ammoCounter = clipSize;
                canFireAgain = fireRateControl;
                bulletPrefab = (GameObject)Resources.Load("prefabs/Player_Bullet_0", typeof(GameObject));
                automatic = true;
                break;

            case 2:
                Debug.Log("Shotgun");
                clipSize = 6;
                fireRateControl = 0.8f;  
                reloadingTime = 1.5f;
                ammoCounter = clipSize;
                canFireAgain = fireRateControl;
                bulletPrefab = (GameObject)Resources.Load("prefabs/Player_Bullet_2", typeof(GameObject));
                automatic = false;
                break;

            case 3:
                Debug.Log("Missile Laucher");
                clipSize = 3;
                fireRateControl = 0.3f;
                reloadingTime = 1.8f;
                ammoCounter = clipSize;
                canFireAgain = fireRateControl;
                bulletPrefab = (GameObject)Resources.Load("prefabs/Player_Bullet_3", typeof(GameObject));
                automatic = false;
                break;

            default:
                Debug.Log("Pistol");
                clipSize = 12;
                fireRateControl = 0.0f;
                reloadingTime = 1.0f;
                ammoCounter = clipSize;
                canFireAgain = fireRateControl;
                bulletPrefab = (GameObject)Resources.Load("prefabs/Player_Bullet_0", typeof(GameObject));
                automatic = false;
                break;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }

    IEnumerator ShotGunShoot(int shotgunPulletsCount)
    {
        Instantiate(shootBarrelFireEffect, firepoint.position, firepoint.rotation);
        for (int i = 0; i < shotgunPulletsCount; i++)
        {
            float angleRandomaiser = Random.Range(-5.0f, 12.0f);
            float positonRandomiserY = Random.Range(0.23f, -0.3f);
            float positonRandomiserX = Random.Range(1.374f, 2.90f);
            firepoint.localPosition = new Vector3(positonRandomiserX, positonRandomiserY, 0);
            firepoint.transform.eulerAngles = new Vector3(0.0f, facingDirection, angleRandomaiser);
            Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            FirepointReset();  
        }
        yield return 0;
    }
    void FirepointReset() // palauttaa aseen normaaliin kulmaan ja asentoon.
    {
        firepoint.localPosition = new Vector3(1.374f, 0.23f, 0);
        firepoint.transform.eulerAngles = new Vector3(0,0,0);
    }
}
