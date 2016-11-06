using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mindblower.Gui;

namespace Mindblower.Core
{
    [RequireComponent(typeof(LevelsLoader))]
    public class CoreRunner : MonoBehaviour
    {
        private GuiController guiController;
        private LevelsLoader levelsLoader;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            levelsLoader = GetComponent<LevelsLoader>();
            SceneManager.LoadScene("GuiScene");
        }
        
        IEnumerator Start()
        {
            guiController = GameObject.FindGameObjectWithTag("GuiController").GetComponent<GuiController>();
            if (guiController == null)
            {
                guiController = GameObject.FindObjectOfType<GuiController>();
                Debug.Assert(guiController != null, "Gui is not found.");
            }

            yield return null; //Let gui scene full load
            SceneManager.LoadScene("StartLevel");

            Destroy(this); 
        }
    }
}

