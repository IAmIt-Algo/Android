using UnityEngine.EventSystems;

namespace Mindblower.Level4
{
    public interface ITurtleDragHandler : IEventSystemHandler
    {
        void OnTurtleDrag(Turtle turtle);
        void OnTurtleDrop(Turtle turtle);
    }
}
