using UnityEngine;
using System.Collections;

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
            if (Input.GetKeyDown(KeyCode.Menu))
            {
                Application.LoadLevel(11);
            }
        }
    }
}

