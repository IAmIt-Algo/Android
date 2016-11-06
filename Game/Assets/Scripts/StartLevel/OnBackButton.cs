using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mindblower.Gui;

public class OnBackButton : MonoBehaviour {

    public void ButtonClick()
    {
        SceneManager.LoadScene("StartLevel");
    }
}
