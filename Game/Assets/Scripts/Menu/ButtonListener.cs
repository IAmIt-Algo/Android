using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class ButtonListener : MonoBehaviour {

	// Use this for initialization
	public void Start () {
       
	}
    public void Back() {
        Application.LoadLevel(2);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
