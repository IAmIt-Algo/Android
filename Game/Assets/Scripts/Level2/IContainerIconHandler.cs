using UnityEngine.EventSystems;

namespace Mindblower.Level2
{
    public interface IContainerIconHandler : IEventSystemHandler
    {
        void OnContainerIconDrag(ContainerIcon icon);
        void OnContainerIconDrop(ContainerIcon icon);
    }
}
