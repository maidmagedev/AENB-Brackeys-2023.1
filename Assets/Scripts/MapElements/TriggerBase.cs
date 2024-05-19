using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerBase : MonoBehaviour
{
    [Header("Required Settings")]
    [SerializeField] ModeOfActivation activationMode = ModeOfActivation.OnEnter; // Determines when functions are called

    [Header("OPTIONAL: Target Selectivity Options")]
    [SerializeField] bool onlyTargetColWithTag = true; // Only target colliders with the tagToTarget tag when enabled, ignoring everything else.
    [SerializeField] string tagToTarget = "Player"; // case sensitive probably

    [Header("OPTIONAL: Action Specific Fields")]
    [SerializeField] string interactionPromptText; // REQ: activationMode == ModeOfActivation.InteractionPrompt

    [Header("References")]
    [SerializeField] Settings settings;

    [SerializeField] TextMeshProUGUI promptTextMesh; // This should be hooked up by default in the prefab, but if you are not in 
                                                     // the prefab then you might need to add it manually.
                                                     // REQ: activationMode == ModeOfActivation.InteractionPrompt
    [SerializeField] TextFade textFade;

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
        if (interactionPromptText != null && promptTextMesh != null) {
            promptTextMesh.text = interactionPromptText;
        }
    }

    // Finds necessary fields that may be empty/null or other references that are not able to be set up prior to runtime.
    void VerifyOrFindReferences()
    {
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
        if (textFade == null)
        {
            textFade = GetComponent<TextFade>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnEnter");
        if (onlyTargetColWithTag && !collision.CompareTag(tagToTarget))
        {
            // targetting a specific target, this collider is not the target.
            return;
        }

        switch (activationMode)
        {
            case ModeOfActivation.OnEnter:
                DoAction();
                break;
            case ModeOfActivation.InteractionPrompt:
                //promptTextMesh.gameObject.SetActive(true);
                textFade.EndFade();
                StartCoroutine(textFade.FadeText(0.0f, 1.0f));
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
        Debug.Log("Exit");
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
                textFade.EndFade();
                StartCoroutine(textFade.FadeText(1.0f, 0.0f));
                //promptTextMesh.gameObject.SetActive(false);
                break;
        }
    }

    public virtual void DoAction()
    {
        Debug.Log("Original DoAction()");
    }
}
