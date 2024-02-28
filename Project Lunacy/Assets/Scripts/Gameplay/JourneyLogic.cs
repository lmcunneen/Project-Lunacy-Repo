using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JourneyLogic : JourneyScript
{
    [SerializeField] private uint stepCountDisplay;

    //STATIC VARIABLES:
    public List<CharacterBars> activeCharacters = new List<CharacterBars>();
    public List<CharacterBars> activeEventCharacters = new();
    
    [SerializeField] private float stepTimeSeconds = 1f;

    public List<JourneyEvent> activeEvents = new();
    
    private List<JourneyEvent> activeSoloEvents = new();
    private List<JourneyEvent> activeDuoEvents = new();
    private List<JourneyEvent> activeTrioEvents = new();
    private List<JourneyEvent> activeQuadrioEvents = new();
    private List<JourneyEvent> activeWholePartyEvents = new();

    private EventScreen eventScreenComponent;

    private bool isWaiting = false;
    private bool eventIsChosen = false;
    public static bool isOccurrenceEvent = false;

    void Start()
    {
        eventScreenComponent = GetComponent<EventScreen>();

        eventScreenComponent.CloseEventTab();
        
        foreach (var character in FindObjectsByType<CharacterBars>(FindObjectsSortMode.InstanceID))
        {
            activeCharacters.Add(character);
        }

        foreach (var jEvent in activeEvents)
        {
            if (jEvent.choiceList.Count > 4)
            {
                Debug.LogError(jEvent + " has greater than four choices. Fix immediately!");
            }

            SortJourneyEventType(jEvent);
        }
    }

    void Update()
    {
        if (!isWaiting)
        {
            StartCoroutine(IncrementStep());
        }

        if (stepCountStatic % 5 != 0)
        {
            eventIsChosen = false;
            return;
        }
        
        if (!eventIsChosen)
        {
            eventIsChosen = true;

            eventScreenComponent.OpenEventTab();

            JourneyEvent chosenEvent = GenerateRandomEvent();

            eventScreenComponent.DisplayEvent(chosenEvent, activeEventCharacters);

            StopAllCoroutines();
        }
    }

    private void SortJourneyEventType(JourneyEvent givenJourneyEvent)
    {
        JourneyEvent.EventSize givenEventSize = givenJourneyEvent.eventSize;
        
        switch(givenEventSize)
        {
            case JourneyEvent.EventSize.Solo:
                activeSoloEvents.Add(givenJourneyEvent);
                break;

            case JourneyEvent.EventSize.Duo:
                activeDuoEvents.Add(givenJourneyEvent);
                break;

            case JourneyEvent.EventSize.Trio:
                activeTrioEvents.Add(givenJourneyEvent);
                break;

            case JourneyEvent.EventSize.Quadrio:
                activeQuadrioEvents.Add(givenJourneyEvent);
                break;

            case JourneyEvent.EventSize.WholeParty:
                activeWholePartyEvents.Add(givenJourneyEvent);
                break;

            default:
                Debug.LogError("Invalid Input for SetJourneyEventType. Debug immediately!");
                break;
        }
    }
    
    private JourneyEvent GenerateRandomEvent()
    {
        JourneyEvent.EventSize randomEventSize = (JourneyEvent.EventSize)Random.Range(0, 4);
        List<JourneyEvent> randomEventPool = GetJourneyEventList(randomEventSize);

        activeEventCharacters = AssignCharactersToEvent(randomEventSize);

        if (activeEventCharacters == null)
        {
            var soloEventSize = JourneyEvent.EventSize.Solo;
            randomEventPool = GetJourneyEventList(soloEventSize);

            activeEventCharacters = AssignCharactersToEvent(soloEventSize);
        }

        int randomIndex = Random.Range(0, randomEventPool.Count);

        return randomEventPool[randomIndex];
    }

    private List<JourneyEvent> GetJourneyEventList(JourneyEvent.EventSize givenEventSize)
    {
        switch(givenEventSize)
        {
            case JourneyEvent.EventSize.Solo:
                return activeSoloEvents;

            case JourneyEvent.EventSize.Duo:
                return activeDuoEvents;
            
            case JourneyEvent.EventSize.Trio:
                return activeTrioEvents;

            case JourneyEvent.EventSize.Quadrio:
                return activeQuadrioEvents;

            case JourneyEvent.EventSize.WholeParty:
                return activeWholePartyEvents;

            default:
                Debug.LogError("Invalid Input for GetJourneyEventList. Debug immediately!");
                return null;
        }
    }

    private List<CharacterBars> AssignCharactersToEvent(JourneyEvent.EventSize givenEventSize)
    {
        switch(givenEventSize)
        {
            case JourneyEvent.EventSize.Solo:
                return ReturnRandomPartyList(1, false);
                                             
            case JourneyEvent.EventSize.Duo:
                return ReturnRandomPartyList(2, false);
                                             
            case JourneyEvent.EventSize.Trio: 
                return ReturnRandomPartyList(3, false);

            case JourneyEvent.EventSize.Quadrio:
                return ReturnRandomPartyList(4, false);

            case JourneyEvent.EventSize.WholeParty:
                return ReturnRandomPartyList(99, true);

            default:
                Debug.LogError("Invalid Input for AssignCharactersToEvent. Debug immediately!");
                return null;
        }
    }

    private List<CharacterBars> ReturnRandomPartyList(int amountOfPartyMembers, bool isWholeParty)
    {
        if (activeCharacters.Count < amountOfPartyMembers && !isWholeParty) //This doesn't change the event, only the participants. Fix whenever you get the chance
        {
            Debug.LogWarning("Chosen Event Size too large for current party! Resetting to One!");
            return null;
        }
        
        List<CharacterBars> availableCharacters = activeCharacters;
        List<CharacterBars> eventCharacters = new();
        
        for (int i = 0; i < amountOfPartyMembers; i++)
        {
            if (activeCharacters.Count <= 0) //Safeguards the list from being checked if empty
            {
                return eventCharacters;
            }
            
            int randomCharacterIndex = Random.Range(0, availableCharacters.Count);

            eventCharacters.Add(activeCharacters[randomCharacterIndex]);
            availableCharacters.RemoveAt(randomCharacterIndex);
        }

        return eventCharacters;
    }

    public void ChoiceResultEventTab(ChoiceData givenChoiceData)
    {
        if (isOccurrenceEvent)
        {
            ApplyEventEffect.ApplyAllEffects(givenChoiceData.results[0], activeEventCharacters);
        
            CloseEventTab();
            return;
        }
        
        if (givenChoiceData.results.Count < 1)
        {
            Debug.Log("No results for '" + givenChoiceData.choiceName + "', so event tab has closed");
            CloseEventTab();
            return;
        }
        
        int randomIndex = Random.Range(0, givenChoiceData.results.Count);
        ChoiceResult result = givenChoiceData.results[randomIndex];

        eventScreenComponent.DisplayChoiceResult(result);

        ApplyEventEffect.ApplyAllEffects(result, activeEventCharacters);
    }

    public void CloseEventTab()
    {
        eventScreenComponent.CloseEventTab();

        List<CharacterBars> listReference = new();
        
        foreach (var character in activeEventCharacters)
        {
            activeCharacters.Add(character);
            listReference.Add(character);
        }

        foreach (var character in listReference)
        {
            activeEventCharacters.Remove(character);
        }

        StartCoroutine(IncrementStep());
    }

    IEnumerator IncrementStep()
    {
        isWaiting = true;

        stepCountDisplay++;
        stepCountStatic++;

        yield return new WaitForSeconds(stepTimeSeconds);

        isWaiting = false;
    }
}
