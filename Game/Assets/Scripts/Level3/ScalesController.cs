using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace Mindblower.Level3
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    public class ScalesController : MonoBehaviour, IPointerClickHandler
    {
        private Animator animator;

        [SerializeField]
        private CoinTray leftPan;
        [SerializeField]
        private CoinTray rightPan;
        [SerializeField]
        private GameObject weighButton;

        private bool isPressed = false;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void LeftPutDown()
        {
            animator.SetInteger("weightSide", -1);
        }

        public void RightPutDown()
        {
            animator.SetInteger("weightSide", 1);
        }

        public void Normilize()
        {
            animator.SetInteger("weightSide", 0);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.position.y <= 105)
            {
                if (!isPressed)
                {
                    leftPan.GetComponent<PanAligner>().SetWeighing();
                    rightPan.GetComponent<PanAligner>().SetWeighing();
                    if (leftPan.GetWeight() > rightPan.GetWeight())
                    {
                        LeftPutDown();
                    }
                    else if (leftPan.GetWeight() == rightPan.GetWeight())
                    {
                        Normilize();
                    }
                    else
                    {
                        RightPutDown();
                    }
                    Level.IsBusy = true;
                    weighButton.SetActive(false);
                    ExecuteEvents.ExecuteHierarchy<ITaskEventsHandler>(gameObject, null, (x, y) => x.OnWeightCheck());
                }
                else
                {
                    leftPan.GetComponent<PanAligner>().SetDefault();
                    rightPan.GetComponent<PanAligner>().SetDefault();
                    Normilize();
                    weighButton.SetActive(true);
                    Level.IsBusy = false;
                }
                isPressed = !isPressed;
            }
        }
    }
}

