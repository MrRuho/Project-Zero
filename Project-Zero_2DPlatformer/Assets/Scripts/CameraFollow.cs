using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float smoothTimeY;
    public float smoothTimeX;

    private Vector2 velocity;

    public GameObject followPoint;

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    void Start ()
    {
        smoothTimeY = 0.1f;
        smoothTimeX = 0.1f;

        followPoint = GameObject.FindGameObjectWithTag("CameraPoint");		
	}

    private void Update()
    {
        if (followPoint == null)
        {
            followPoint = GameObject.FindGameObjectWithTag("CameraPoint");
        }
    }

    void FixedUpdate()
    {
        if (Player.dead == false)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, followPoint.transform.position.x, ref velocity.x, smoothTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, followPoint.transform.position.y, ref velocity.y, smoothTimeY);

            transform.position = new Vector3(posX, posY, transform.position.z);
        }

        if(bounds)
        {
            transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
            Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
            Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z)
            );

        }

    }

    //CameraFollowEditor. Poimii kameran max ja minimi arvot jotka voit sitten asettaa arvoiksi menussa.
     public void SetMinCamPosition()
    {
        minCameraPos = gameObject.transform.position;
    }
     public void SetMaxCamPosition()
    {
        maxCameraPos = gameObject.transform.position;
    }

    public void SeePlayer(bool playerIsWisible)
    {
        if (playerIsWisible == false)
        {
            smoothTimeY = 1f;
            smoothTimeX = 1f;
        } else if (playerIsWisible == true) 
        {
            smoothTimeY = 0.1f;
            smoothTimeX = 0.1f;
        }

    }
}
