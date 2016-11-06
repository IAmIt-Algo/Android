using Mindblower.Gui;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Newtonsoft.Json;

namespace Mindblower.Core
{
    public class LevelHandler : MonoBehaviour, ILevelEventsHandler, IAmItRequestListener
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

        public void OnLevelCanceled(LevelInfo info)
        {
            var model = new AddAttemptModel
            {
                LevelName = info.LevelId,
                Stars = info.StarsCount,
                Time = info.Time
            };
            var data = JsonConvert.SerializeObject(model);
            
            IAmItHttpRequest.post(data,IAmItServerMethods.ADD_ATTEMPT, this);
        }

        public void OnLogin()
        {
            throw new NotImplementedException();
        }

        public void OnFail(string code)
        {
            Debug.Log("Request failed, code = " + code);
        }

        public void OnGet(string response)
        {
            throw new NotImplementedException();
        }

        public void OnPost(string s)
        {
            throw new NotImplementedException();
        }

        public void OnLogOut()
        {
            throw new NotImplementedException();
        }
    }
}
