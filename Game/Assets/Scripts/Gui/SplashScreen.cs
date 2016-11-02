using UnityEngine;

namespace Mindblower.Gui
{
    [RequireComponent(typeof(Animator))]
    public class SplashScreen : GuiBehaviour, ISplashScreenState
    {
        [SerializeField]
        private ProgressBar progressBar;

        private int ShowTriggerId;
        private int HideTriggerId;

        private Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
            ShowTriggerId = Animator.StringToHash("Show");
            HideTriggerId = Animator.StringToHash("Hide");
        }

        public void Show()
        {
            animator.SetTrigger(ShowTriggerId);
        }

        public void Hide()
        {
            animator.SetTrigger(HideTriggerId);
        }

        public void ShowProgressBar()
        {
            progressBar.gameObject.SetActive(true);
        }

        public void HideProgressBar()
        {
            progressBar.gameObject.SetActive(false);
        }

        public void OnAnimationCompleted()
        {
            IsBusy = false;
        }

        public float Progress
        {
            set
            {
                progressBar.Progress = value;
            }
        }
    }
}
