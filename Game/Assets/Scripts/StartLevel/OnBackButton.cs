using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class OnBackButton : MonoBehaviour {

    public void ButtonClick()
    {
        SceneManager.LoadScene("StartLevel");
    }
}
