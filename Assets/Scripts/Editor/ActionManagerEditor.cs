using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActionManager))]
public class ActionManagerEditor : Editor {
    public override void OnInspectorGUI () {
        base.OnInspectorGUI();
        ActionManager actionManager = (ActionManager)target;

        if (GUILayout.Button("Load Config")) {
            string configDir = Application.dataPath + "/ActionConfigs/";
            string jsonPath = configDir + actionManager.configFile;
            string json = File.ReadAllText(jsonPath);
            JsonUtility.FromJsonOverwrite(json, actionManager);
        }
    }
}
