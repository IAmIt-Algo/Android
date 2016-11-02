using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindblower.Level5
{
    public class DragDropHandler : MonoBehaviour, IStickerDropHandler
    {
        private List<CrateController> crates;

        void Awake()
        {
            crates = new List<CrateController>();
            crates.AddRange(GetComponentsInChildren<CrateController>());
        }

        public void OnStickerDrop(StickerController sticker)
        {
            foreach (var crate in crates)
            {
                if (crate.GetComponent<Collider2D>().bounds.Intersects(sticker.GetComponent<Collider2D>().bounds))
                {
                    crate.GetComponent<Crate>().Icon = sticker.StickerType;
                    break;
                }
            }
        }
    }
}
