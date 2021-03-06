﻿using Mindblower.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Mindblower.Level1
{
    public class Level : MonoBehaviour, ITaskEventsHandler
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

        [SerializeField]
        private int Result = 0;

        private bool isGameOvered = false;

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
                isGameOvered = true;

                var info = new LevelInfo
                {
                    LevelId = "Level1",
                    StarsCount = Result,
                    Time = (int)Time.timeSinceLevelLoad
                };

                if (levelEventsHandler != null)
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelGameOver(info));
                Debug.Log("Game Over!");
            }

            if (RightCoast.HasGameOver && BoatObject.transform.position == LeftCoast.BoatDock.transform.position)
            {
                isGameOvered = true;

                var info = new LevelInfo
                {
                    LevelId = "Level1",
                    StarsCount = Result,
                    Time = (int)Time.timeSinceLevelLoad
                };

                if (levelEventsHandler != null)
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelGameOver(info));
                Debug.Log("Game Over!");
            }
        }

        private void CheckComplete()
        {
            if (RightCoast.HasAllCharacters)
            {
                isGameOvered = true;

                if (stepsNumber <= 17)
                    Result = 3;
                else if (stepsNumber <= 20)
                    Result = 2;
                else
                    Result = 1;

                var info = new LevelInfo
                {
                    LevelId = "Level1",
                    StarsCount = Result,
                    Time = (int)Time.timeSinceLevelLoad
                };

                if (levelEventsHandler != null)
                    ExecuteEvents.ExecuteHierarchy<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelComplete(info));
                Debug.Log("Victory!");
            }
        }
        void OnDisable()
        {
            if (!isGameOvered)
            {
                var info = new LevelInfo
                {
                    LevelId = "Level1",
                    StarsCount = Result,
                    Time = (int)Time.timeSinceLevelLoad
                };
                ExecuteEvents.ExecuteHierarchy<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelCanceled(info));
            }
            Debug.Log("Cancel");
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


    }
}
