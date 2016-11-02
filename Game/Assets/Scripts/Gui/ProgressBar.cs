using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Mindblower.Gui
{
    public class ProgressBar : GuiBehaviour
    {
        [SerializeField]
        private GameObject progressView;
        [SerializeField]
        private Text info;

        public float Progress
        {
            set
            {
                progressView.GetComponent<RectTransform>().localScale =
                    new Vector3(value, 1, 1);
            }
        }
        public string Info
        {
            set
            {
                info.text = value;
            }
        }
    }
}
