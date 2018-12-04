using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet_ShotGun : MonoBehaviour
{
    /*    - Aseen spawn pisteestä(se kohta mistä ammukset lähtee) ilmestyy luoteja 50 kpl peräkanaa.Haulien välissä on pieni satunnainen tulo ja jokainen
     hauli saa oman satunnaisen ammuntakulman tietyllä välillä.

    - for looppi toimii niin kauan kunnes kaikki haulit on ammuttu piipusta ulos.


    *** For loopin sisällä ***
    - Haulin satunnainen ammuntakulma.
    - Haulin satunnainen odotus ennen seuraavan haulin ulostuloa.
    - Hauli laskuri.
    wait for randon seconds.
    spawn bullet randon angle.
     *******************************
    Ammunnan jälkeen on lataustauko kunnes ammunta on jälleen mahdollinen.


    int haulit. 
    -----------*/
    public float speed = 20f;
    public int damage = 10;
    int bulletCount = 30;

    public GameObject pullets;
    public Transform FirePoint;
 
   

	// Use this for initialization
	void Start ()
    {
     
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            for (int i = 0; i < bulletCount; i++)
            {

            }
        }
    }

    void fire ()
    {
   
    }
}
