using UnityEngine;
namespace Mindblower.Level5
{
    public class Crate : MonoBehaviour
    {
        [HideInInspector]
        public ContentType Content;
        private Icon icon;

        void Awake()
        {
            icon = GetComponentInChildren<Icon>();
        }

        public ContentType Icon
        {
            get
            {
                return icon.IconType;
            }
            set
            {
                icon.IconType = value;
            }
        }

        public void Init(ContentType content, ContentType icon)
        {
            this.Content = content;
            this.Icon = icon;
        }
    }
}
