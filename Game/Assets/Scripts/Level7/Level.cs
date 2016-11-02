using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Mindblower.Core;

namespace Mindblower.Level7
{
    public class Level : MonoBehaviour
    {
		public static bool IsBusy = false;

        [SerializeField]
        private TextAsset rules;

        private GameObject levelEventsHandler;

        void Awake()
        {
            levelEventsHandler = GameObject.FindGameObjectWithTag("CoreController");
        }

        void Start()
        {
            if (levelEventsHandler != null)
                ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelLoaded(rules));
        }
    }
}

