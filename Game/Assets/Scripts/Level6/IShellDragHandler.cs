using UnityEngine.EventSystems;

namespace Mindblower.Level6
{
    public interface IShellDragHandler : IEventSystemHandler
    {
        void OnShellDrag(ShellController shellController);
    }
}
