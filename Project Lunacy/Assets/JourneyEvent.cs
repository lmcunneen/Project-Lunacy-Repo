using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChoiceData
{
    public CharacterBars.ConsitutionType effectType;
    public int effectAmount;
}

[CreateAssetMenu(fileName = "EventName", menuName = "Project Lunacy/Journey Event", order = 1)]
public class JourneyEvent : ScriptableObject
{
    [SerializeField] private string displayName;
    [SerializeField] private List<ChoiceData> choiceList;
}
