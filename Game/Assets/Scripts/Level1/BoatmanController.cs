using UnityEngine;

namespace Mindblower.Level1
{
    public class BoatmanController : MonoBehaviour
    {
        private Animator animator;

        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public void StartRaw()
        {
            animator.SetBool("IsMoving", true);
        }

        public void StopRaw()
        {
            animator.SetBool("IsMoving", false);
        }

        public void RotateLeft()
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -1;
            transform.localScale = newScale;
        }

        public void RotateRight()
        {
            Vector3 newScale = transform.localScale;
            newScale.x = 1;
            transform.localScale = newScale;
        }
    }
}
