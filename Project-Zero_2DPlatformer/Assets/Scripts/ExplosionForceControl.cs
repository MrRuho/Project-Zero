using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForceControl : MonoBehaviour
{
    public float delayTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delay());     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator delay() {
        yield return new WaitForSeconds(delayTime);
        DestroyComponent();
        yield return 0;
    }

    void DestroyComponent()
    {
        Destroy(GetComponent<PointEffector2D>());
    }

}
