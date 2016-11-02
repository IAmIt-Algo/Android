using UnityEngine;
using System.Collections;

namespace Mindblower.Map
{
    public class DummyContent : MonoBehaviour
    {
        public void OnDestroyComplete()
        {
            Destroy(gameObject);
        }
    }
}

