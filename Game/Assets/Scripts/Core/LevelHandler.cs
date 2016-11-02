using Mindblower.Gui;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mindblower.Core
{
    public class LevelHandler : MonoBehaviour, ILevelEventsHandler
    {
        private GuiController guiController;

        void Start()
        {
            guiController = GameObject.FindGameObjectWithTag("GuiController").GetComponent<GuiController>();
        }

        private IEnumerator ShowOnLevelComplete(int result)
        {
            guiController.HideMapButton();
            yield return new WaitForGui();
            guiController.ShowLevelComplete(result);
        }

        public void OnLevelComplete(int result)
        {
            guiController.StopBackgroundMusic();
            guiController.PlayVictory();
            int prevResult = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, 0);
            int saveResult = Mathf.Max(prevResult, result);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, saveResult);
            StartCoroutine(ShowOnLevelComplete(result));
        }

        private IEnumerator ShowOnLevelGameOver()
        {
            guiController.HideMapButton();
            yield return new WaitForGui();
            guiController.ShowLevelGameOver();
        }

        public void OnLevelGameOver()
        {
            guiController.StopBackgroundMusic();
            guiController.PlayFail();
            StartCoroutine(ShowOnLevelGameOver());
        }

        public void OnLevelLoaded(TextAsset rulesFile)
        {
            guiController.LoadRules(rulesFile);
        }
    }
}
