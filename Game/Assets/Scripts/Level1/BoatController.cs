using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level1
{
    [RequireComponent(typeof(CheckPointMoveBehaviour))]
    public class BoatController : MonoBehaviour, ICoastClickHandler
    {
        private MoveBehaviour moveBehaviour;
        private BoatmanController boatmanController;
        public CheckPoint CurrentDock;

        [SerializeField]
        private float rawTime;
        [SerializeField]
        private float rawDelay;

        void Awake()
        {
            moveBehaviour = GetComponent<MoveBehaviour>();
            boatmanController = GetComponentInChildren<BoatmanController>();
        }

        private IEnumerator RawTo(CheckPoint checkPoint)
        {
            if (CurrentDock == checkPoint)
                yield break;

            bool rotateBoatman = false;
            if (checkPoint.transform.position.x < transform.position.x)
            {
                rotateBoatman = true;
                boatmanController.RotateLeft();
            }

            boatmanController.StartRaw();
            yield return StartCoroutine(moveBehaviour.MoveTo(checkPoint.transform.position, rawTime, rawDelay));
            boatmanController.StopRaw();
            
            if (rotateBoatman)
                boatmanController.RotateRight();

            CurrentDock = checkPoint;
            ExecuteEvents.ExecuteHierarchy<ITaskEventsHandler>(gameObject, null, (x, y) => x.OnBoatMoved());
            Level.IsBusy = false;
        }

        public void OnCoastClicked(Coast coast)
        {
            if (!Level.IsBusy)
            {
                Level.IsBusy = true;
                StartCoroutine(RawTo(coast.BoatDock));
            }
                
        }
    }
}
