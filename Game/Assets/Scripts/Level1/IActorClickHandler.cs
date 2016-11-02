using UnityEngine.EventSystems;

namespace Mindblower.Level1
{
    public interface IActorClickHandler : IEventSystemHandler
    {
        void OnActorClicked(Actor actor);
    }
}
