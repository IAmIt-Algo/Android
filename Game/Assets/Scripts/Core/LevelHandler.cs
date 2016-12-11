using Mindblower.Gui;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading;

namespace Mindblower.Core
{
    public class LevelHandler : MonoBehaviour, ILevelEventsHandler, IAmItRequestListener
    {
        private GuiController guiController;
        private bool _visible = false;
        private string _code = "";
        private bool _isShowed = false;

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
            _isShowed = false;
            _visible = true;
            _code = code;
        }

        void OnGUI()
        {
            if (_visible)
            {
                GUI.Box(new Rect(Screen.width / 2 - Screen.width * 4 / 10, Screen.height * 23 / 32, Screen.width * 4 / 5, Screen.height / 4), "");
                GUI.Label(new Rect(Screen.width / 2 - Screen.width * 4 / 10, Screen.height * 23 / 32, Screen.width * 4 / 5, Screen.height / 4), _code);
                Thread myThread = new Thread(new ThreadStart(Pause));
                myThread.Start();
            }
        }

        public void Pause()
        {
            if (!_isShowed)
            {
                _isShowed = true;
                Debug.Log("Count");
                Thread.Sleep(3000);
                _visible = false;
            }
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
