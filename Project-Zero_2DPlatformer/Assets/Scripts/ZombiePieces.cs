using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePieces : MonoBehaviour{

    int timeToDestroyThis = 5;

    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update(){
        if (timeToDestroyThis <= 0){
            Destroy(gameObject);
        }
    }

    public int zombieBodyPartsHasDestoyedCounter(){
        
        return timeToDestroyThis --;     
    }
}
