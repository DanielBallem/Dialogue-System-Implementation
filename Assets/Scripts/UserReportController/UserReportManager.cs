using DS.Data;
using DS.ScriptableObjects;
using UnityEngine;
using ReportCreator;

public class UserReportManager : MonoBehaviour
{
    string currentDialogue;
    Timer timer;
    Report report = new Report();
    void Start()
    {
        timer = GetComponent<Timer>();
        SubscribeToDialogueEvents();
    }

    public void SubscribeToDialogueEvents()
    {
        DialogueController.newDialogueEvent += OnNewDialogue;
        UIChoiceController.choiceSelectedEvent += EndTimer;
    }

    private void OnDestroy()
    {
        DialogueController.newDialogueEvent -= OnNewDialogue;
        UIChoiceController.choiceSelectedEvent -= EndTimer;
    }

    public void OnNewDialogue(DSDialogueSO dialogue) {
        if (!dialogue)
        {
            SaveReport();
        }
        else { 
        currentDialogue = dialogue.Text;
        timer.StartTimer();
        }
    }

    private void SaveReport()
    {
        report.Save("UserReport.csv");
    }

    public void EndTimer(DSDialogueChoiceData choice) {
        float timeInSeconds = timer.EndTimer();
        report.AddEntry(currentDialogue, choice.Text, timeInSeconds);
    }
}

