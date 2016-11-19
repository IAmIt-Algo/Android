using UnityEngine.EventSystems;
using UnityEngine;

namespace Mindblower.Level1
{
    public interface IActorClickHandler : IEventSystemHandler
    {
        void OnActorClicked(Actor actor, Vector3 start, Vector3 end);
    }
}
