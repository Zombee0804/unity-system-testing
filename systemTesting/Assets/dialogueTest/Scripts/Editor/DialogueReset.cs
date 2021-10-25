using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueManager))]
public class DialogueReset : Editor
{
    public override void OnInspectorGUI () {

        DialogueManager manager = (DialogueManager)target;
        
        if (GUILayout.Button("Reset") && Application.isPlaying) {
            manager.Reset();
        }

        base.OnInspectorGUI();
    }
}
