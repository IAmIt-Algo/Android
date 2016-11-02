using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mindblower.Core;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
	
	}
public void BackToMap()
{
    SceneManager.LoadScene(2);
}
}
