using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> questList;
    public Quest pinned;
    [SerializeField] QuestUI questUI;

    // Start is called before the first frame update
    void Start()
    {
        SetupAllQuests();
        pinned = GetQuest("THE DIG");
        questUI.UpdateUI(pinned);
    }

    void Update() {
        // DEBUG KEY
        if (Input.GetKeyDown(KeyCode.K)) {
            //Quest q1 = GetQuest("THE DIG");
            pinned.ProgressToNextQuestStep(questUI);
            questUI.UpdateUI(pinned);
        }
    }

    // This creates every quest and then puts them into the List<Quest> quests.
    void SetupAllQuests() {
        questList = new List<Quest>();
        Quest roanoke = new Quest("ROANOKE", 
            "Homebase has lost communication with the research team stationed on this planet. Travel to their outpost and investigate the situation.",
            Quest.QuestType.main,
            Quest.QuestStatus.active);
        roanoke.questSteps.Add(000, "Travel to the research team's outpost.");
        roanoke.questSteps.Add(010, "Investigate the area.");
        roanoke.questSteps.Add(020, "Read the report records stored in the main computer.");
        roanoke.questSteps.Add(030, "Download the map data from the survey tower.");
        questList.Add(roanoke);
        questUI.CreateQuestMenuItem(roanoke);

        Quest theDig = new Quest("THE DIG",
            "The research team's primary objective was to collect samples from this planet's innermost strata. ENSURE THAT THIS EXCAVATION IS COMPLETED AT ANY MEANS NECESSARY.",
            Quest.QuestType.main,
            Quest.QuestStatus.active);
        theDig.questSteps.Add(000, "Locate the Excavation Site. It should be located at the Research Team's Outpost.");
        theDig.questSteps.Add(010, "Repair the Excavator.");
        theDig.questSteps.Add(020, "Fuel the Excavator.");
        theDig.questSteps.Add(030, "Keep the Excavator running.");
        theDig.questSteps.Add(100, "Enter the mineshaft.");
        questList.Add(theDig);
        questUI.CreateQuestMenuItem(theDig);


        Quest wayHome = new Quest("A WAY HOME",
            "When the mission's over you're cleared to return to homebase. Gather enough fuel for the journey home, and then once everything else is complete, takeoff.",
            Quest.QuestType.main,
            Quest.QuestStatus.active);
        wayHome.questSteps.Add(000, "Fuel the rocketship.");
        wayHome.questSteps.Add(100, "Complete all other main objectives.");
        wayHome.questSteps.Add(999, "Leave the planet.");
        questList.Add(wayHome);
        questUI.CreateQuestMenuItem(wayHome);


        Quest mia = new Quest("M.I.A.",
            "Two officers from the research team remain unaccounted for. Attempt to locate them, or their remains.",
            Quest.QuestType.sidequest,
            Quest.QuestStatus.undiscovered);
            // these names for the Doctors are placeholders btw.
            // these steps should probably be simultaneous steps, rather than singular, but that hasn't been implemented yet.
        mia.questSteps.Add(000, "Locate Doctor M.");
        mia.questSteps.Add(010, "Locate Doctor Cain."); 
        questList.Add(mia);
        //questUI.CreateQuestMenuItem(mia); // not doing this since the quest is marked as undiscovered.


        // Not sure how I want to handle the boss fight for the sniper yet.. or if this should link up directly with the MIA quest.
        Quest rogueSniper = new Quest("A LETHAL THREAT",
            "",
            Quest.QuestType.sidequest,
            Quest.QuestStatus.undiscovered);

    }

    public Quest GetQuest(string questName) {
        Quest desiredQuest = null;
        foreach (Quest currQuest in questList) {
            if (String.Equals(currQuest.name, questName)) {
                desiredQuest = currQuest;
                break;
            }
        }
        return desiredQuest;
    }

}
