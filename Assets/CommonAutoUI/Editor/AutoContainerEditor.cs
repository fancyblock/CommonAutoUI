using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AutoContainer))]
public class AutoContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AutoContainer ac = target as AutoContainer;

        foreach (string widget in ac.WIDGET_NAMES)
            EditorGUILayout.LabelField(widget);

        EditorGUILayout.Space();

        if(GUILayout.Button("ConnectWidgets"))
        {
            AutoUiTool.ConnectWidgets(ac.gameObject);
        }
    }
}
