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
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelComplete(result));
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
    }
}
