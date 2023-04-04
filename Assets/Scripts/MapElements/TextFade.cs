using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
    [SerializeField] float duration = 1.0f; // The duration of the fade animation
    [SerializeField] TextMeshProUGUI text; // The TextMeshProUGUI component to fade

    public void EndFade()
    {
        StopAllCoroutines();
    }

    public void SetToVal(float finalVal)
    {
        Color color = text.color;
        color.a = finalVal;
        text.color = color;
    }

    public IEnumerator FadeText(float valA, float valB)
    {
        float time = 0.0f;
        Color color = text.color;

        while (time < duration)
        {
            // Calculate the current alpha value
            float alpha = Mathf.Lerp(valA, valB, time / duration);

            // Set the alpha value in the color variable
            color.a = alpha;

            // Set the text color to the new color value
            text.color = color;

            // Wait for the next frame
            yield return null;

            // Increment the time variable
            time += Time.deltaTime;
        }

        // Ensure the text is fully transparent at the end of the animation
        color.a = valB;
        text.color = color;
    }
}
