using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ChoiceData
{
    public string choiceName;
    public List<ChoiceResult> results;
}

[Serializable]
public struct ChoiceResult
{
    public string resultName;
    [TextArea(10, 30)]
    public string resultDescription;
    public string resultChoiceText;
    public JourneyScript.ConsitutionType effectType;
    [Range(-100, 100)]
    public int effectAmount;
}

[CreateAssetMenu(fileName = "EventName", menuName = "Project Lunacy/Journey Event", order = 1)]
public class JourneyEvent : ScriptableObject
{
    public string displayName;
    [TextArea(10,30)]
    public string description;
    public List<ChoiceData> choiceList;
}
