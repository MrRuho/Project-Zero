using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet_ShotGun : MonoBehaviour {

    public int pelletCount;
    public float spreadAngle;
    public float pelletFireVel;
    
    public GameObject pellet;
    public Transform barrelExit;
    private Rigidbody2D rb2d;
    List<Quaternion> pellets;

	// Use this for initialization
	void Start ()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        pellets = new List<Quaternion>(pelletCount);

        for (int i = 0; i < pelletCount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            fire();
        }
    }

    void fire ()
    {
        int i = 0;
        foreach (Quaternion quat in pellets)
        {
            pellets[i] = Random.rotation;
            GameObject p = Instantiate(pellet, barrelExit.position, barrelExit.rotation);
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
           // p.GetComponents<Rigidbody2D>().AddForce(p.transform.right * pelletFireVel);
            i++;
        }
    }
}
