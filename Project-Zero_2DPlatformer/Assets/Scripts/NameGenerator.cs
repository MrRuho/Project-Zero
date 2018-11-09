using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameGenerator : MonoBehaviour {

   public  List<string> names;
   public  string[] lines;

	// Use this for initialization
	void Start ()
    {
        TextAsset nameText = Resources.Load<TextAsset>("Names");

        lines = nameText.text.Split("\n"[0]);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, 50, 50), "generate name"))
        {
            Debug.Log(lines[Random.Range(0, lines.Length)]);
        }
    }
}
