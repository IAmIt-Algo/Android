using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level2
{
    public class Barrel : MonoBehaviour, IContainerHandler
    {
        public void OnContainerDrop(Container container)
        {
            Bucket bucket = container.GetComponent<Bucket>();
            if (bucket.CurrentVolume > 0)
            {
                bucket.CurrentVolume = 0;
                ExecuteEvents.ExecuteHierarchy<IBucketHandler>(gameObject, null, (x, y) => x.OnBucketClean(bucket.Id));
            }
            
        }
    }
}
