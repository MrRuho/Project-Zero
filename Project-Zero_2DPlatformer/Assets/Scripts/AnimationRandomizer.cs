using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRandomizer : MonoBehaviour {


  public  Animator randomizer;
	// Use this for initialization
	void Start ()
    {
        randomizer.Play("MushroomIdle", -1, Random.Range(0.0f, 1.0f));
    }
	
	
}
