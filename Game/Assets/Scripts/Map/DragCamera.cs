using UnityEngine;
using System.Collections;

namespace Mindblower.Map
{
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class DragCamera : MonoBehaviour
    {
        private Camera dragCamera;
        private Rigidbody2D physicsCamera;

        void Awake()
        {
            dragCamera = GetComponent<Camera>();
            physicsCamera = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            float savedY = PlayerPrefs.GetFloat("Camera_Position_Y", 0);
            Vector3 newPosition = transform.position;
            newPosition.y = savedY;
            transform.position = newPosition;
        }

        private Vector3 lastTouchPosition;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastTouchPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 currentTouchPosition = Input.mousePosition;
                Vector2 delta = dragCamera.ScreenToWorldPoint(lastTouchPosition - currentTouchPosition) - dragCamera.ScreenToWorldPoint(Vector3.zero);

                physicsCamera.AddForce(delta, ForceMode2D.Impulse);

                lastTouchPosition = currentTouchPosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                lastTouchPosition = Vector3.zero;
            }
        }
    }
}

