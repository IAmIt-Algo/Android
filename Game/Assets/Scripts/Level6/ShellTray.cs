using UnityEngine;
using System.Collections;

namespace Mindblower.Level6
{
    public class ShellTray : MonoBehaviour
    {
        public void PushShell(ShellController shellController)
        {
            shellController.transform.parent = transform;
            Vector3 newShellPosition = transform.position;
            newShellPosition.z = shellController.transform.position.z;
            newShellPosition.y -= GetComponent<SpriteRenderer>().bounds.size.y;
            newShellPosition.y += shellController.GetComponent<SpriteRenderer>().bounds.size.y / 2;

            int pushNumber = GetComponentsInChildren<ShellController>().Length;
            float horizontalOffset = 0;

            switch (pushNumber)
            {
                case 2:
                    horizontalOffset = -shellController.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                    break;
                case 3:
                    horizontalOffset = shellController.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                    break;
                case 4:
                    horizontalOffset = shellController.GetComponent<SpriteRenderer>().bounds.size.x / 4;
                    break;
            }
            newShellPosition.x += horizontalOffset;

            shellController.transform.position = newShellPosition;
        }

        public float GetWeight()
        {
            float result = 0;
            foreach (var shell in GetComponentsInChildren<Shell>())
            {
                result += shell.Weight;
            }
            return result;
        }

        public void FreeTray()
        {
            foreach (var shell in GetComponentsInChildren<ShellController>())
            {
                shell.PushTable();
            }
        }
    }
}

