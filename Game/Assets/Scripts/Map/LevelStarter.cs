using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mindblower.Core;
using System.Collections.Generic;

namespace Mindblower.Map
{
    public class LevelStarter : MonoBehaviour
    {
        [SerializeField]
        private string levelId;

        private LevelsLoader levelsLoader;

        [SerializeField]
        private List<SpriteRenderer> stars;

        void Awake()
        {
            levelsLoader = GameObject.FindGameObjectWithTag("CoreController").GetComponent<LevelsLoader>();
        }

        void Start()
        {
            int result = PlayerPrefs.GetInt(levelId, 0);
            stars.ForEach(x => x.enabled = false);
            for (int i = 0; i < result; ++i)
                stars[i].enabled = true;
        }

        void OnMouseDown()
        {
            PlayerPrefs.SetFloat("Camera_Position_Y", Camera.main.transform.position.y);
            levelsLoader.LoadLevel(levelId);
        }
    }
}

