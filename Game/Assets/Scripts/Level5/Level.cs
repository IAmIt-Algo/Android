using Mindblower.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Core;
using System;

namespace Mindblower.Level5
{
    public class Level : MonoBehaviour, IStoneClickHandler, ITaskEventsHandler, IAmItRequestListener
    {
        private const int cratesNumber = 3;
        private List<Crate> crates;

        private GameObject levelEventsHandler;

        [SerializeField]
        private TextAsset rules;

        private int stepsNumber;

        void Awake()
        {
            crates = new List<Crate>();
            crates.AddRange(GetComponentsInChildren<Crate>());

            if (crates.Count != cratesNumber)
            {
                Debug.LogErrorFormat("Number of crates is not equal to {0}", cratesNumber);
            }

            levelEventsHandler = GameObject.FindGameObjectWithTag("CoreController");
        }

        private void GenerateContents()
        {
            bool[] used = new bool[cratesNumber];
            for (int i = 0; i < used.Length; ++i)
                used[i] = false;

            for (int i = 0; i < cratesNumber; ++i)
            {
                bool isDone = false;
                while (!isDone)
                {
                    int contentTypeIndex = Random.Range(0, cratesNumber);
                    ContentType content = (ContentType)contentTypeIndex;
                    if (!used[contentTypeIndex])
                    {
                        used[contentTypeIndex] = true;
                        crates[i].Content = content;
                        isDone = true;
                    }
                }
            }
        }

        private void GenerateIcons()
        {
            ContentType swordAndShieldCrateIcon = ContentType.Shield;

            for (int i = 0; i < cratesNumber; ++i)
            {
                if (crates[i].Content == ContentType.SwordAndShield)
                {
                    int iconTypeIndex = Random.Range(0, 2);
                    ContentType icon = (ContentType)iconTypeIndex;

                    crates[i].Icon = icon;
                    swordAndShieldCrateIcon = icon;

                    break;
                }
            }

            ContentType lastIcon = ContentType.Shield;

            for (int i = 0; i < cratesNumber; ++i)
            {
                if (crates[i].Content != ContentType.SwordAndShield && crates[i].Content != swordAndShieldCrateIcon)
                {
                    crates[i].Icon = ContentType.SwordAndShield;
                    lastIcon = crates[i].Content;
                    break;
                }
            }

            for (int i = 0; i < cratesNumber; ++i)
            {
                if (crates[i].Content == swordAndShieldCrateIcon)
                {
                    crates[i].Icon = lastIcon;
                    break;
                }
            }
        }

        void Start()
        {
            GenerateContents();
            GenerateIcons();

            if (levelEventsHandler != null)
                ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelLoaded(rules));
        }

        public void OnStoneClicked()
        {
            foreach (var crate in crates)
            {
                if (crate.Icon != crate.Content)
                {
                    if (levelEventsHandler != null)
                        ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelGameOver());
                    return;
                }
            }

            int result;
            if (stepsNumber <= 1)
                result = 3;
            else if (stepsNumber <= 2)
                result = 2;
            else
                result = 1;

            if (levelEventsHandler != null)
            {
                ExecuteEvents.Execute<ILevelEventsHandler>(levelEventsHandler, null, (x, y) => x.OnLevelComplete(result));

                AddAttemptModel model = new AddAttemptModel();
                model.LevelName = "5";
                model.Stars = result;
                model.Time = 0;

                IAmItHttpRequest.Post(model, IAmItServerMethods.ADD_ATTEPT, this);
            }
        }

        public void OnCreateClick()
        {
            ++stepsNumber;
        }

        public void OnLogin()
        {
            throw new NotImplementedException();
        }

        public void OnFail(string code)
        {
            throw new NotImplementedException();
        }

        public void OnGet(string response)
        {
            throw new NotImplementedException();
        }

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
