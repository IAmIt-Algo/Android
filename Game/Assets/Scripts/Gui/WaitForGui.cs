using UnityEngine;

namespace Mindblower.Gui
{
    public class WaitForGui : CustomYieldInstruction
    {
        public override bool keepWaiting
        {
            get
            {
                return GuiBehaviour.IsBusy;
            }
        }
    }
}
