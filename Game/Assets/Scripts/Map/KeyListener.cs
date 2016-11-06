using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Mindblower.Map
{
    public class KeyListener : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("ESCAPE!!!");
                Application.Quit();
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene(11);
            }
        }
    }
}

