using UnityEngine;

namespace Mindblower.Level6
{
    public class ScalesController : MonoBehaviour
    {
        [SerializeField]
        private ShellTray leftTray;
        [SerializeField]
        private ShellTray rightTray;

        public void Weigh()
        {
            int weighSide = 0;
            if (leftTray.GetWeight() > rightTray.GetWeight())
                weighSide = -1;
            if (rightTray.GetWeight() > leftTray.GetWeight())
                weighSide = 1;
            GetComponent<Animator>().SetInteger("WeighSide", weighSide);
        }

        public void FreeScales()
        {
            leftTray.FreeTray();
            rightTray.FreeTray();
            GetComponent<Animator>().SetInteger("WeighSide", 0);
        }
    }
}
