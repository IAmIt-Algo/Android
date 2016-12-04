using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToSignIn : MonoBehaviour {

    public void ButtonClick()
    {
        if (PlayerPrefs.HasKey("Token"))
            SceneManager.LoadScene("MapLevel");
        else
        {
            SceneManager.LoadScene("SignInLevel");
        }
    }
}
