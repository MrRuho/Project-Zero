using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float smoothTimeY;
    public float smoothTimeX;

    private Vector2 velocity;

    public GameObject player;

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    void Start ()
    {

      player = GameObject.FindGameObjectWithTag("CameraPoint");
		
	}

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);

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
}
