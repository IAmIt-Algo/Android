using UnityEngine.EventSystems;

namespace Mindblower.Level2
{
    public interface IBucketHandler : IEventSystemHandler
    {
        void OnBucketPour(int bucketId);
        void OnBucketClean(int bucketId);
        void OnBucketFill(int bucketId);
    }
}
