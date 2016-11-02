using UnityEngine.EventSystems;

namespace Mindblower.Level1
{
    public interface ICoastClickHandler : IEventSystemHandler
    {
        void OnCoastClicked(Coast coast);
    }
}
