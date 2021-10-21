using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="DialogueObject", menuName = "Dialogue Object")]
public class DialogueObject : ScriptableObject
{
    public string speaker;
    public string[] sentences;
    public ResponseObject[] responses;
}
