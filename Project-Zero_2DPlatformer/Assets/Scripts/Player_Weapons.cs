using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapons : MonoBehaviour {

    public Transform firepoint;

    void Update ()
    {
        if (Input.GetKeyDown("left"))
        {
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            
        }

        if (Input.GetKeyDown("right"))
        {
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
           
        }
 
    }

}
