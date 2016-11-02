using System.Collections;
using UnityEngine;

namespace Mindblower.Level1
{
    public class MoveBehaviour : MonoBehaviour
    {
        public IEnumerator MoveTo(Vector2 toPosition, float time, float delay = 0)
        {
            yield return new WaitForSeconds(delay);

            Vector3 startPosition = transform.position;
            Vector3 toPositionExt = toPosition; toPositionExt.z = startPosition.z;
            float startTime = Time.time;

            bool isDone = time <= 0 ? true : false;
            while (!isDone)
            {
                float deltaTime = Time.time - startTime;
                if (deltaTime >= time)
                {
                    transform.position = toPosition;
                    isDone = true;
                }
                else
                {
                    Vector3 newPosition = Vector3.Lerp(startPosition, toPosition, deltaTime / time);
                    transform.position = newPosition;
                    yield return null;
                }
            }
        }

        public IEnumerator MoveTo(float toX, float toY, float time, float delay = 0)
        {
            yield return StartCoroutine(MoveTo(new Vector2(toX, toY), time));
        }
    }
}
