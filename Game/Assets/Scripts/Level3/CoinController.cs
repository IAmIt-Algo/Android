using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level3
{
    public class CoinController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [HideInInspector]
        public Vector3 TablePosition;
        [HideInInspector]
        public int Index;

        public void RotateHorizontally(bool left)
        {
            GetComponent<Animator>().SetInteger("Layout", left ? 1 : 2);
        }

        public void RotateVertically()
        {
            GetComponent<Animator>().SetInteger("Layout", 0);
        }

        private Vector3 startDragPosition;
        private Vector3 offset;
        private int startDragSortingOrder;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!Level.IsBusy)
            {
                startDragPosition = transform.position;
                startDragSortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

                offset = startDragPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offset.z = 0;

                GetComponent<SpriteRenderer>().sortingOrder = 2000;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!Level.IsBusy)
            {
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                newPosition.z = transform.position.z;
                transform.position = newPosition;

                ExecuteEvents.ExecuteHierarchy<ICoinDragHandler>(gameObject, null, (x, y) => x.OnCoinDrag(GetComponent<Coin>()));
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!Level.IsBusy)
            {
                ExecuteEvents.ExecuteHierarchy<ICoinDragHandler>(gameObject, null, (x, y) => x.OnCoinDrop(GetComponent<Coin>()));
                GetComponent<SpriteRenderer>().sortingOrder = startDragSortingOrder;
            }
        }
    }
}
