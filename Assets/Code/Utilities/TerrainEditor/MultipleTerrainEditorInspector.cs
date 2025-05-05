using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;



[CustomEditor(typeof(MultipleTerrainEditor))]
public class MultipleTerrainEditorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MultipleTerrainEditor editor = (MultipleTerrainEditor)target;

        if(GUILayout.Button("Assign active terrains")) 
        {
            editor.AssignActiveTerrains();
        }

        if (GUILayout.Button("ChangeSettings"))
        {
            editor.ChangeSettings();
        }
    }
}

#endif
