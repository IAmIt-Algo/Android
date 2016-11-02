using UnityEngine.EventSystems;

namespace Mindblower.Level5
{
    public interface ITaskEventsHandler : IEventSystemHandler
    {
        void OnCreateClick();
    }
}
