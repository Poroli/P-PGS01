using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues")]
public class DialoguesSO : ScriptableObject
{
    public Dialogue[] Dialogues;
}

[Serializable]
public class Dialogue
{
    public string DialogueID;
    public DialoguePart[] DialogueParts;
}

[Serializable]
public class DialoguePart
{
    public string NameWhoSpeaks;
    [TextArea(3, 10)] public string TextSpoken;
}
