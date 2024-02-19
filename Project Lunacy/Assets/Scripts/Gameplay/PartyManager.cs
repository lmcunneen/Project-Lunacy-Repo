using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : JourneyScript
{
    [SerializeField] private List<CharacterBars> activeCharactersReference;
    
    void Start()
    {
        activeCharactersReference = activeCharactersStatic;
    }

    void Update()
    {
        
    }
}
