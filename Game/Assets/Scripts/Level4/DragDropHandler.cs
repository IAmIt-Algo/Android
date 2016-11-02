using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level4
{
    public class DragDropHandler : MonoBehaviour, ITurtleDragHandler
    {
        private List<Tower> towerList;

        void Awake()
        {
            towerList = new List<Tower>();
            towerList.AddRange(GetComponentsInChildren<Tower>());
        }

        public void OnTurtleDrag(Turtle turtle)
        {
            towerList.ForEach(x => x.TurnLight(false));

            var intersectedTower = towerList.FindAll((x) => (x.Intersects(turtle) && !x.Contains(turtle)));
            if (intersectedTower.Count > 0)
            {
                var firstIntersected = intersectedTower.Aggregate((x, y) => x.GetComponent<SpriteRenderer>().sortingOrder > y.GetComponent<SpriteRenderer>().sortingOrder ? x : y);
                firstIntersected.TurnLight(true);
            }
        }

        public void OnTurtleDrop(Turtle turtle)
        {
            towerList.ForEach(x => x.TurnLight(false));

            var intersectedTower = towerList.FindAll((x) => (x.Intersects(turtle) && !x.Contains(turtle)));
            if (intersectedTower.Count > 0)
            {
                var firstIntersected = intersectedTower.Aggregate((x, y) => x.GetComponent<SpriteRenderer>().sortingOrder > y.GetComponent<SpriteRenderer>().sortingOrder ? x : y);
                firstIntersected.TryPushTurtle(turtle);
            }
            else
            {
                ExecuteEvents.Execute<ITowerPushHandler>(turtle.gameObject, null, (x, y) => x.OnTurtlePushRefuse());
            }
        }
    }
}
