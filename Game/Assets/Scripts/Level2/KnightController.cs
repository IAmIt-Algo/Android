using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level2
{
    [RequireComponent(typeof(Animator))]
    public class KnightController : MonoBehaviour, IBucketHandler
    {
        private const int FillAction = 0;
        private const int CleanAction = 1;
        private const int PourAction = 2;

        private const string ActionBucketParam = "BucketId";
        private const string ActionTypeParam = "ActionType";
        private const string ActionTriggerParam = "ActionTrigger";

        private Animator animator;

        [SerializeField]
        private GameObject bucket3;
        [SerializeField]
        private GameObject bucket5;
        
        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Fill(int bucketId)
        {
            Level.IsBusy = true;

            animator.SetInteger(ActionBucketParam, bucketId);
            animator.SetInteger(ActionTypeParam, FillAction);
            animator.SetTrigger(ActionTriggerParam);
        }

        public void Clean(int bucketId)
        {
            Level.IsBusy = true;

            animator.SetInteger(ActionBucketParam, bucketId);
            animator.SetInteger(ActionTypeParam, CleanAction);
            animator.SetTrigger(ActionTriggerParam);
        }

        public void Pour(int bucketId)
        {
            Level.IsBusy = true;

            animator.SetInteger(ActionBucketParam, bucketId);
            animator.SetInteger(ActionTypeParam, PourAction);
            animator.SetTrigger(ActionTriggerParam);
        }

        public void OnBucket3MovedBack()
        {
            bucket3.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

        public void OnBucket3MovedFront()
        {
            bucket3.GetComponent<SpriteRenderer>().sortingOrder = 10;
        }

        public void OnBucket5MovedBack()
        {
            bucket5.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

        public void OnBucket5MovedFront()
        {
            bucket5.GetComponent<SpriteRenderer>().sortingOrder = 7;
        }

        public void OnBucketPour(int bucketId)
        {
            Pour(bucketId);
            ExecuteEvents.ExecuteHierarchy<ITaskEventsHandler>(gameObject, null, (x, y) => x.OnWaterPoured());
        }

        public void OnBucketClean(int bucketId)
        {
            Clean(bucketId);
            ExecuteEvents.ExecuteHierarchy<ITaskEventsHandler>(gameObject, null, (x, y) => x.OnWaterPoured());
        }

        public void OnBucketFill(int bucketId)
        {
            Fill(bucketId);
            ExecuteEvents.ExecuteHierarchy<ITaskEventsHandler>(gameObject, null, (x, y) => x.OnWaterPoured());
        }
    }
}
