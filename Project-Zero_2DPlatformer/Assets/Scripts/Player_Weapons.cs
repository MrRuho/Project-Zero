using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapons : MonoBehaviour {

    public Transform firepoint;
    bool haveTurned = true;

    private void Start()
    {
        
    }

    void Update ()
    {
        if (Input.GetKeyDown("left") && haveTurned == false)
        {
            Flip();
        }

        if (Input.GetKeyDown("right") && haveTurned == true)
        {       
            Flip();
        }
 
    }

    private void Flip()
    {
        haveTurned = !haveTurned;
        transform.Rotate(0f, 180f, 0f);
    }

}
