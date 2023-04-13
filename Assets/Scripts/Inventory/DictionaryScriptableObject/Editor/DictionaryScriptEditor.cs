using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Dictionary_Inventory))]
public class DictionaryScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (((Dictionary_Inventory)target).modifyValues)
        {
            if (GUILayout.Button("Save changes"))
            {
                ((Dictionary_Inventory)target).DeserializeDictionary();
            }

        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        if (GUILayout.Button("Print Dictionary"))
        {
            ((Dictionary_Inventory)target).PrintDictionary();
        }

    }
}
