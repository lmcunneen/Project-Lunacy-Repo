using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : JourneyScript
{
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private HorizontalLayoutGroup characterTab;
    [SerializeField] private List<CharacterBars> activeCharactersDisplay;

    private List<string> names = new List<string>
        { "Jackson", "Declan", "Rhys", "Castiel", "Joseph", "Mitchell", "Neo", "Elias", "Daniel", "Matthew", "Natalie", "Vicki", "Roger",
          "David", "Jason", "Casper", "Connor", "Ashton", "Mateo", "Mars", "Pheobe", "Hayley", "Callum", "Georgia", "James", "Jet", "John", 
          "Paul", "George", "Ringo", "Thomas", "Pete", "Billy", "Tai", "Noah", "Marcus", "Alex", "Josh", "Jack", "Torr", "Elliot", "Cooper",
          "Martin", "Olivia", "Cynthia", "Stephen", "Corey", "Oscar", "Zac", "Tarek", "Aiden", "Jacob", "Atilla", "Konrad", "Ben", "Lerm"};

    void Awake()
    {
        for (int i = 0; i < SettingsObject.numberOfPartyMembers; i++)
        {
            GenerateCharacter();
        }
    }

    void Update()
    {
        
    }

    private void GenerateCharacter()
    {
        GameObject newCharacter = Instantiate(characterPrefab);
        CharacterBars newCharacterBars = newCharacter.GetComponent<CharacterBars>();

        newCharacter.transform.SetParent(characterTab.transform);
        newCharacter.transform.localScale = characterPrefab.transform.localScale;

        GenerateName(newCharacterBars);

        activeCharactersStatic.Add(newCharacterBars);
    }

    private void GenerateName(CharacterBars character)
    {
        int randomIndex = Random.Range(0, names.Count);
        character.SetName(names[randomIndex]);

        names.RemoveAt(randomIndex);
    }
}
