using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AIManeger))]

public class AiInspctorMenu : Editor
{

    string[] options = new string[] { "Shooting", "Melee" };
    public override void OnInspectorGUI()
    {
        AIManeger ai = (AIManeger)target;
        GUILayout.Label("Setings");
        base.OnInspectorGUI();
        ai.AIType = EditorGUILayout.Popup("AI type", ai.AIType, options);
        if (options[ai.AIType] == "Melee")
        {
            ai.AIType = 1;
            ai.Melee_attackDMG = EditorGUILayout.FloatField("Attack DMG", ai.Melee_attackDMG);
            ai.Melee_distanceToAttackMode = EditorGUILayout.FloatField("Attack distence", ai.Melee_distanceToAttackMode);
        }
    }
}
