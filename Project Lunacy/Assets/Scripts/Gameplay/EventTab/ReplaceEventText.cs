using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceEventText : MonoBehaviour
{
    public static string InsertCharactersInDescription(string description, List<CharacterBars> eventCharacters)
    {        
        switch(eventCharacters.Count)
        {
            case 1:
                description = description.Replace("<Char1>", eventCharacters[0].name);
                return description;

            case 2:
                description = description.Replace("<Char1>", eventCharacters[0].name);
                description = description.Replace("<Char2>", eventCharacters[1].name);
                return description;

            case 3:
                description = description.Replace("<Char1>", eventCharacters[0].name);
                description = description.Replace("<Char2>", eventCharacters[1].name);
                description = description.Replace("<Char3>", eventCharacters[2].name);
                return description;

            case 4:
                description = description.Replace("<Char1>", eventCharacters[0].name);
                description = description.Replace("<Char2>", eventCharacters[1].name);
                description = description.Replace("<Char3>", eventCharacters[2].name);
                description = description.Replace("<Char4>", eventCharacters[3].name);
                return description;

            default:
                Debug.LogWarning("Invalid input for InsertCharactersInDescription! Returning original string");
                return description;
        }
    }
}
