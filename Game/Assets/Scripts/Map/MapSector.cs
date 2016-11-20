using UnityEngine;
using System.Collections;
using Mindblower.Core;

namespace Mindblower.Map
{
    public class MapSector : MonoBehaviour
    {
        [SerializeField]
        private string licenseId;
        [SerializeField]
        private GameObject dummyContent;
        [SerializeField]
        private GameObject realContent;

        private void UpdateContent()
        {
            if (PlayerPrefs.GetInt(licenseId, 0) == 1)
            {
                if (dummyContent != null)
                {
                    dummyContent.GetComponent<Animator>().enabled = true;
                }
                realContent.SetActive(true);
            }
        }

        public void BuyLicense()
        {
            PlayerPrefs.SetInt(licenseId, 1);
            UpdateContent();
        }

        void Awake()
        {
            if (PlayerPrefs.GetInt(licenseId, 0) == 1)
            {
                if (dummyContent != null)
                {
                    realContent.SetActive(true);
                    Destroy(dummyContent);
                }
            }
            if (!Synchronizer.IsUsed)
            {
                using (var sync = new Synchronizer())
                {
                    sync.Synchronize();
                }
            }
        }
    }
}

