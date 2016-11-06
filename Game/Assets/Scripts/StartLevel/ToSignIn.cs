using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToSignIn : MonoBehaviour {

    public void ButtonClick()
    {
        SceneManager.LoadScene("SignInLevel");
    }
}
