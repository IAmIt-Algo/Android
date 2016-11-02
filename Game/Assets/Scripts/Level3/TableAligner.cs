using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mindblower.Level3
{
    public class TableAligner : MonoBehaviour, ICoinTrayHandler
    {
        private void AllignCoins()
        {
            List<Coin> coins = GetComponent<CoinTray>().Coins;
            foreach (var coin in coins)
            {
                coin.GetComponent<CoinController>().RotateVertically();
                coin.transform.position = coin.GetComponent<CoinController>().TablePosition;
            }
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
