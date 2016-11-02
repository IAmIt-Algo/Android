using UnityEngine;
using System.Collections;

namespace Mindblower.Menu
{
    public class DummyContent : MonoBehaviour
    {
        public void OnDestroyComplete()
        {
            Destroy(gameObject);
        }
    }
}

