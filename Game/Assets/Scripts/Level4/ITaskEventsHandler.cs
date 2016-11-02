using UnityEngine.EventSystems;

namespace Mindblower.Level4
{
    public interface ITaskEventsHandler : IEventSystemHandler
    {
        void OnTurtlePush();
        void OnTurtlePushLastTower(Tower tower);
    }
}
