using UnityEngine;
using System.Collections;

namespace Mindblower.Gui
{
    [RequireComponent(typeof(Multilanguage))]
    [RequireComponent(typeof(MultilanguageRules))]
    public class GuiController : GuiBehaviour
    {
        [SerializeField]
        private SplashScreen splashScreen;
        [SerializeField]
        private PopupWindow levelGameOverWindow;
        [SerializeField]
        private PopupWindow levelCompleteWindow;
        [SerializeField]
        private ResultSetter levelCompleteResult;
        [SerializeField]
        private PopupWindow rulesWindow;
        [SerializeField]
        private GameObject mapButton;
        [SerializeField]
        private GameObject changeLanguageButton;
        [SerializeField]
        private GameObject rulesButton;
        [SerializeField]
        private AudioClip victorySound;
        [SerializeField]
        private AudioClip failSound;

        private Multilanguage multilanguage;
        private MultilanguageRules multilanguageRules;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (splashScreen == null)
                splashScreen = GetComponentInChildren<SplashScreen>();
            Debug.Assert(splashScreen != null, "Splash screen reference is missed.");
            Debug.Assert(levelGameOverWindow != null, "Game over window reference is missed.");
            Debug.Assert(levelCompleteWindow != null, "Complete window reference is missed.");
            Debug.Assert(rulesWindow != null, "Rules window reference is missed.");
            Debug.Assert(levelCompleteResult != null, "Complete window reference is missed.");
            Debug.Assert(mapButton != null, "Map button reference is missed.");
            
            multilanguage = GetComponent<Multilanguage>();
            multilanguageRules = GetComponent<MultilanguageRules>();
        }

        public void ShowSplashScreen()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                splashScreen.Show();
            }
        }

        public void HideSplashScreen()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                splashScreen.Hide();
            }
        }

        public void ShowProgressBar()
        {
            splashScreen.ShowProgressBar();
        }

        public void HideProgressBar()
        {
            splashScreen.HideProgressBar();
        }

        public float Progress
        {
            set { splashScreen.Progress = value; }
        }

        public void ShowLevelGameOver()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                levelGameOverWindow.Show();
            }
        }

        public void HideLevelGameOver()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                levelGameOverWindow.Hide();
            }
        }

        public void ShowLevelComplete(int result)
        {
            if (!IsBusy)
            {
                levelCompleteResult.Result = result;
                levelCompleteWindow.Show();
            }
        }

        public void HideLevelComplete()
        {
            if (!IsBusy)
            {
                levelCompleteWindow.Hide();
            }
        }

        public void ShowRules()
        {
            if (!IsBusy)
            {
                rulesWindow.Show();
            }
        }

        public void HideRules()
        {
            if (!IsBusy)
            {
                rulesWindow.Hide();
            }
        }

        public void ShowMapButton() { mapButton.SetActive(true); }
        public void HideMapButton() { mapButton.SetActive(false); }
        public void ShowChangeLanguageButton() { changeLanguageButton.SetActive(true); }
        public void HideChangeLanguageButton() { changeLanguageButton.SetActive(false); }
        public void ShowRulesButton() { rulesButton.SetActive(true); }
        public void HideRulesButton() { rulesButton.SetActive(false); }

        public void ChangeLanguage()
        {
            multilanguage.ChangeLanguage();
            multilanguageRules.ChangeLanguage();
        }

        public void LoadRules(TextAsset rulesFile)
        {
            multilanguageRules.LoadRules(rulesFile);
        }

        public void PlayVictory()
        {
            AudioSource.PlayClipAtPoint(victorySound, Vector3.zero);
        }

        public void PlayFail()
        {
            AudioSource.PlayClipAtPoint(failSound, Vector3.zero);
        }

        public void StopBackgroundMusic()
        {
            AudioSource backgroundPlayer = GameObject.FindObjectOfType<AudioSource>();
            backgroundPlayer.enabled = false;
        }
    }
}

