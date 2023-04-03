using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [Header("Quest Journal")]
    [SerializeField] GameObject journalUI; // full menu, takes up the whole screen
    [Header("Quest Sidebar")]
    [SerializeField] GameObject questInfo; // visible usually always
    [SerializeField] TextMeshProUGUI textMesh;
    [Header("References")]
    [SerializeField] Settings settings;
    // Start is called before the first frame update
    void Start()
    {
        
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
        textMesh.text = quest.questSteps.GetValueOrDefault(quest.currentQuestStep);
    }

    public void SetInfoVisibility(bool state) {
        questInfo.SetActive(state);
    }

    public void ToggleJournalVisibility() {
        journalUI.SetActive(!journalUI.activeSelf);
    }
}
