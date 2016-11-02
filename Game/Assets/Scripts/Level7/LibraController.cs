using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Mindblower.Level7
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class LibraController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Animator libraPurseAnimator;
        [SerializeField]
        private LibraPurse libraPurse;
        [SerializeField]
        private LibraWeightWriter libraWeightWriter;
                
        private bool isPushed;

        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            isPushed = false;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isPushed)
            {
                libraPurseAnimator.SetTrigger("PushUp");
                spriteRenderer.enabled = true;

                libraPurse.RemoveCoins();
                libraWeightWriter.SetWeight(libraPurse.Weight);

                isPushed = false;
				Level.IsBusy = false;
            }
            else
            {
                libraPurseAnimator.SetTrigger("PushDown");
                spriteRenderer.enabled = false;

                libraWeightWriter.SetWeight(libraPurse.Weight);

                isPushed = true;
				Level.IsBusy = true;
            }
        }
    }
}

