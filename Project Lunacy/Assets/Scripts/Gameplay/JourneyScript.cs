using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JourneyScript : MonoBehaviour
{
    //This is a parent class that all journey-related scripts derive from.

    public enum EffectType
    {
        //Constitution Types
        Vitality,
        Willpower,
        Sanity,
        //Special Event Types
        JourneyLength,
        AddNewCharacter,
        //No effect
        None
    }

    //STATIC VARIABLES - Set by JourneyLogic, can be read by all JourneyScript child classes
    public static uint stepCountStatic;
    public static int dayCountStatic = 7;
    public static List<CharacterBars> activeCharactersStatic = new();
}
