using UnityEngine;

namespace Mindblower.Level2
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ContainerHighlight : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void TurnLight(bool onoff)
        {
            spriteRenderer.enabled = onoff;
        }
    }
}