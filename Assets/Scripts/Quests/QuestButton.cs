using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script is added onto each QuestMenuItem prefab, since these are prefabs that get instantiated during runtime, the buttons need to reference something that exists
// on the prefab. The buttons simply get a Quest reference when they are instantiated, and using that Quest reference they will update the current selected quest description.
// Thats the giant box on the right in the UI.
public class QuestButton : MonoBehaviour
{
    public Quest storedQuest;
    public TextMeshProUGUI mainQuestTitle;
    public TextMeshProUGUI mainQuestDescription;
    public TextMeshProUGUI currentStepInfo;
    
    // Called by buttons, and some scripts.
    public void ChangeDescription()
    {
        mainQuestTitle.text = storedQuest.name;
        mainQuestDescription.text = storedQuest.description;
        currentStepInfo.text = storedQuest.questSteps.GetValueOrDefault(storedQuest.currentQuestStep);
    }
}
