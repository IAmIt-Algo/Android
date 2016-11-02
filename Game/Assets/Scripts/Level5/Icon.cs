using UnityEngine;

namespace Mindblower.Level5
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Icon : MonoBehaviour
    {
        [SerializeField]
        private Sprite SwordSprite;
        [SerializeField]
        private Sprite ShieldSprite;
        [SerializeField]
        private Sprite SwordAndShieldSprite;

        private SpriteRenderer spriteRenderer;
        private ContentType iconType;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public ContentType IconType
        {
            set 
            {
                switch (value)
                {
                    case ContentType.Shield:
                        spriteRenderer.sprite = ShieldSprite;
                        break;
                    case ContentType.Sword:
                        spriteRenderer.sprite = SwordSprite;
                        break;
                    case ContentType.SwordAndShield:
                        spriteRenderer.sprite = SwordAndShieldSprite;
                        break;
                }
                iconType = value;
            }
            get
            {
                return iconType;
            }
        }
    }
}
