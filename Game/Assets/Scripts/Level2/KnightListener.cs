using UnityEngine;
using System.Collections;

namespace Mindblower.Level2
{
    public class KnightListener : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.idle knight"))
            {
                Level.IsBusy = false;
            }
        }
    }
}

