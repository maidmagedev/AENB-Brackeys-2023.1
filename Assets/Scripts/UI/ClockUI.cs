using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockUI : MonoBehaviour
{
    private TextMeshProUGUI clockText;
    // Start is called before the first frame update
    void Start()
    {
        clockText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        (float minutes, float seconds, bool isDay) dayNightInfo = FindObjectOfType<DayNightCycle>().getTime();
        if (dayNightInfo.isDay)
        {
            clockText.text = "Time till night: ";
        }
        else
        {
            clockText.text = "Time till day: ";
        }

        clockText.text += dayNightInfo.minutes + ":" + (dayNightInfo.seconds < 10 ? "0" + dayNightInfo.seconds: dayNightInfo.seconds);
    }
}
