﻿using Mindblower.Core;
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

        [SerializeField]
        private int Result;

        private int stepsNumber;

        public void OnTurtlePushLastTower(Tower tower)
        {
            if (tower.TurtlesCount == 4)
            {
                if (stepsNumber <= 15)
                    Result = 3;
                else if (stepsNumber <= 19)
                    Result = 2;
                else
                    Result = 1;
                if (levelEventsHandler != null)
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelComplete(Result));

                    AddAttemptModel model = new AddAttemptModel();
                    model.LevelName = "4";
                    model.Stars = result;
                    model.Time = 0;

                    IAmItHttpRequest.Post(model, IAmItServerMethods.ADD_ATTEPT, this);
                }
            }

                    AddAttemptModel model = new AddAttemptModel();
                    model.LevelName = "4";
                    model.Stars = result;
                    model.Time = 0;

                    IAmItHttpRequest.Post(model, IAmItServerMethods.ADD_ATTEPT, this);
                }
        }

        void OnDisable()
        {
            var info = new LevelInfo
            {
                LevelId = "Level4",
                StarsCount = Result,
                Time = (int)Time.timeSinceLevelLoad
            };
            if (levelEventsHandler != null)
                ExecuteEvents.ExecuteHierarchy<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelCanceled(info));
            Debug.Log("Cancel");
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
