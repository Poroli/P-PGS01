using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private bool debugMessages;

    private static TMP_Text dialogueName;
    private static TMP_Text dialogueText;
    private static int dialoguePartIndicator = 0;
    private static Dialogue tmpDialogue;
    private static GameObject dialogueWindowGO;

    public static void StartDialogue(string DialogueID)
    {
        // Set the Dialogue
        for (int i = 0; i < CentalSOAssing.dialoguesSORef.Dialogues.Length; i++)
        {
            if (CentalSOAssing.dialoguesSORef.Dialogues[i].DialogueID != DialogueID)
            {
                continue;
            }
            tmpDialogue = CentalSOAssing.dialoguesSORef.Dialogues[i];
            break;
        }

        //activate Dialogue Object
        dialogueName.text = tmpDialogue.DialogueParts[dialoguePartIndicator].NameWhoSpeaks;
        dialogueText.text = tmpDialogue.DialogueParts[dialoguePartIndicator].TextSpoken;

        dialoguePartIndicator = 1;
        dialogueWindowGO.SetActive(true);
    }

    public void NextPartOfDialogue()
    {
        print("next DialoguePart");
        if (dialoguePartIndicator < tmpDialogue.DialogueParts.Length)
        {
            dialogueName.text = tmpDialogue.DialogueParts[dialoguePartIndicator].NameWhoSpeaks;
            dialogueText.text = tmpDialogue.DialogueParts[dialoguePartIndicator].TextSpoken;

            dialoguePartIndicator++;
        }
        else
        {
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        dialoguePartIndicator = 0;
        tmpDialogue = null;
        dialogueName.text = null;
        dialogueText.text = null;
        //deactivate Dialogue Object
        dialogueWindowGO.SetActive(false);
    }

    private void Start()
    {
        dialogueWindowGO = GameObject.Find("DialogueBox");
        dialogueName = dialogueWindowGO.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        dialogueText = dialogueWindowGO.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>();

        if (debugMessages)
        {
            print("DialogueName GO:" + dialogueName.name);
            print("DialogueText GO:" + dialogueText.name);
        }

        dialogueWindowGO.SetActive(false);
    }
}
