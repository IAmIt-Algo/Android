using UnityEngine;

namespace Mindblower.Level2
{
    public class ContainerController : MonoBehaviour
    {
        private ContainerHighlight highlight;

        void Awake()
        {
            highlight = GetComponentInChildren<ContainerHighlight>();
        }

        public void TurnLight(bool onoff)
        {
            highlight.TurnLight(onoff);
        }
    }
}
