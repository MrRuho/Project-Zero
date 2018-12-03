using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapons : MonoBehaviour {

    private float fireRateControl = 0.0f;
    private float canFireAgain = 0.0f;
    private float reloadingTime = 0.0f;

    public int weapon = 0; //Valiaikainen aseenvaihto editorin kautta.
    private int clipSize = 0;
    private int ammoCounter = 0;
    private int weaponSwitch = 0;
    
    private bool reloadingWait = false;
    private bool automatic = false;
    
    public Transform firepoint;
    GameObject bulletPrefab;
    

    private void Start()
    {
        WeaponSwitcher(weapon); //Valiaikainen aseenvaihto editorin kautta.
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

        // Ei automaattiset aseet.
        if (automatic == false)
        {
            if (Input.GetButtonDown("Fire2") && ammoCounter > 0 && canFireAgain >= fireRateControl)
            {
                Debug.Log(ammoCounter);
                Shoot();
                ammoCounter -= 1;
                canFireAgain = 0.0f;
                StartCoroutine(FireRate());
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
                fireRateControl = 0.3f;  
                reloadingTime = 1.5f;
                ammoCounter = clipSize;
                canFireAgain = fireRateControl;
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
                clipSize = 6;
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
}
