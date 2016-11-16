using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Core
{
    public interface ILevelEventsHandler : IEventSystemHandler
    {
        void OnLevelLoaded(TextAsset rules);
        void OnLevelComplete(LevelInfo info);
        void OnLevelGameOver(LevelInfo info);
        void OnLevelCanceled(LevelInfo info);
    }
}
