using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level5
{
    [RequireComponent(typeof(Crate))]
    public class CrateController : MonoBehaviour, IPointerClickHandler
    {
        private const string ActionTypeParam = "ActionType";
        private const string ActionTriggerParam = "ActionTrigger";

        private Crate crate;
        private Animator animator;

        void Awake()
        {
            crate = GetComponent<Crate>();
            animator = GetComponent<Animator>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (crate.Content)
            {
                case ContentType.Shield:
                    animator.SetInteger(ActionTypeParam, 0);
                    animator.SetTrigger(ActionTriggerParam);
                    break;
                case ContentType.Sword:
                    animator.SetInteger(ActionTypeParam, 1);
                    animator.SetTrigger(ActionTriggerParam);
                    break;
                case ContentType.SwordAndShield:
                    int actionType = Random.Range(0, 2);
                    animator.SetInteger(ActionTypeParam, actionType);
                    animator.SetTrigger(ActionTriggerParam);
                    break;
            }

            ExecuteEvents.ExecuteHierarchy<ITaskEventsHandler>(gameObject, null, (x, y) => x.OnCreateClick());
        }
    }
}
