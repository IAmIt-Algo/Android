using Mindblower.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Core;
using System;

namespace Mindblower.Level4
{
    public class Level : MonoBehaviour, ITaskEventsHandler, IAmItRequestListener
    {
        private GameObject levelEventsHandler;

        [SerializeField]
        private TextAsset rules;

        private int stepsNumber;

        public void OnTurtlePushLastTower(Tower tower)
        {
            if (tower.TurtlesCount == 4)
            {
                int result;
                if (stepsNumber <= 15)
                    result = 3;
                else if (stepsNumber <= 19)
                    result = 2;
                else
                    result = 1;
                if (levelEventsHandler != null)
                {
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelComplete(result));

                    AddAttemptModel model = new AddAttemptModel();
                    model.LevelName = "4";
                    model.Stars = result;
                    model.Time = 0;

                    IAmItHttpRequest.Post(model, IAmItServerMethods.ADD_ATTEPT, this);
                }
            }
        }

        public void OnTurtlePush()
        {
            ++stepsNumber;
        }

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
