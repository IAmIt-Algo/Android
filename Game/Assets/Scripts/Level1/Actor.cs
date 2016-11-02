using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Mindblower.Level1
{
    [RequireComponent(typeof(ActorController))]
    public class Actor : MonoBehaviour, IPointerClickHandler
    {
        public ActorEnum ActorType;

        public bool CanEat(Actor victim)
        {
            if (ActorType == ActorEnum.Wolf && victim.ActorType == ActorEnum.Goat)
                return true;

            if (ActorType == ActorEnum.Goat && victim.ActorType == ActorEnum.Cabbage)
                return true;

            return false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ExecuteEvents.Execute<IActorClickHandler>(transform.parent.gameObject, null, (x, y) => x.OnActorClicked(this));
        }
    }
}

