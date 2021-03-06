﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level1
{
    [RequireComponent(typeof(BoatController))]
    public class Boat : MonoBehaviour, IActorClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public List<ActorCheckPoint> ActorPoints;
        public Actor Passenger;

        private BoatController boatController;

        public CheckPoint CurrentDock
        {
            get
            {
                return boatController.CurrentDock;
            }
        }

        void Awake()
        {
            boatController = GetComponent<BoatController>();
        }

        public bool HasPassenger
        {
            get { return Passenger != null; }
        }

        private IEnumerator LaneCoast(Coast coast, Actor actor, Vector3 start, Vector3 end)
        {
            yield return StartCoroutine(coast.LaneActor(actor, start, end));
            Level.IsBusy = false;
        }

        public void OnActorClicked(Actor actor, Vector3 start, Vector3 end)
        {
            if (!Level.IsBusy)
            {
                Coast coast = CurrentDock.transform.parent.GetComponent<Coast>();
                StartCoroutine(LaneCoast(coast, actor, start, end));
                Level.IsBusy = true;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ExecuteEvents.Execute<IBoatSwipeHandler>(transform.parent.gameObject, null, (x, y) => x.OnBoatSwiped(new Vector3
            {
                x = eventData.pressPosition.x,
                y = eventData.pressPosition.y,
                z = 0
            }, new Vector3
            {
                x = eventData.position.x,
                y = eventData.position.y,
                z = 0
            }, null));
        }
    }
}
