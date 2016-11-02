using UnityEngine;
using System.Collections;

namespace Mindblower.Level7
{
    [RequireComponent(typeof(TextMesh))]
    public class LibraWeightWriter : MonoBehaviour
    {
        private TextMesh textMesh;

        void Awake()
        {
            textMesh = GetComponent<TextMesh>();
        }

        void Start()
        {
            GetComponent<MeshRenderer>().sortingOrder = 100;
        }

        public void SetWeight(int weight)
        {
            textMesh.text = weight.ToString();
        }
    }
}

