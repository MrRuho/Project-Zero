using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpsePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.IgnoreLayerCollision(12, 11);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnCollisionStay(Collision collide)
    {
        //Output the name of the GameObject you collide with
        Debug.Log("I hit the GameObject : " + collide.gameObject.name);
    }
}
