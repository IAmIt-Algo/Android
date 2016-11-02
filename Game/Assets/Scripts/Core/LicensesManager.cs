using System.Collections.Generic;
using UnityEngine;
namespace Mindblower.Core
{
    public class LicensesManager : MonoBehaviour
    {
        private List<string> licenses;

        public List<string> Licenses
        {
            get
            {
                if (licenses == null)
                    return new List<string>();

                List<string> copy = new List<string>();
                copy.AddRange(licenses);
                return copy;
            }
        }

        public void UpdateLicenses()
        {
            licenses.Clear();
            if (PlayerPrefs.GetInt("license_1_5", 0) == 1)
            {
                licenses.Add("license_1_5");
            }

            if (PlayerPrefs.GetInt("license_6_10", 0) == 1)
            {
                licenses.Add("license_6_10");
            }
        }

        public void BuyLicense(string licenseId)
        {
            PlayerPrefs.SetInt(licenseId, 1);
            UpdateLicenses();
        }

        void Awake()
        {
            licenses = new List<string>();
            PlayerPrefs.SetInt("license_1_5", 1);
            UpdateLicenses();
        }
    }
}
