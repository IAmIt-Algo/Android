using Mindblower.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Core;
using System;
using Newtonsoft.Json;

namespace Mindblower.Level1
{
    public class Level : MonoBehaviour, ITaskEventsHandler, IAmItRequestListener
    {
        [HideInInspector]
        public static bool IsBusy;

        private int stepsNumber;

        [SerializeField]
        private int bestStepsNumber;
        [SerializeField]
        private int goodStepsNumber;
        [SerializeField]
        private int badStepsNumber;

        [SerializeField]
        private Coast LeftCoast;
        [SerializeField]
        private Coast RightCoast;
        [SerializeField]
        private Boat BoatObject;

        [SerializeField]
        private TextAsset rules;

        void Awake()
        {
            stepsNumber = 0;
            levelEventsHandler = GameObject.FindGameObjectWithTag("CoreController");
        }

        void Start()
        {
            if (levelEventsHandler != null)
                ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelLoaded(rules));
        }

        private GameObject levelEventsHandler;

        public void SetEventsHandler(GameObject eventsHandler)
        {
            levelEventsHandler = eventsHandler;
        }

        private void CheckGameOver()
        {
            if (LeftCoast.HasGameOver && BoatObject.transform.position == RightCoast.BoatDock.transform.position)
            {
                if (levelEventsHandler != null)
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelGameOver());
                Debug.Log("Game Over!");
            }

            if (RightCoast.HasGameOver && BoatObject.transform.position == LeftCoast.BoatDock.transform.position)
            {
                if (levelEventsHandler != null)
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelGameOver());
                Debug.Log("Game Over!");
            }
        }

        private void CheckComplete()
        {
            if  (RightCoast.HasAllCharacters)
            {
                int result;
                if (stepsNumber <= 17)
                    result = 3;
                else if (stepsNumber <= 20)
                    result = 2;
                else
                    result = 1;

                if (levelEventsHandler != null)
                {
                    ExecuteEvents.ExecuteHierarchy<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelComplete(result));


                    AddAttemptModel model = new AddAttemptModel();
                    model.LevelName = "1";
                    model.Stars = result;
                    model.Time = 0;
                    
                    IAmItHttpRequest.Post(model, IAmItServerMethods.ADD_ATTEPT, this);

                }
                Debug.Log("Victory!");
            }
        }

        public void OnBoatMoved()
        {
            ++stepsNumber;
            CheckGameOver();
        }

        public void OnCharacterMoved()
        {
            ++stepsNumber;
            CheckComplete();
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
