﻿using Mindblower.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Mindblower.Level2
{
    public class Level : MonoBehaviour, ITaskEventsHandler
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
        private int Result = 0;

        private int stepsNumber;
        private bool isGameOvered = false;

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
                isGameOvered = true;

                if (stepsNumber <= 6)
                    Result = 3;
                else if (stepsNumber <= 8)
                    Result = 2;
                else
                    Result = 1;

                var info = new LevelInfo
                {
                    LevelId = "Level2",
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
                    LevelId = "Level2",
                    StarsCount = Result,
                    Time = (int)Time.timeSinceLevelLoad
                };
                ExecuteEvents.ExecuteHierarchy<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelCanceled(info));
            }
            Debug.Log("Cancel");
        }
    }
}
