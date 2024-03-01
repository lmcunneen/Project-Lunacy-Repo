using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEventEffect : JourneyScript
{
    /* 
      This script is predicated on the EffectType enum, so the following comment is the enum structure that this script
      is basing it's logic off. If this is outdated, the script needs to be updated:
      
      0 - Vitality
      1 - Willpower,
      2 - Sanity,
      3 - JourneyLength,
      4 - AddNewCharacter,
      5 - None
     */

    public static void ApplyAllEffects(ChoiceResult givenResult, List<CharacterBars> activeEventCharacters, JourneyLogic journeyLogic)
    {
        foreach (var effect in givenResult.effects)
        {
            ApplyTypeAndValue(effect.effectType, effect.effectAmount, activeEventCharacters, journeyLogic);
        }
    }
    
    private static void ApplyTypeAndValue(EffectType givenType, int givenValue, List<CharacterBars> effectedCharacters, JourneyLogic journeyLogic)
    {
        switch(givenType)
        {
            case EffectType.Vitality:
            case EffectType.Willpower:
            case EffectType.Sanity:

                foreach (var character in effectedCharacters)
                {
                    character.SetConstitutionValue(givenType, givenValue);
                }

                break;

            case EffectType.JourneyLength:
                journeyLogic.ChangeDayLength(givenValue);
                break;

            case EffectType.AddNewCharacter:
                //Add new Character here
                break;

            case EffectType.None:
                Debug.Log("'None' input for ApplyTypeAndValue, so it's being skipped");
                break;

            default:
                Debug.LogError("Invalid Input for ApplyTypeAndValue. Debug immediately!");
                break;
        }
    }
}
