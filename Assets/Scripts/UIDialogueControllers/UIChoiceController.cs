using DS.Data;
using DS.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChoiceController : MonoBehaviour
{
    public delegate void ChoiceSelected(DSDialogueChoiceData choice);
    public static event ChoiceSelected choiceSelectedEvent;


    public GameObject buttonPrefab;
    Text dialogueText;

    private List<GameObject> buttons = new List<GameObject>();
    private void Awake()
    {
        SubscribeToDialogueEvent();
        dialogueText = GetComponentInChildren<Text>();
        if (!dialogueText) Debug.LogError("No text found in UI to display dialogue.");
    }

    private void SubscribeToDialogueEvent()
    {
        DialogueController.newDialogueEvent += PopulateUi;
    }

    private void OnDestroy()
    {
        DialogueController.newDialogueEvent -= PopulateUi;
    }

    private void PopulateUi(DSDialogueSO dialogue)
    {
        ResetChoiceButtons();

        if (dialogue == null) {
            dialogueText.text = "End";
            return;
        }
        dialogueText.text = dialogue.Text;

        
        PopulateChoiceButtons(dialogue);
    }

    private void PopulateChoiceButtons(DSDialogueSO dialogue)
    {
        List<DSDialogueChoiceData> choices = dialogue.Choices;
        foreach (DSDialogueChoiceData choice in choices)
        {
            AddChoiceButton(choice);
        }
    }

    private void ResetChoiceButtons()
    {
        foreach (GameObject button in buttons) {
            GameObject.Destroy(button);
        }
        buttons.Clear();
    }

    void AddChoiceButton(DSDialogueChoiceData choice)
    {
        GameObject instance = Instantiate(buttonPrefab, transform);
        instance.GetComponentInChildren<Text>().text = choice.Text;

        // Get the Button component in the instance and add an onClick event
        Button buttonComponent = instance.GetComponent<Button>();
        buttonComponent.onClick.AddListener(() => OnButtonClick(choice));
        buttons.Add(instance);
    }

    private void OnButtonClick(DSDialogueChoiceData choiceSelected)
    {
        choiceSelectedEvent(choiceSelected);
        DialogueController.PushDialogue(choiceSelected.NextDialogue);
    }
}
