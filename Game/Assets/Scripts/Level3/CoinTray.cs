using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level3
{
    public class CoinTray : MonoBehaviour
    {
        private List<Coin> coins;

        public List<Coin> Coins
        {
            get
            {
                List<Coin> copy = new List<Coin>();
                copy.AddRange(coins);
                return copy;
            }
        }

        void Awake()
        {
            coins = new List<Coin>();
        }

        public void PushCoin(Coin coin)
        {
            coins.Add(coin);
            ExecuteEvents.Execute<ICoinTrayHandler>(gameObject, null, (x, y) => x.OnCoinAdded());
        }

        public void RemoveCoin(Coin coin)
        {
            coins.Remove(coin);
            ExecuteEvents.Execute<ICoinTrayHandler>(gameObject, null, (x, y) => x.OnCoinRemoved());
        }

        public bool Intersects(Coin coin)
        {
            if (name == "LeftPan" || name == "RightPan")
            {
                var spaceX = GetComponent<BoxCollider2D>().bounds.center.x;
                var spaceY = GetComponent<BoxCollider2D>().bounds.center.y;
                var coinX = coin.GetComponent<Collider2D>().bounds.center.x;
                var coinY = coin.GetComponent<Collider2D>().bounds.center.y;
                var halfWidth = GetComponent<BoxCollider2D>().bounds.size.x / 2;
                var halfHeight = GetComponent<BoxCollider2D>().bounds.size.y / 2;
                if (Math.Abs(spaceX - coinX) <= halfWidth && Math.Abs(spaceY - coinY) <= halfHeight)
                {
                    return true;
                }
            }
            if (GetComponent<SpriteRenderer>().bounds.Intersects(coin.GetComponent<Collider2D>().bounds))
                return true;

            foreach (var trayCoin in coins)
            {
                if (trayCoin.GetComponent<Collider2D>().bounds.Intersects(coin.GetComponent<Collider2D>().bounds))
                    return true;
            }

            return false;
        }

        public bool Contains(Coin coin)
        {
            return coin.Tray == this;
        }

        public int GetWeight()
        {
            int result = 0;
            foreach (var coin in coins)
            {
                result += coin.Weight;
            }
            return result;
        }
    }
}
