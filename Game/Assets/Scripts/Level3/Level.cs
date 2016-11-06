using Mindblower.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Core;
using System;

namespace Mindblower.Level3
{
    public class Level : MonoBehaviour, ITaskEventsHandler, IAmItRequestListener
    {
        public static bool IsBusy = false;

        private GameObject levelEventsHandler;

        [SerializeField]
        private TextAsset rules;

        private int stepsNumber;

        void Awake()
        {
            levelEventsHandler = GameObject.FindGameObjectWithTag("CoreController");
            stepsNumber = 0;
        }

        void Start()
        {
            if (levelEventsHandler != null)
                ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelLoaded(rules));
        }

        public void OnCoinCheck(Coin coin)
        {
            if (coin.Weight == 10)
            {
                if (levelEventsHandler != null)
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelGameOver());
            }
            else
            {
                int result;
                if (stepsNumber <= 3)
                    result = 3;
                else if (stepsNumber <= 4)
                    result = 2;
                else
                    result = 1;
                if (levelEventsHandler != null)
                {
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelComplete(result));

                    AddAttemptModel model = new AddAttemptModel();
                    model.LevelName = "3";
                    model.Stars = result;
                    model.Time = 0;

                    IAmItHttpRequest.Post(model, IAmItServerMethods.ADD_ATTEPT, this);
                }
            }
        }

        public void OnWeightCheck()
        {
            ++stepsNumber;
        }

        public void OnLogin()
        {
            throw new NotImplementedException();
        }

        public void OnFail(string code)
        {
            throw new NotImplementedException();
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
