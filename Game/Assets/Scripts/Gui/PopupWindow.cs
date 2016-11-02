using UnityEngine;

namespace Mindblower.Gui
{
    [RequireComponent(typeof(Animator))]
    public class PopupWindow : GuiBehaviour, IPopupWindowState
    {
        private int ShowTriggerId;
        private int HideTriggerId;

        private Animator animator;
        private bool isVisible;

        void Awake()
        {
            animator = GetComponent<Animator>();

            ShowTriggerId = Animator.StringToHash("Show");
            HideTriggerId = Animator.StringToHash("Hide");
        }

        void Start()
        {
            isVisible = false;
        }

        public void Show()
        {
            if (!isVisible)
            {
                IsBusy = true;
                animator.SetTrigger(ShowTriggerId);
                isVisible = true;
            }
        }

        public void Hide()
        {
            if (isVisible)
            {
                IsBusy = true;
                animator.SetTrigger(HideTriggerId);
                isVisible = false;
            }
        }

        public void OnAnimationComplete()
        {
            IsBusy = false;
        }
    }
}
