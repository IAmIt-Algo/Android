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
                int result;
                if (stepsNumber <= 3)
                    result = 3;
                else if (stepsNumber <= 4)
                    result = 2;
                else
                    result = 1;
                if (levelEventsHandler != null)
                    ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelComplete(result));
            }
        }

        public void OnWeightCheck()
        {
            ++stepsNumber;
        }
    }
}
