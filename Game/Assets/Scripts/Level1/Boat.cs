using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mindblower.Level1
{
    [RequireComponent(typeof(BoatController))]
    public class Boat : MonoBehaviour, IActorClickHandler
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

        private IEnumerator LaneCoast(Coast coast, Actor actor)
        {
            yield return StartCoroutine(coast.LaneActor(actor));
            Level.IsBusy = false;
        }

        public void OnActorClicked(Actor actor)
        {
            if (!Level.IsBusy)
            {
                Coast coast = CurrentDock.transform.parent.GetComponent<Coast>();
                StartCoroutine(LaneCoast(coast, actor));
                Level.IsBusy = true;
            }
        }
    }
}
