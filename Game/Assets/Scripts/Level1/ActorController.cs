using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level1
{
    [RequireComponent(typeof(CheckPointMoveBehaviour))]
    public class ActorController : MonoBehaviour
    {
        [SerializeField]
        private float walkTime;
        [SerializeField]
        private float jumpTime;
        [SerializeField]
        private float jumpDelay;

        private CheckPointMoveBehaviour checkPointMoveBehaviour;
        private Animator animator;

        private float baseRightXDirection;

        void Awake()
        {
            checkPointMoveBehaviour = GetComponent<CheckPointMoveBehaviour>();
            animator = GetComponentInChildren<Animator>();

            baseRightXDirection = transform.localScale.x;
        }

        public IEnumerator WalkTo(CheckPoint checkPoint)
        {
            animator.SetTrigger("Walk");
            yield return StartCoroutine(checkPointMoveBehaviour.MoveTo(checkPoint, walkTime, 0));
            animator.SetTrigger("Stop");
        }

        public IEnumerator JumpTo(CheckPoint checkPoint)
        {
            animator.SetTrigger("Jump");
            yield return StartCoroutine(checkPointMoveBehaviour.MoveTo(checkPoint, jumpTime, jumpDelay));
            animator.SetTrigger("Stop");
        }

        public void RotateLeft()
        {
            Vector3 newScale = transform.localScale;
            newScale.x = baseRightXDirection * (-1);
            transform.localScale = newScale;
        }

        public void RotateRight()
        {
            Vector3 newScale = transform.localScale;
            newScale.x = baseRightXDirection;
            transform.localScale = newScale;
        }
    }
}
