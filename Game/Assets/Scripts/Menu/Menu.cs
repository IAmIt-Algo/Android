using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void BackToMap()
    {
        SceneManager.LoadScene("MapLevel");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
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
