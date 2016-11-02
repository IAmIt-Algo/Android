using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace Mindblower.Level6
{
    public class ShellController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private GameObject table;
        private Vector3 tablePosition;
        private int startSortingOrder;
        private Vector3 offset;

        void Awake()
        {
            table = transform.parent.gameObject;
            tablePosition = transform.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            startSortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
            tablePosition = transform.position;

            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset.z = 0;

            GetComponent<SpriteRenderer>().sortingOrder = 1000;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = transform.position.z;
            transform.position = newPosition;

            ExecuteEvents.ExecuteHierarchy<IShellDragHandler>(gameObject, null, (x, y) => x.OnShellDrag(this));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GetComponent<SpriteRenderer>().sortingOrder = startSortingOrder;
            ExecuteEvents.ExecuteHierarchy<IShellDropHandler>(gameObject, null, (x, y) => x.OnShellDrop(this));
        }

        public void PushTable()
        {
            transform.position = tablePosition;
            transform.parent = table.transform;

            transform.localScale = new Vector3(0.7f, 0.7f, 1);
            transform.rotation = Quaternion.identity;
        }
    }
}

