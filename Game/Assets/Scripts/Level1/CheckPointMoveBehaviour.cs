using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level1
{
    [RequireComponent(typeof(MoveBehaviour))]
    public class CheckPointMoveBehaviour : MonoBehaviour
    {
        private MoveBehaviour moveBehaviour;

        void Awake()
        {
            moveBehaviour = GetComponent<MoveBehaviour>();
        }

        public IEnumerator MoveTo(CheckPoint checkPoint, float time, float delay)
        {
            yield return StartCoroutine(moveBehaviour.MoveTo(checkPoint.transform.position, time, delay));
        }
    }
}
