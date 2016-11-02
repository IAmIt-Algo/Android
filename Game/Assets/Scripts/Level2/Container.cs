using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level2
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(ContainerController))]
    public class Container : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private ContainerIcon icon;
        private Collider2D containerCollider;

        void Awake()
        {
            icon = GetComponentInChildren<ContainerIcon>();
            containerCollider = GetComponent<Collider2D>();
        }

        private Vector3 startDragPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!Level.IsBusy)
            {
                icon.Show();
                startDragPosition = icon.transform.position;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!Level.IsBusy)
            {
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newPosition.z = icon.transform.position.z;
                icon.transform.position = newPosition;

                ExecuteEvents.ExecuteHierarchy<IContainerIconHandler>(gameObject, null, (x, y) => x.OnContainerIconDrag(icon));
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!Level.IsBusy)
            {
                ExecuteEvents.ExecuteHierarchy<IContainerIconHandler>(gameObject, null, (x, y) => x.OnContainerIconDrop(icon));

                icon.transform.position = startDragPosition;
                icon.Hide();
            }
        }

        public bool Intersects(ContainerIcon icon)
        {
            return icon.GetComponent<SpriteRenderer>().bounds.Intersects(containerCollider.bounds);
        }
    }
}
