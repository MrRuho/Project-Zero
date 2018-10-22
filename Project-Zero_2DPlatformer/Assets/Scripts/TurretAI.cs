using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    public int currentHealth;
    public int maxHealth;

    public float distance;
    public float wakeRange;
    public float shootIterval;
    public float bulletSpeed = 100;
    public float bulletTimer;

    public bool awake = false;
    public bool lookingRight = true;

    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootPointLeft;
    public Transform shootPointRight;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;    
    }

    void Update()
    {
        anim.SetBool("Awake", awake);
        anim.SetBool("LookingRight", lookingRight);

        RangeCheck();

        if (target.transform.position.x > transform.position.x)
        {
            lookingRight = true;
        }
        if (target.transform.position.x < transform.position.x)
        {
            lookingRight = false;
        }

    }

    // Mittaa etaisyyta valittuun kohteeseen. (Tassa tapauksessa pelaaja.) Kohteen voi vaihtaa Turretin paneelista. wakeRangen voi maarittaa paneelista.
    // Ehtojen tayttessa kaynnistaa Awake animaation. (Turretti nousee ylos maasta.)
    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if(distance < wakeRange)
        {
            awake = true;
        }
        if (distance > wakeRange)
        {
            awake = false;
        }

    }


}
