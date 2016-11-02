using UnityEngine.EventSystems;

namespace Mindblower.Level6
{
    public interface IShellDropHandler : IEventSystemHandler
    {
        void OnShellDrop(ShellController shellController);
    }
}
