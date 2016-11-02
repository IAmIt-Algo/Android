using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace Mindblower.Level6
{
    public class WeighButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private ScalesController scalesController;
        [SerializeField]
        private SpriteRenderer buttonSprite;

        private bool isPushed;

        void Start()
        {
            isPushed = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isPushed)
            {
                scalesController.FreeScales();
                buttonSprite.enabled = true;
                isPushed = false;
            }
            else
            {
                scalesController.Weigh();
                buttonSprite.enabled = false;
                isPushed = true;
            }
        }
    }
}

