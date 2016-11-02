using UnityEngine.EventSystems;

namespace Mindblower.Level5
{
    public interface IStoneClickHandler : IEventSystemHandler
    {
        void OnStoneClicked();
    }
}
