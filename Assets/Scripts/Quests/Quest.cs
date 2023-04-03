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
    public bool ProgressToNextQuestStep() {
        bool foundCurrent = false;
        foreach(var q in questSteps) {
            //Debug.Log("<QUEST_STEP: " + q.Key);
            if (foundCurrent) {
                //Debug.Log("<QUEST: UPDATING " + currentQuestStep + " TO " + q.Key + ">");
                currentQuestStep = q.Key;
                return true;
            }
            if (q.Key == currentQuestStep) {
                foundCurrent = true;
            }
            
        }
        return false;
    }

    public void SetQuestStep(int questStep) {
        this.currentQuestStep = questStep;
    }

    
}