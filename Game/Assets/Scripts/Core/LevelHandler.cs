using Mindblower.Gui;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

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

        public void OnLevelComplete(LevelInfo info)
        {
            guiController.StopBackgroundMusic();
            guiController.PlayVictory();
            int prevResult = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, 0);
            int saveResult = Mathf.Max(prevResult, info.StarsCount);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, saveResult);
            StartCoroutine(ShowOnLevelComplete(info.StarsCount));
            SendRequest(info);
        }

        private IEnumerator ShowOnLevelGameOver()
        {
            guiController.HideMapButton();
            yield return new WaitForGui();
            guiController.ShowLevelGameOver();
        }

        public void OnLevelGameOver(LevelInfo info)
        {
            guiController.StopBackgroundMusic();
            guiController.PlayFail();
            StartCoroutine(ShowOnLevelGameOver());
            SendRequest(info);
        }

        public void OnLevelLoaded(TextAsset rulesFile)
        {
            guiController.LoadRules(rulesFile);
        }

        public void OnLevelCanceled(LevelInfo info)
        {
            SendRequest(info);
        }

        private void SendRequest(LevelInfo info)
        {
            var model = new AddAttemptModel
            {
                LevelName = info.LevelId,
                Stars = info.StarsCount,
                Time = info.Time
            };
            IAmItHttpRequest.Post<AddAttemptModel>(model, IAmItServerMethods.ADD_ATTEMPT, this);
        }

        public void OnLogin()
        {
            throw new NotImplementedException();
        }

        public void OnFail(string code)
        {
            Debug.Log("Request failed, code = " + code);
        }

        public void OnGet<T>(T responseModel)
        {
            throw new NotImplementedException();
        }

        public void OnPost(string s)
        {
            Debug.Log("Level: Post: response is " + s);
        }

        public void OnLogOut()
        {
            throw new NotImplementedException();
        }
    }
}
