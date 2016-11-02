using UnityEngine;
using UnityEngine.EventSystems;
namespace Mindblower.Level5
{
    public class StoneController : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            ExecuteEvents.ExecuteHierarchy<IStoneClickHandler>(gameObject, null, (x, y) => x.OnStoneClicked());
        }
    }
}
