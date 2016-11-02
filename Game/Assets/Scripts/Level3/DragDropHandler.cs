using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mindblower.Level3
{
    public class DragDropHandler : MonoBehaviour, ICoinDragHandler
    {
        private List<CoinTray> trays;
        private Pot pot;

        void Awake()
        {
            trays = new List<CoinTray>();
            trays.AddRange(GetComponentsInChildren<CoinTray>());

            pot = GetComponentInChildren<Pot>();
        }

        public void OnCoinDrag(Coin coin)
        {
            var intersectedTrays = trays.FindAll((x) => (x.Intersects(coin) && !x.Contains(coin)));
            if (intersectedTrays.Count > 0)
            {
                var firstIntersected = intersectedTrays.Aggregate((x, y) => x.GetComponent<SpriteRenderer>().sortingOrder > y.GetComponent<SpriteRenderer>().sortingOrder ? x : y);
            }
        }

        public void OnCoinDrop(Coin coin)
        {
            if (pot.Intersects(coin))
            {
                pot.CheckCoin(coin);
            }
            else
            {
                var intersectedTrays = trays.FindAll((x) => (x.Intersects(coin) && !x.Contains(coin)));
                if (intersectedTrays.Count > 0)
                {
                    var firstIntersected = intersectedTrays.Aggregate((x, y) => x.GetComponent<SpriteRenderer>().sortingOrder > y.GetComponent<SpriteRenderer>().sortingOrder ? x : y);
                    coin.Tray = firstIntersected;
                }
                else
                {
                    coin.Tray = coin.Tray;
                }
            }
        }
    }
}

