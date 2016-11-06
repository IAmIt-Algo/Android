using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mindblower.Gui;
public class ToSignUp : MonoBehaviour {

    public void ButtonClick() {
        SceneManager.LoadScene("SignUpLevel");
    }
}
