using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogueClient : MonoBehaviour
{

    [HideInInspector] public string IDSelectedDialogue;

    public void WaitForDialogue()
    {
        StartCoroutine(WaitDialoguePartnerIsReached());
    }

    private IEnumerator WaitDialoguePartnerIsReached()
    {
        yield return new WaitUntil(PlayerMove.DialoguePartnerReached);
        print("PlayDialogue");
        DialogueManager.StartDialogue(IDSelectedDialogue);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueClient))]
class ActivateNPCDialogueEditor : Editor
{
    private DialogueClient dialogueClient;
    private DialoguesSO tempDialoguesSO;

    private void OnEnable()
    {
        MonoBehaviour monoBev = (MonoBehaviour)target;
        dialogueClient = monoBev.GetComponent<DialogueClient>();
        tempDialoguesSO = CentalSOAssing.dialoguesSORef;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Current Selected DialogueID: " + dialogueClient.IDSelectedDialogue))
        {
            GenericMenu menu = new GenericMenu();
            for (int i = 0; i < tempDialoguesSO.Dialogues.Length; i++)
            {
                string tempDialogueID = tempDialoguesSO.Dialogues[i].DialogueID;
                menu.AddItem(new GUIContent(tempDialoguesSO.Dialogues[i].DialogueID), false, () =>
                {
                    dialogueClient.IDSelectedDialogue = tempDialogueID;
                    Debug.Log("DialogueSelected: " + tempDialogueID);
                });
            }
            menu.ShowAsContext();
        }
    }
}
#endif
