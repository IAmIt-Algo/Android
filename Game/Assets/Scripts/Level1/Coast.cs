using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level1
{
    public class Coast : MonoBehaviour, IActorClickHandler, IBoatSwipeHandler
    {
        public List<ActorCheckPoint> StopPoints;
        public List<ActorCheckPoint> JumpPoints;
        public CheckPoint BoatDock;
        public Boat MainBoat;
        public Coast LeftCoast;
        public Coast RightCoast;

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

        public IEnumerator LaneActor(Actor actor, Vector3 start, Vector3 end)
        {
            if ((end.x - start.x > 0 && Location == CoastLocation.RightCoast) || (end.x - start.x < 0 && Location == CoastLocation.LeftCoast))
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
        }

        public void OnActorClicked(Actor actor, Vector3 start, Vector3 end)
        {
            var renderer = MainBoat.GetComponentInChildren<SpriteRenderer>();
            var endVector = Camera.main.ScreenToWorldPoint(end);
            endVector.z += 10;
            if (renderer.bounds.Contains(endVector))
            {
                if (!Level.IsBusy)
                {
                    Level.IsBusy = true;
                    StartCoroutine(MoveActor(actor));
                }
            }
        }

        public void OnBoatSwiped(Vector3 start, Vector3 end, Coast coast)
        {
            if ((end.x - start.x > 0 && Location == CoastLocation.LeftCoast) || (end.x - start.x < 0 && Location == CoastLocation.RightCoast))
            {
                if (this.name == LeftCoast.name)
                    coast = RightCoast;
                else
                    coast = LeftCoast;
                MainBoat.transform.parent = coast.transform;
                ExecuteEvents.Execute<IBoatSwipeHandler>(MainBoat.gameObject, null, (x, y) => x.OnBoatSwiped(start, end, coast));
            }
        }
    }
}
