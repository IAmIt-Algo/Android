using UnityEngine.EventSystems;

namespace Mindblower.Level5
{
    public interface IStickerDropHandler : IEventSystemHandler
    {
        void OnStickerDrop(StickerController sticker);
    }
}
