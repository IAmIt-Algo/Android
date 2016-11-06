using Mindblower.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level3
{
    public class Level : MonoBehaviour, ITaskEventsHandler
    {
        public static bool IsBusy = false;

        private GameObject levelEventsHandler;

        [SerializeField]
        private TextAsset rules;

        [SerializeField]
        private int Result;

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
                if (stepsNumber <= 3)
                    Result = 3;
                else if (stepsNumber <= 4)
                    Result = 2;
                else
                    Result = 1;
                if (levelEventsHandler != null)
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelComplete(Result));
            }
        }

        void OnDisable()
        {
            var info = new LevelInfo
            {
                LevelId = "Level3",
                StarsCount = Result,
                Time = (int)Time.timeSinceLevelLoad
            };
            if (levelEventsHandler != null)
                ExecuteEvents.ExecuteHierarchy<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelCanceled(info));
            Debug.Log("Cancel");
        }

        public void OnWeightCheck()
        {
            ++stepsNumber;
        }
    }
}
