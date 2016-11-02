using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Gui
{
    public class SplashScreenAnimation : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ExecuteEvents.Execute<ISplashScreenState>(animator.gameObject, null, (x, y) => x.OnAnimationCompleted());
        }
    }
}

