using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraFollow))]
public class CameraFollowEditor : Editor {

    public override void OnInspectorGUI()
    {
        //Luo editoriin napit joilla voidaan asettaa kameralle rajat joita se ei ylita. Max (aarimmainen oikea ylanurkka) Min ( aarimmainen vasen alanurkka)
        DrawDefaultInspector();

        CameraFollow cf = (CameraFollow)target;
        
        if (GUILayout.Button("Set Min Cam Pos"))
        {
            cf.SetMinCamPosition();
        }
        if (GUILayout.Button("Set Max Cam Pos"))
        {
            cf.SetMaxCamPosition();
        }

    }

    
	
}
