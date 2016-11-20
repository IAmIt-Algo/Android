using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace Mindblower.Level1
{
    [RequireComponent(typeof(ActorController))]
    public class Actor : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
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

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ExecuteEvents.Execute<IActorClickHandler>(transform.parent.gameObject, null, (x, y) => x.OnActorClicked(this, new Vector3 {
                x = eventData.pressPosition.x,
                y = eventData.pressPosition.y,
                z = 0}, new Vector3 {
                x = eventData.position.x,
                y = eventData.position.y,
                z = 0}));
        }
    }
}

