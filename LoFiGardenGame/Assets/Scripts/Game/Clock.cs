using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Clock : MonoBehaviour
{
    [SerializeField]
    private float realTimeSecondsPerInGameMinute = 1f;

    private int hour = 0;
    private int minute = 0;
    private float timer = 0f;
    private TextMeshProUGUI clockText;
    private bool updateClock = true;

    private void Start()
    {
        clockText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= realTimeSecondsPerInGameMinute)
        {
            minute++;
            timer = 0f;
            updateClock = true;
        }

        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if (hour >= 24)
        {
            hour = 0;
        }

        if (updateClock)
        {
            clockText.SetText($"{hour.ToString("00")} : {minute.ToString("00")}");
            updateClock = false;
        }
    }
}
