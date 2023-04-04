using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    public string name;
    public string description;
    public QuestType questType;
    public QuestStatus questStatus; // used for status
    public Dictionary<int, string> questSteps; 
    public int currentQuestStep;

    public enum QuestStatus {
        undiscovered,
        active,
        completed
    }

    public enum QuestType {
        main,
        sidequest
    }

    public Quest(string newName, string newDesc, QuestType newType, QuestStatus newQuestStatus) {
        this.name = newName;
        this.description = newDesc;
        this.questType = newType;
        this.questStatus = newQuestStatus;
        this.questSteps = new Dictionary<int, string>();
        this.currentQuestStep = 000;
    }

    // These are a little clunky, but it shouldn't be a big deal.
    // I was worried that this wouldn't be iterating in order since it's a dictionary, but it seems to check out.
    // Returns true if successful, returns false otherwise. If false, then we are already at the last available queststep.
    public bool ProgressToNextQuestStep(QuestUI qUI) {
        bool foundCurrent = false;
        foreach(var q in questSteps) {
            //Debug.Log("<QUEST_STEP: " + q.Key);
            if (foundCurrent) {
                //Debug.Log("<QUEST: UPDATING " + currentQuestStep + " TO " + q.Key + ">");
                currentQuestStep = q.Key;
                qUI.UpdateUI(this);
                return true;
            }
            if (q.Key == currentQuestStep) {
                // grabs the quest on the next step
                foundCurrent = true;
            }
            
        }
        return false;
    }

    public void SetQuestStep(int questStep, QuestUI qUI) {
        this.currentQuestStep = questStep;
        qUI.UpdateUI(this);
    }

    // This method should be called for quests not created at startup - because those get initialized to active...
    // This method assumes the quest has not been unlocked yet. This means that the questStatus is undiscovered.
    // Nothing catastrophic should occur if it is already active before this method is called,
    // but if this method is called twice then there might be duplicates in the quest journal.
    public void DiscoverQuest(QuestUI qUI)
    {
        questStatus = QuestStatus.active;
        currentQuestStep = 0;
        qUI.CreateQuestMenuItem(this);
        qUI.UpdateUI(this);
    }


}