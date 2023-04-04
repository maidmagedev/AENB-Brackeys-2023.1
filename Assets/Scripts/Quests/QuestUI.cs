using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [Header("Quest Journal")]
    [SerializeField] GameObject journalUI; // full menu, takes up the whole screen
    [SerializeField] TextMeshProUGUI journalHighlightedQuestTitle;
    [SerializeField] TextMeshProUGUI journalHighlightedQuestDesc; // on the UI, this is the giant section on the right of the menu.
    [SerializeField] TextMeshProUGUI journalHighlightedQuestStep; // part of that ui above.
    [SerializeField] GameObject pfQuestMenuPanel; // where questMenuItem prefabs are instantiated into
    [SerializeField] GameObject pfQuestMenuItem; // prefab, instantiated for each quest.
    private List<QuestButton> questButtons;

    [Header("Quest Sidebar")]
    [SerializeField] GameObject questInfo; // visible usually always
    [SerializeField] TextMeshProUGUI textMesh;

    [Header("References")]
    [SerializeField] Settings settings;
    // Start is called before the first frame update
    void Start()
    {
        if (questButtons == null)
        {
            questButtons = new List<QuestButton>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(settings.questJournalKey)) {
            ToggleJournalVisibility();
        }
    }

    public void UpdateUI(Quest quest) {
        //Debug.Log("Feeding in - " + quest.currentQuestStep);
        // Update the Sidebar
        textMesh.text = quest.questSteps.GetValueOrDefault(quest.currentQuestStep);

        // Update the Journal
        foreach (QuestButton qb in questButtons)
        {
            if (qb.storedQuest == quest)
            {
                qb.ChangeDescription();
            }
        }
    }

    public void SetInfoVisibility(bool state) {
        questInfo.SetActive(state);
    }

    public void ToggleJournalVisibility() {
        journalUI.SetActive(!journalUI.activeSelf);
    }

    public void CreateQuestMenuItem(Quest quest)
    {
        Debug.Log("Creating a QuestMenuItem.");
        GameObject spawnedItem = Instantiate(pfQuestMenuItem, pfQuestMenuPanel.transform);
        TextMeshProUGUI itemText = spawnedItem.GetComponentInChildren<TextMeshProUGUI>();
        itemText.text = quest.name; // on the  button itself

        // tell the button what to swap information with
        QuestButton buttonLogic = spawnedItem.GetComponent<QuestButton>();
        buttonLogic.storedQuest = quest;
        buttonLogic.mainQuestTitle = journalHighlightedQuestTitle;
        buttonLogic.mainQuestDescription = journalHighlightedQuestDesc;
        buttonLogic.currentStepInfo = journalHighlightedQuestStep;

        if (questButtons == null)
        {
            questButtons = new List<QuestButton>();
        }
        questButtons.Add(buttonLogic);
    }
}
