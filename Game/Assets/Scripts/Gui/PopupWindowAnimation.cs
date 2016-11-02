using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Gui
{
    public class PopupWindowAnimation : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ExecuteEvents.Execute<IPopupWindowState>(animator.gameObject, null, (x, y) => x.OnAnimationComplete());
        }
    }
}
