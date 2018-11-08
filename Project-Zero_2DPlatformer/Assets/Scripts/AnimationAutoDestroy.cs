using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAutoDestroy : MonoBehaviour {

    public float delay = 0f;

    public GameObject destroyThisGameObject;
    bool animationPlayed = false;

    // Use this for initialization
    void Start()
    {
      /*  Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
        {
            animationPlayed = true;
        }*/

        
    }
    private void Update()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
        {
            animationPlayed = true;
        }
        if (animationPlayed)
        {
            Destroy(destroyThisGameObject);
        }
    }
    
}
