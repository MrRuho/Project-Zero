using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    float smoothTimeY;
    float smoothTimeX;

    private bool timeToReturn = false;
    private int cameraControl = 0; // kontrolloi sitä että kamera etsii uuden kohteen vain kerran.

    public static bool cameraHasReturn = false;

    private Vector2 velocity;

    public GameObject followPoint;

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    void Start ()
    {
        smoothTimeY = 0.1f;
        smoothTimeX = 0.1f;    	
	}

    private void Update()
    {
        if (followPoint == null)
        {
            followPoint = GameObject.FindGameObjectWithTag("CameraPoint");
        }
        if (Player.dead == false && cameraHasReturn == true && cameraControl == 0)
        {         
            followPoint = GameObject.FindGameObjectWithTag("CameraPoint");
            timeToReturn = false;
            cameraHasReturn = false;
        }
        else if (Player.dead == true && timeToReturn == false && cameraControl == 0)
        {
            cameraControl = 1;          
            followPoint = GameObject.FindGameObjectWithTag("DeadPlayerCameraPoint");
            StartCoroutine(CameraReturnToStartPoint());
        }
        else if (timeToReturn == true && cameraHasReturn == false && cameraControl == 1)
        {
            cameraControl = 0; 
            followPoint = GameObject.FindGameObjectWithTag("CameraStartAndReturnPoint");
            StartCoroutine(CameraHasReturn());
        }    
    }

    IEnumerator CameraReturnToStartPoint()
    {
        yield return new WaitForSeconds(1f);
        yield return timeToReturn = true;
    }

    IEnumerator CameraHasReturn() {
        yield return new WaitForSeconds(1f);
        yield return cameraHasReturn = true;
    }

    void FixedUpdate()
    {
        if (followPoint != null)
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
