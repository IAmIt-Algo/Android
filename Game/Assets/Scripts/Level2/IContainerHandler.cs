using UnityEngine.EventSystems;

namespace Mindblower.Level2
{
    public interface IContainerHandler : IEventSystemHandler
    {
        void OnContainerDrop(Container container);
    }
}
