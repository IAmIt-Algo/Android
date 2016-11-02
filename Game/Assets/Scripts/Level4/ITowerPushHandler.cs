using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level4
{
    public interface ITowerPushHandler : IEventSystemHandler
    {
        void OnTurtlePushAccept();
        void OnTurtlePushRefuse();
    }
}
