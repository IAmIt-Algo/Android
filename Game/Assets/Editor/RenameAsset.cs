using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections.Generic;

public class RenameAsset : EditorWindow {
    public static RenameAsset win;

    [MenuItem("Window/Scripts/Rename Asset... &r")]
    static void Init() {
        win = ScriptableObject.CreateInstance(typeof(RenameAsset)) as RenameAsset;
        win.OnSelectionChange();
        win.ShowUtility();
    }

    string assetName = string.Empty;
    UnityEngine.Object asset = null;

    void OnGUI() {
        assetName = EditorGUILayout.TextField("New name", assetName);

        GUI.backgroundColor = Color.white;
        if (GUILayout.Button("OK")) {
            Execute();
        }
        GUI.backgroundColor = Color.clear;

        var controlId = GUIUtility.GetControlID(FocusType.Passive);
        var controlEvent = Event.current.GetTypeForControl(controlId);

        if (controlEvent == EventType.KeyDown) {
            if (Event.current.keyCode == KeyCode.Return) {
                Execute();
                Close();
            }
        }
    }

    void OnSelectionChange() {
        var assets = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
        if (assets.Length != 1) {
            Debug.Log(string.Format("Invalid number of selected assets: {0}", assets.Length));
        } else if (assets[0] != asset) {
            asset = assets[0];
            assetName = asset.name;
        }
    }

    void Execute() {
        if (asset == null) {
            Debug.Log("No asset selected");
        } else if (string.IsNullOrEmpty(assetName)) {
            Debug.Log("Asset name cannot be empty");
        } else {
            var path = AssetDatabase.GetAssetPath(asset);
            string error = AssetDatabase.RenameAsset(path, assetName);
            if (!string.IsNullOrEmpty(error)) {
                Debug.Log(string.Format("Failed to rename '{0}' to '{1}': {2}", path, assetName, error));
            }
        }
    }
}
