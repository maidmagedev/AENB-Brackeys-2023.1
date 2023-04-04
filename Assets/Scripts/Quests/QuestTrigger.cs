using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script should be on a gameobject with a trigger collider. Otherwise it does nothing.
// This is only built to do one singular action.
public class QuestTrigger : MonoBehaviour
{
    [Header("Required Settings")]
    [SerializeField] string questName;
    [SerializeField] TriggerAction triggerAction; // determines function to call.
    [SerializeField] ModeOfActivation activationMode = ModeOfActivation.OnEnter; // Determines when functions are called

    [Header("OPTIONAL: Target Selectivity Options")]
    [SerializeField] bool onlyTargetColWithTag = true; // Only target colliders with the tagToTarget tag when enabled, ignoring everything else.
    [SerializeField] string tagToTarget = "Player"; // case sensitive probably
    
    [Header("OPTIONAL: Action Specific Fields")]
    [SerializeField] int newQuestStep; // REQ: TriggerAction.SetQuestToStep
                                       // Sets the desired quest's queststep to this int newQuestStep.

    [SerializeField] bool onlyUpdateIfOnExpectedStep; // Perform functionality if the expected current step is true.
    [SerializeField] int expectedCurrentStep; // used only if bool above is true

    [SerializeField] string interactionPromptText; // REQ: activationMode == ModeOfActivation.InteractionPrompt

    [Header("References")] 
    // this group can be empty bc they are hooked up in Start(), but its a good idea to do it manually if you have time. (EXCEPT promptTextMesh)
    [SerializeField] QuestManager qMan;
    [SerializeField] QuestUI qUI;
    [SerializeField] Settings settings;

    [SerializeField] TextMeshProUGUI promptTextMesh; // This should be hooked up by default in the prefab, but if you are not in 
                                                     // the prefab then you might need to add it manually.
                                                     // REQ: activationMode == ModeOfActivation.InteractionPrompt

    private Quest q; // This only exists at runtime, and cannot be hooked up prior.


    public enum TriggerAction {
        DiscoverQuest,
        ProgressQuest,
        SetQuestToStep
    }

    public enum ModeOfActivation
    {
        OnEnter,
        OnExit,
        InteractionPrompt
    }


    // Start is called before the first frame update
    void Start()
    {
        VerifyOrFindReferences();
    }

    // Finds necessary fields that may be empty/null or other references that are not able to be set up prior to runtime.
    void VerifyOrFindReferences()
    {
        if (qMan == null)
        {
            qMan = FindObjectOfType<QuestManager>();
        }
        if (qUI == null)
        {
            qUI = FindObjectOfType <QuestUI>();
        }
        if (settings == null)
        {
            settings = FindObjectOfType<Settings>();
        }
        // Finding the text mesh is weird since we cant use FindObjectOfType<>().
        // This assumes this object with the collider, has a canvas child, which has a textmesh child.
        if (activationMode == ModeOfActivation.InteractionPrompt && promptTextMesh == null)
        {
            Debug.Log("<QuestTrigger.cs> : promptTextMesh is null, attempting to find a suitable textmesh but it may fail.");
            promptTextMesh = transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        }

        //q = qMan.GetQuest(questName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onlyTargetColWithTag && !collision.CompareTag(tagToTarget))
        {
            // targetting a specific target, this collider is not the target.
            return;
        }

        switch(activationMode)
        {
            case ModeOfActivation.OnEnter:
                DoAction();
                break;
            case ModeOfActivation.InteractionPrompt:
                promptTextMesh.gameObject.SetActive(true);
                if (Input.GetKeyDown(settings.interactKey))
                {
                    DoAction();
                }
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (onlyTargetColWithTag && !collision.CompareTag(tagToTarget))
        {
            // targetting a specific target, this collider is not the target.
            return;
        }

        if (activationMode == ModeOfActivation.InteractionPrompt)
        {
            if (Input.GetKeyDown(settings.interactKey))
            {
                DoAction();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onlyTargetColWithTag && !collision.CompareTag(tagToTarget))
        {
            // targetting a specific target, this collider is not the target.
            return;
        }

        switch (activationMode)
        {
            case ModeOfActivation.OnExit:
                DoAction();
                break;
            case ModeOfActivation.InteractionPrompt:
                promptTextMesh.gameObject.SetActive(false);
                break;
        }
            
    }

    private void DoAction()
    {
        if (q == null)
        {
            q = qMan.GetQuest(questName);
        }

        if (onlyUpdateIfOnExpectedStep && q.currentQuestStep != expectedCurrentStep)
        {
            return;
        }

        switch(triggerAction)
        {
            case TriggerAction.DiscoverQuest:
                q.DiscoverQuest(qUI);
                break;
            case TriggerAction.ProgressQuest:
                q.ProgressToNextQuestStep(qUI);
                break;
            case TriggerAction.SetQuestToStep:
                q.SetQuestStep(newQuestStep, qUI);
                break;
        }
    }
}
