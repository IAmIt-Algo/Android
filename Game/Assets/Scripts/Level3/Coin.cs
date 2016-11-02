using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level3
{
    public class Coin : MonoBehaviour
    {
        public int Weight;

        private CoinTray currentTray;

        public CoinTray Tray
        {
            get
            {
                return currentTray;
            }
            set
            {
                if (currentTray != null)
                    currentTray.RemoveCoin(this);                   
                currentTray = value;
                transform.parent = currentTray.transform;
                currentTray.PushCoin(this);
            }
        }
    }
}
