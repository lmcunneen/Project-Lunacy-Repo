using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChoiceData
{
    public string choiceName;
    public List<ChoiceResult> results = new List<ChoiceResult>(new ChoiceResult[1]);
}

[Serializable]
public class ChoiceResult
{
    public string resultName;
    [TextArea(10, 30)]
    public string resultDescription;
    public string resultChoiceText;
    public List<ChoiceEffect> effects = new List<ChoiceEffect> ( new ChoiceEffect[1]);
}

[Serializable]
public struct ChoiceEffect
{
    public JourneyScript.EffectType effectType;
    [Range(-100, 100)]
    public int effectAmount;
}

[CreateAssetMenu(fileName = "EventName", menuName = "Project Lunacy/Journey Event", order = 1)]
public class JourneyEvent : ScriptableObject
{
    public enum EventSize 
    {
        Solo,
        Duo,
        Trio,
        Quadrio,
        WholeParty
    }
    
    public string displayName;
    public EventSize eventSize;

    [Header("----Parameters----")]
    [TextArea(10,30)]
    public string description;
    public List<ChoiceData> choiceList;
}
