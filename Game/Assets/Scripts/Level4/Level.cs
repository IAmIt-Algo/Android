﻿using Mindblower.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Mindblower.Level4
{
    public class Level : MonoBehaviour, ITaskEventsHandler
    {
        private GameObject levelEventsHandler;

        [SerializeField]
        private TextAsset rules;

        [SerializeField]
        private int Result = 0;

        private int stepsNumber;
        private bool isGameOvered = false;


        public void OnTurtlePushLastTower(Tower tower)
        {
            if (tower.TurtlesCount == 4)
            {
                isGameOvered = true;

                if (stepsNumber <= 15)
                    Result = 3;
                else if (stepsNumber <= 19)
                    Result = 2;
                else
                    Result = 1;

                var info = new LevelInfo
                {
                    LevelId = "Level4",
                    StarsCount = Result,
                    Time = (int)Time.timeSinceLevelLoad
                };

                if (levelEventsHandler != null)
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelComplete(info));
            }
        }

        void OnDisable()
        {
            if (!isGameOvered) {
                var info = new LevelInfo
                {
                    LevelId = "Level4",
                    StarsCount = Result,
                    Time = (int)Time.timeSinceLevelLoad
                };
                if (levelEventsHandler != null)
                    ExecuteEvents.ExecuteHierarchy<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelCanceled(info));
            }
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
    }
}
