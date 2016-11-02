using UnityEngine;
using System.Collections;

namespace Mindblower.Level7
{
    public class LibraPurse : MonoBehaviour
    {
        private int weight;

        void Start()
        {
            weight = 0;
        }

        public void PushCoin(int weight)
        {
            this.weight += weight;
        }

        public void RemoveCoins()
        {
            weight = 0;
        }

        public int Weight { get { return weight; } }
    }
}

