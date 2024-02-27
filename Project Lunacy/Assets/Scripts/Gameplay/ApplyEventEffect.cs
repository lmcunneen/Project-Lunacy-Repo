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

    public static void ApplyAllEffects(ChoiceResult givenResult, List<CharacterBars> activeEventCharacters)
    {
        foreach (var effect in givenResult.effects)
        {
            ApplyTypeAndValue(effect.effectType, effect.effectAmount, activeEventCharacters);
        }
    }
    
    private static void ApplyTypeAndValue(EffectType givenType, int givenValue, List<CharacterBars> effectedCharacters)
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
        }
    }
}
