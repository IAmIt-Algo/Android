using UnityEngine.EventSystems;

namespace Mindblower.Level1
{
    public interface ITaskEventsHandler : IEventSystemHandler
    {
        void OnCharacterMoved();
        void OnBoatMoved();
    }
}
