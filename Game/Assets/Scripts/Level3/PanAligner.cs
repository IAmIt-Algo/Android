using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Mindblower.Level3
{
    [RequireComponent(typeof(CoinTray))]
    public class PanAligner : MonoBehaviour, ICoinTrayHandler
    {
        [SerializeField]
        private float defaultOffset;
        [SerializeField]
        private float weighOffset;
        [SerializeField]
        private float defaultDistance;
        [SerializeField]
        private float weighDistance;

        private CoinTray tray;

        void Awake()
        {
            tray = GetComponent<CoinTray>();
        }

        private bool isDefault = true;

        private void AllignCoins()
        {
            List<Coin> coins = tray.Coins.OrderByDescending((x) => x.GetComponent<CoinController>().Index).ToList();
            for (int i = 0; i < coins.Count; ++i)
            {
                coins[i].GetComponent<CoinController>().RotateHorizontally((i % 2 == 0) ? true : false);
                float xPos = transform.position.x;
                float yPos;
                if (isDefault)
                    yPos = transform.position.y + defaultOffset + i * defaultDistance;
                else
                    yPos = transform.position.y + weighOffset + i * weighDistance;
                coins[i].transform.position = new Vector3(xPos, yPos, coins[i].transform.position.z);
            }
        }

        public void SetWeighing()
        {
            isDefault = false;
            AllignCoins();
        }

        public void SetDefault()
        {
            isDefault = true;
            AllignCoins();
        }

        public void OnCoinAdded()
        {
            AllignCoins();
        }

        public void OnCoinRemoved()
        {
            AllignCoins();
        }
    }
}
