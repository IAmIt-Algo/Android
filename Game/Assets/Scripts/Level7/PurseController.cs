using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Mindblower.Level7
{
    [RequireComponent(typeof(Animator))]
    public class PurseController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private LibraPurse libraPurse;

        [SerializeField]
        private int coinWeight;

        private Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
			if (!Level.IsBusy) 
			{
				animator.SetTrigger("Throw");
				libraPurse.PushCoin(coinWeight);
			}
        }
    }
}

