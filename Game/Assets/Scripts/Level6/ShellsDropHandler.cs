using System.Collections.Generic;
using UnityEngine;

namespace Mindblower.Level6
{
    public class ShellsDropHandler : MonoBehaviour, IShellDragHandler, IShellDropHandler
    {
        private List<ShellTray> trays;

        void Awake()
        {
            trays = new List<ShellTray>();
            trays.AddRange(GetComponentsInChildren<ShellTray>());
        }

        public void OnShellDrag(ShellController shellController)
        {
            
        }

        public void OnShellDrop(ShellController shellController)
        {
            bool shellPushed = false;

            foreach (var tray in trays)
            {
                if (tray.GetComponent<Collider2D>().bounds.Intersects(shellController.GetComponent<Collider2D>().bounds))
                {
                    tray.PushShell(shellController);
                    shellPushed = true;
                }
            }

            if (!shellPushed)
            {
                shellController.PushTable();
            }
        }
    }
}
