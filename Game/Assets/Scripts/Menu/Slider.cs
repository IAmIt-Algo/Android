using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour {

    public UnityEngine.UI.Slider slider;

	// Use this for initialization
	void Start () {
        slider.value = AudioListener.volume;
    }
	
	// Update is called once per frame
	void Update () {
        AudioListener.volume = slider.value;
	}
}
