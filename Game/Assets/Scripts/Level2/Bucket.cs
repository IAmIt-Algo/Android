using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level2
{
    public class Bucket : MonoBehaviour, IContainerHandler
    {
        public int Id;

        [SerializeField]
        private int capacity;
        public int Capacity { get { return capacity; } }

        private int currentVolume;
        public int CurrentVolume
        {
            get
            {
                return currentVolume;
            }
            set
            {
                currentVolume = value;
                for (int i = 0; i < Capacity; ++i)
                    WaterLevels[i].GetComponent<SpriteRenderer>().enabled = false;

                for (int i = 0; i < currentVolume; ++i)
                    WaterLevels[i].GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        public int RemainVolume { get { return Capacity - CurrentVolume; } }

        public List<GameObject> WaterLevels;

        public void OnContainerDrop(Container container)
        {
            Bucket bucket = container.GetComponent<Bucket>();
            if (bucket != null)
            {
                if (this.RemainVolume > 0 && bucket.CurrentVolume> 0)
                {
                    int flowVolume = Mathf.Min(this.RemainVolume, bucket.CurrentVolume);
                    this.CurrentVolume += flowVolume;
                    bucket.CurrentVolume -= flowVolume;
                    ExecuteEvents.ExecuteHierarchy<IBucketHandler>(gameObject, null, (x, y) => x.OnBucketPour(bucket.Id));
                }
            }
            else
            {
                if (this.RemainVolume > 0)
                {
                    this.CurrentVolume = this.Capacity;
                    ExecuteEvents.ExecuteHierarchy<IBucketHandler>(gameObject, null, (x, y) => x.OnBucketFill(Id));
                }
                
            }
        }
    }
}
