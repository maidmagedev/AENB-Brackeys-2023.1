using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script can be called by other functions, but innately isn't supposed to do anything itself on Start() or Update().
public class TextScroller : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    private string textToWrite;
    private IEnumerator coroutine;
    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DisplayText(string newText) {
        if (isActive) {
            return;
        }
        textToWrite = newText;
        coroutine = DisplayTextByChar();
        StartCoroutine(coroutine);
    }

    public void HideText() {
        StopCoroutine(coroutine);
        text.text = "";
    }

    IEnumerator DisplayTextByChar() {
        // clear the text
        isActive = true;
        text.text = "";
        foreach (char c in textToWrite) {
            text.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        isActive = false;
    }

    

    
}
