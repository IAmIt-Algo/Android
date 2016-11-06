using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mindblower.Gui;

public class SignIn : MonoBehaviour {


    public void ButtonClick()
    {
        SceneManager.LoadScene("MapLevel");
    }
}
