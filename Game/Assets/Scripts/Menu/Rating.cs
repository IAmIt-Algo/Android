using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Rating : MonoBehaviour {

    public Text Position;
    public Text Stars;
    int LowestPosition = 5;
    int HighestPosition = 2;
    int StarsCount = 25;

	// Use this for initialization
	void Start () {
        Position.text = HighestPosition + " - " + LowestPosition;
        Stars.text = StarsCount + "";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
