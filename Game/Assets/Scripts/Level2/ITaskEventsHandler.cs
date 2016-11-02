using UnityEngine.EventSystems;

namespace Mindblower.Level2
{
    public interface ITaskEventsHandler : IEventSystemHandler
    {
        void OnWaterPoured();
    }
}
