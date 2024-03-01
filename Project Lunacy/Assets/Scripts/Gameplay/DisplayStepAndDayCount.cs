using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStepAndDayCount : MonoBehaviour
{
    [SerializeField] private Text stepCounter;
    [SerializeField] private Text daysLeftCounter;
    [SerializeField] private Text dayLengthCounter;

    void Update()
    {
        stepCounter.text = "Steps taken: " + (JourneyScript.stepCountStatic - JourneyLogic.dayStartStepCount);
        daysLeftCounter.text = "Days left: " + JourneyScript.dayCountStatic;
        dayLengthCounter.text = "Current day length: " + JourneyLogic.dayLengthMod;
    }
}
