using UnityEngine;
using System.Collections;

public class ButtonCreator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {


        Rect rect = new Rect(10, 30, 100, 20);
        if (GUI.Button(rect, "Audio On"))

        {

            GUI.Button(rect, "Audio Of");

        }
        if (GUI.Button(rect, "Audio Of"))

        {

            GUI.Button(rect, "Audio On");

        }


    }
}
