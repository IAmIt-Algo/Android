using UnityEngine;
using UnityEngine.UI;

namespace Mindblower.Level3
{
    [RequireComponent(typeof(CoinTray))]
    public class Runner : MonoBehaviour
    {
        [SerializeField]
        private Coin coinPrefab;
        [SerializeField]
        private int coinsNumber;

        [SerializeField]
        private GameObject coinsStartPoint;
        [SerializeField]
        private GameObject coinsEndPoint;

        void Start()
        {
            float offset = (coinsEndPoint.transform.position.x - coinsStartPoint.transform.position.x) / (coinsNumber - 1);

            int badCoinIndex = Random.Range(0, coinsNumber);

            for (int i = 0; i < coinsNumber; ++i)
            {
                Coin coin = Instantiate(coinPrefab);
                Vector3 tablePosition = coinsStartPoint.transform.position;
                tablePosition.x += offset * i;
                coin.GetComponent<CoinController>().TablePosition = tablePosition;
                coin.Tray = GetComponent<CoinTray>();
                coin.GetComponent<CoinController>().Index = i + 1;
                coin.GetComponentInChildren<Text>().text = (i + 1).ToString();

                if (i == badCoinIndex)
                    coin.Weight = 11;
                else
                    coin.Weight = 10;
            }

            Debug.Log(badCoinIndex);
        }
    }
}
