using Mindblower.Core;
using UnityEngine;

namespace Mindblower.Gui
{
    [RequireComponent(typeof(GuiController))]
    public class GuiHandler : GuiBehaviour
    {
        private LevelsLoader levelsLoader;
        private GuiController guiController;

        void Awake()
        {
            levelsLoader = GameObject.FindGameObjectWithTag("CoreController").GetComponent<LevelsLoader>();
            guiController = GetComponent<GuiController>();
        }

        public void OnBackButtonClick()
        {
            levelsLoader.LoadLevel("MapLevel");
        }

        public void OnMapButtonClick()
        {
            guiController.HideLevelComplete();
            guiController.HideLevelGameOver();
            levelsLoader.LoadLevel("MapLevel");
        }

        public void OnRestartButtonClick()
        {
            guiController.HideLevelComplete();
            guiController.HideLevelGameOver();
            levelsLoader.ReloadLevel();
        }

        public void OnChangeLanguageButtonClick()
        {
            guiController.ChangeLanguage();
        }

        public void OnRulesCloseButtonClick()
        {
            guiController.HideRules();
        }

        public void OnRulesButtonClick()
        {
            guiController.ShowRules();
        }
    }
}


