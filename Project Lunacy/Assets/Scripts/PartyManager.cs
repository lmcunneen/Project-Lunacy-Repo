using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : JourneyLogic
{
    [SerializeField] private List<CharacterBars> activeCharactersReference;
    
    void Start()
    {
        activeCharactersReference = activeCharacters;
    }

    void Update()
    {
        
    }
}
