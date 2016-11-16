using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level4
{
    [RequireComponent(typeof(Turtle))]
    public class TurtleController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, ITowerPushHandler
    {
        private Turtle turtle;

        void Awake()
        {
            turtle = GetComponent<Turtle>();
            IsDragable = false;
        }

        [HideInInspector]
        public bool IsDragable;

        private Vector3 offset;
        private Vector3 startDragPosition;
        private int startDratSortingOrder;

        public void OnBeginDrag(PointerEventData eventData)
        {
            startDratSortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
            if (IsDragable)
            {
                startDragPosition = transform.position;
                offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - startDragPosition;
                offset.z = 0;

                
                GetComponent<SpriteRenderer>().sortingOrder = 2000;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsDragable)
            {
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newPosition.z = transform.position.z;
                transform.position = newPosition - offset;

                ExecuteEvents.ExecuteHierarchy<ITurtleDragHandler>(gameObject, null, (x, y) => x.OnTurtleDrag(turtle));
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GetComponent<SpriteRenderer>().sortingOrder = startDratSortingOrder;
            if (IsDragable)
                ExecuteEvents.ExecuteHierarchy<ITurtleDragHandler>(gameObject, null, (x, y) => x.OnTurtleDrop(turtle));
        }

        public void OnTurtlePushAccept()
        {
        }

        public void OnTurtlePushRefuse()
        {
            turtle.transform.position = startDragPosition;
        }
    }
}
