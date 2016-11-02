using UnityEngine;

namespace Mindblower.Gui
{
    public abstract class GuiBehaviour : MonoBehaviour
    {
        public static bool IsBusy { get; protected set; }
    }
}
