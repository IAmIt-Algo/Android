﻿using Mindblower.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Core;
using System;

namespace Mindblower.Level2
{
    public class Level : MonoBehaviour, ITaskEventsHandler, IAmItRequestListener
    {
        public static bool IsBusy = false;

        [SerializeField]
        private int bestStepsNumber;
        [SerializeField]
        private int goodStepsNumber;
        [SerializeField]
        private int badStepsNumber;

        [SerializeField]
        private Bucket bucket3;
        [SerializeField]
        private Bucket bucket5;

        [SerializeField]
        private TextAsset rules;

        [SerializeField]
        private int Result;

        private int stepsNumber;
        private GameObject levelEventsHandler;

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

        public void OnWaterPoured()
        {
            ++stepsNumber;
            if (bucket3.CurrentVolume == 4 || bucket5.CurrentVolume == 4)
            {
                if (stepsNumber <= 6)
                    Result = 3;
                else if (stepsNumber <= 8)
                    Result = 2;
                else
                    Result = 1;

                if (levelEventsHandler != null)
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelComplete(Result));

                    AddAttemptModel model = new AddAttemptModel();
                    model.LevelName = "2";
                    model.Stars = result;
                    model.Time = 0;

                    IAmItHttpRequest.Post(model, IAmItServerMethods.ADD_ATTEPT, this);
                }
            }
        }

        public void OnLogin()
        {
            throw new NotImplementedException();
        }
        }
        void OnDisable()
        {
            var info = new LevelInfo
            {
                LevelId = "Level2",
                StarsCount = Result,
                Time = (int)Time.timeSinceLevelLoad
            };
            ExecuteEvents.ExecuteHierarchy<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelCanceled(info));
            Debug.Log("Cancel");
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
