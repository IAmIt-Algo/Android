using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Core
{
    public interface ILevelEventsHandler : IEventSystemHandler
    {
        void OnLevelLoaded(TextAsset rules);
        void OnLevelComplete(int result);
        void OnLevelGameOver();
        void OnLevelCanceled(LevelInfo info);
    }
}
