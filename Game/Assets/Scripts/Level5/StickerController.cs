using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level5
{
    public class StickerController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public ContentType StickerType;

        private Vector3 startDragPosition;
        private Vector3 offset;

        public void OnBeginDrag(PointerEventData eventData)
        {
            startDragPosition = transform.position;
            offset = startDragPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset.z = 0;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ExecuteEvents.ExecuteHierarchy<IStickerDropHandler>(gameObject, null, (x, y) => x.OnStickerDrop(this));
            transform.position = startDragPosition;
        }
    }
}
