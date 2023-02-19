using DS.ScriptableObjects;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DSDialogueContainerSO dialogueContainer;

    public delegate void NewSpeaker(DSDialogueSO dialogue);
    public static event NewSpeaker newDialogueEvent;

    DSDialogueSO currentDialogue;
    void Start()
    {
        currentDialogue = dialogueContainer.GetUngroupedStartingDialogues()[0];

        if (!currentDialogue) 
        { 
            Debug.LogError("No current dialogue found"); 
            return; 
        }
        PushDialogue(currentDialogue);

    }

    public static void PushDialogue(DSDialogueSO currentDialogue)
    {
        newDialogueEvent(currentDialogue);
    }
}
