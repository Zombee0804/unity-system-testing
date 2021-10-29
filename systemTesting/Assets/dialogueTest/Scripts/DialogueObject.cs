using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="DialogueObject", menuName = "Scriptable Objects/Dialgoue Test/Dialogue Object")]
public class DialogueObject : ScriptableObject
{
    public string speaker;
    public string[] sentences;
    public ResponseObject[] responses;
    public DialogueObject[] additionalDialogues;
}
