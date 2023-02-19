using UnityEngine;
using UnityEngine.UI;
using DS.Enumerations;
using DS.ScriptableObjects;

public class SpeakerController : MonoBehaviour
{
    public DSSpeaker speaker = DSSpeaker.Person1;
    Outline outline;
    // Start is called before the first frame update

    private void Awake()
    {
        outline = GetComponent<Outline>();
        if (!outline) Debug.LogError("No outline on speaker");
        
        SubscribeToDialogueEvent();
    }

    public void SubscribeToDialogueEvent() {
        DialogueController.newDialogueEvent += ToggleOutline;
    }

    private void OnDestroy()
    {
        DialogueController.newDialogueEvent -= ToggleOutline;
    }

    private void ToggleOutline(DSDialogueSO currDialogue)
    {
        if (currDialogue == null) { 
            outline.enabled = false;
            return;
        }
        outline.enabled = currDialogue.Speaker == speaker;
    }
}
