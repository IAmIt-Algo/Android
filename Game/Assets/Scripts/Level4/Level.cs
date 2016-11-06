using Mindblower.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level4
{
    public class Level : MonoBehaviour, ITaskEventsHandler
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
    }
}
