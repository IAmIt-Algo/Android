using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level3
{
    public class Pot : MonoBehaviour
    {
        [SerializeField]
        private GameObject taskEventsHandler;

        public void CheckCoin(Coin coin)
        {
            ExecuteEvents.Execute<ITaskEventsHandler>(gameObject, null, (x, y) => x.OnCoinCheck(coin));
        }

        public bool Intersects(Coin coin)
        {
            return GetComponent<Collider2D>().bounds.Intersects(coin.GetComponent<Collider2D>().bounds);
        }
    }
}

