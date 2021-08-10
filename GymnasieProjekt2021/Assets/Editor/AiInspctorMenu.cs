using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AIManeger))]

public class AiInspctorMenu : Editor
{
    private int selected = 0;

    string[] options = new string[] { "Shooting", "Melee" };
    public override void OnInspectorGUI()
    {

        GUILayout.Label("Setings");
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        this.selected = EditorGUILayout.Popup("AI type", selected, options);
        if (EditorGUI.EndChangeCheck())
        {
            
        }
    }
}
