using UnityEngine.EventSystems;

namespace Mindblower.Level3
{
    public interface ICoinDragHandler : IEventSystemHandler
    {
        void OnCoinDrag(Coin coin);
        void OnCoinDrop(Coin coin);
    }
}
