using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level1
{
    public class Coast : MonoBehaviour, IPointerClickHandler, IActorClickHandler
    {
        public List<ActorCheckPoint> StopPoints;
        public List<ActorCheckPoint> JumpPoints;
        public CheckPoint BoatDock;
        public Boat MainBoat;

        [SerializeField]
        private CoastLocation Location;

        public bool HasGameOver
        {
            get
            {
                Actor[] actors = GetComponentsInChildren<Actor>();
                foreach (var predator in actors)
                {
                    foreach (var victim in actors)
                    {
                        if (predator.CanEat(victim))
                            return true;
                    }
                }

                return false;
            }
        }

        public bool HasAllCharacters
        {
            get
            {
                Actor[] actors = GetComponentsInChildren<Actor>();
                return actors.Length == 3;
            }
        }

        private IEnumerator MoveActor(Actor actor)
        {
            if (MainBoat.CurrentDock == BoatDock && (MainBoat.HasPassenger == false))
            {
                ActorController controller = actor.GetComponent<ActorController>();
                ActorCheckPoint jumpPoint = JumpPoints.Find((x) => x.ActorType == actor.ActorType);
                yield return StartCoroutine(controller.WalkTo(jumpPoint));
                ActorCheckPoint boatPoint = MainBoat.ActorPoints.Find((x) => x.ActorType == actor.ActorType);
                yield return StartCoroutine(controller.JumpTo(boatPoint));
                MainBoat.Passenger = actor;
                actor.transform.parent = MainBoat.transform;

                if (Location == CoastLocation.RightCoast)
                    controller.RotateRight();

                ExecuteEvents.ExecuteHierarchy<ITaskEventsHandler>(gameObject, null, (x, y) => x.OnCharacterMoved());
            }
            Level.IsBusy = false;
        }

        public IEnumerator LaneActor(Actor actor)
        {
            MainBoat.Passenger = null;
            ActorController controller = actor.GetComponent<ActorController>();
            if (Location == CoastLocation.LeftCoast)
                controller.RotateLeft();

            ActorCheckPoint jumpPoint = JumpPoints.Find((x) => x.ActorType == actor.ActorType);
            yield return StartCoroutine(controller.JumpTo(jumpPoint));
            ActorCheckPoint stopPoint = StopPoints.Find((x) => x.ActorType == actor.ActorType);
            yield return StartCoroutine(controller.WalkTo(stopPoint));
            actor.transform.parent = transform;

            if (Location == CoastLocation.LeftCoast)
                controller.RotateRight();
            else
                controller.RotateLeft();

            ExecuteEvents.ExecuteHierarchy<ITaskEventsHandler>(gameObject, null, (x, y) => x.OnCharacterMoved());
        }

        public void OnActorClicked(Actor actor)
        {
            if (!Level.IsBusy)
            {
                Level.IsBusy = true;
                StartCoroutine(MoveActor(actor));
            }
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ExecuteEvents.Execute<ICoastClickHandler>(MainBoat.gameObject, null, (x, y) => x.OnCoastClicked(this));
        }
    }
}
