using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public void BackToMap()
    {
        SceneManager.LoadScene("MapLevel");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void Logout()
    {
        SceneManager.LoadScene("StartLevel");
    }
    public void Rating()
    {
        SceneManager.LoadScene("Rating");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuLevel");
    }
}