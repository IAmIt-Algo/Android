using UnityEngine;

namespace Mindblower.Level2
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ContainerIcon : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Show()
        {
            spriteRenderer.enabled = true;
        }

        public void Hide()
        {
            spriteRenderer.enabled = false;
        }
    }
}
