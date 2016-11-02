using UnityEngine.EventSystems;

namespace Mindblower.Gui
{
    public interface ISplashScreenState : IEventSystemHandler
    {
        void OnAnimationCompleted();
    }
}
