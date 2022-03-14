using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AutoContainer))]
public class AutoContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AutoContainer ac = target as AutoContainer;
        List<string> widgets = ac.WIDGET_NAMES;

        if (widgets == null || widgets.Count == 0)
        {
            EditorGUILayout.HelpBox("No widgets", MessageType.Warning);
        }
        else
        {
            foreach (string widget in widgets)
                EditorGUILayout.LabelField(widget);
        }

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Allow VirtualWidget");
        ac.ALLOW_VIRTUAL_WIDGET = EditorGUILayout.Toggle(ac.ALLOW_VIRTUAL_WIDGET);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if(GUILayout.Button("ConnectWidgets"))
        {
            AutoUiTool.ConnectWidgets(ac.gameObject);
        }
    }
}
