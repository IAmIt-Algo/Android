using UnityEngine.EventSystems;

namespace Mindblower.Gui
{
    public interface IPopupWindowState : IEventSystemHandler
    {
        void OnAnimationComplete();
    }
}
