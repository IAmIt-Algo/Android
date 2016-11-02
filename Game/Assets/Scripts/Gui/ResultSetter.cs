using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Mindblower.Gui
{
    public class ResultSetter : GuiBehaviour
    {
        [SerializeField]
        private List<GameObject> stars;

        private const int MaxValue = 3;
        private const int MinValue = 0;

        public int Result
        {
            set
            {
                int result = Mathf.Min(MaxValue, Mathf.Max(MinValue, value));
                stars.ForEach((x) => x.GetComponent<Image>().enabled = false);

                for (int i = 0; i < result; ++i)
                    stars[i].GetComponent<Image>().enabled = true;
            }
        }
    }
}
