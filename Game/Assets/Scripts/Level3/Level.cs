using Mindblower.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Mindblower.Level3
{
    public class Level : MonoBehaviour, ITaskEventsHandler
    {
        public static bool IsBusy = false;

        private GameObject levelEventsHandler;

        [SerializeField]
        private TextAsset rules;

        [SerializeField]
        private int Result = 0;

        private bool isGameOvered = false;

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
            isGameOvered = true;
            if (coin.Weight == 10)
            {
                var info = new LevelInfo
                {
                    LevelId = "Level3",
                    StarsCount = Result,
                    Time = (int)Time.timeSinceLevelLoad
                };

                if (levelEventsHandler != null)
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelGameOver(info));
            }
            else
            {
                if (stepsNumber <= 3)
                    Result = 3;
                else if (stepsNumber <= 4)
                    Result = 2;
                else
                    Result = 1;

                var info = new LevelInfo
                {
                    LevelId = "Level3",
                    StarsCount = Result,
                    Time = (int)Time.timeSinceLevelLoad
                };

                if (levelEventsHandler != null)
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelComplete(info));
            }
        }

        void OnDisable()
        {
            if (!isGameOvered)
            {
                var info = new LevelInfo
                {
                    LevelId = "Level3",
                    StarsCount = Result,
                    Time = (int)Time.timeSinceLevelLoad
                };
                if (levelEventsHandler != null)
                    ExecuteEvents.ExecuteHierarchy<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelCanceled(info));
            }
            Debug.Log("Cancel");
        }

        public void OnWeightCheck()
        {
            ++stepsNumber;
        }
    }
}
