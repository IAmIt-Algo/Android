using Mindblower.Gui;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mindblower.Core
{
    public class LevelsLoader : MonoBehaviour
    {
        private string runningLevelId;
        private GuiController guiController;

        void Awake()
        {
            runningLevelId = string.Empty;
        }

        private IEnumerator ReloadLevelCoroutine()
        {
            yield return new WaitForGui();
            guiController.ShowSplashScreen();
            yield return new WaitForGui();
            guiController.ShowProgressBar();

            var reloadScene = SceneManager.LoadSceneAsync(runningLevelId);
            while (!reloadScene.isDone)
            {
                guiController.Progress = reloadScene.progress;
                yield return null;
            }

            if (runningLevelId == "MapLevel")
            {
                guiController.HideMapButton();
                guiController.HideChangeLanguageButton();
                guiController.HideRulesButton();
            }
            else
            {
                guiController.ShowMapButton();
                guiController.ShowChangeLanguageButton();
                guiController.ShowRulesButton();
            }

            guiController.HideSplashScreen();
            guiController.HideProgressBar();

            yield return new WaitForGui();
        }

        private IEnumerator LoadLevelCoroutine(string levelId)
        {
            yield return new WaitForGui();
            guiController.ShowSplashScreen();
            yield return new WaitForGui();
            guiController.ShowProgressBar();

            var loadScene = SceneManager.LoadSceneAsync(levelId);
            while (!loadScene.isDone)
            {
                guiController.Progress = loadScene.progress;
                yield return null;
            }

            if (levelId == "MapLevel")
            {
                guiController.HideMapButton();
                guiController.HideChangeLanguageButton();
                guiController.HideRulesButton();
                guiController.HideRules();
            }
            else
            {
                guiController.ShowMapButton();
                guiController.ShowChangeLanguageButton();
                guiController.ShowRulesButton();
            }

            yield return new WaitForGui();

            guiController.HideProgressBar();
            guiController.HideSplashScreen();

            yield return new WaitForGui();

            if (levelId != "MapLevel")
            {
                guiController.ShowRules();
            }

            runningLevelId = levelId;
        }

        void Start()
        {
            guiController = GameObject.FindGameObjectWithTag("GuiController").GetComponent<GuiController>();
        }

        public void LoadLevel(string levelId)
        {
            if (runningLevelId == levelId)
            {
                StartCoroutine(ReloadLevelCoroutine());
            }
            else
            {
                StartCoroutine(LoadLevelCoroutine(levelId));
            }
        }

        public void ReloadLevel()
        {
            StartCoroutine(ReloadLevelCoroutine());
        }
    }
}
