using UnityEngine.EventSystems;

namespace Mindblower.Level3
{
    public interface ITaskEventsHandler : IEventSystemHandler
    {
        void OnCoinCheck(Coin coin);
        void OnWeightCheck();
    }
}
