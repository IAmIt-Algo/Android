using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefs : Editor
{
    [MenuItem("Window/Scripts/Clear player preferences")]
    static void ClearExecute()
    {
        PlayerPrefs.DeleteAll();
    }
}
