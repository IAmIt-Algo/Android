using UnityEngine.EventSystems;

namespace Mindblower.Level3
{
    public interface ICoinTrayHandler : IEventSystemHandler
    {
        void OnCoinAdded();
        void OnCoinRemoved();
    }
}
