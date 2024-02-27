using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JourneyLogic : JourneyScript
{
    [SerializeField] private uint stepCountDisplay;

    //STATIC VARIABLES:
    [SerializeField] private List<CharacterBars> activeCharacters = new List<CharacterBars>();
    public List<CharacterBars> activeEventCharacters = new();
    
    [SerializeField] private float stepTimeSeconds = 1f;

    [Header("Event UI References")]
    [SerializeField] private GameObject eventPanel;
    [SerializeField] private Text eventNameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private List<Button> choiceButtons = new();

    public List<JourneyEvent> activeEvents = new();
    
    private List<JourneyEvent> activeSoloEvents = new();
    private List<JourneyEvent> activeDuoEvents = new();
    private List<JourneyEvent> activeTrioEvents = new();
    private List<JourneyEvent> activeQuadrioEvents = new();
    private List<JourneyEvent> activeWholePartyEvents = new();

    private bool isWaiting = false;
    private bool eventIsChosen = false;
    private bool isOccurrenceEvent = false;

    void Awake()
    {
        eventPanel.SetActive(false);
        
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

            eventPanel.SetActive(true);

            JourneyEvent chosenEvent = GenerateRandomEvent();

            DisplayEvent(chosenEvent);

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

    private void DisplayEvent(JourneyEvent journeyEvent)
    {
        eventNameText.text = journeyEvent.displayName;
        descriptionText.text = journeyEvent.description;

        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }

        for (int i = 0; i < journeyEvent.choiceList.Count; i++)
        {
            GameObject activeButton = choiceButtons[i].gameObject;
            ChoiceData choiceData = journeyEvent.choiceList[i];

            activeButton.SetActive(true);
            activeButton.GetComponentInChildren<Text>().text = choiceData.choiceName;

            activeButton.GetComponent<ChoiceButton>().SetButtonChoiceData(choiceData);
        }

        if (journeyEvent.choiceList.Count == 1 && journeyEvent.choiceList[0].results.Count == 1) //If there's only one option and result, do the following...
        {
            isOccurrenceEvent = true;
            Debug.Log(journeyEvent + " is detected as an Occurrence Event. If this is wrong, fix immediately!");
        }

        else
        {
            isOccurrenceEvent = false;
        }
    }

    private List<CharacterBars> AssignCharactersToEvent(JourneyEvent.EventSize givenEventSize)
    {
        switch(givenEventSize)
        {
            case JourneyEvent.EventSize.Solo:
                return ReturnRandomPartyList(1);

            case JourneyEvent.EventSize.Duo:
                return ReturnRandomPartyList(2);

            case JourneyEvent.EventSize.Trio:
                return ReturnRandomPartyList(3);

            case JourneyEvent.EventSize.Quadrio:
                return ReturnRandomPartyList(4);

            case JourneyEvent.EventSize.WholeParty:
                return ReturnRandomPartyList(99);

            default:
                Debug.LogError("Invalid Input for AssignCharactersToEvent. Debug immediately!");
                return null;
        }
    }

    private List<CharacterBars> ReturnRandomPartyList(int amountOfPartyMembers)
    {
        if (activeCharacters.Count < amountOfPartyMembers)
        {
            Debug.LogError("Chosen Event Size too large for current party! Debug immediately!");
        }
        
        List<CharacterBars> availableCharacters = activeCharacters;
        List<CharacterBars> eventCharacters = new();
        
        for (int i = 0; i < amountOfPartyMembers; i++)
        {
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

        eventNameText.text = result.resultName;
        descriptionText.text = result.resultDescription;

        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }

        choiceButtons[0].gameObject.SetActive(true);

        choiceButtons[0].GetComponentInChildren<Text>().text = result.resultChoiceText;
        choiceButtons[0].GetComponent<ChoiceButton>().MakeCloseButton();

        ApplyEventEffect.ApplyAllEffects(result, activeEventCharacters);
    }

    public void CloseEventTab()
    {
        eventPanel.SetActive(false);

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
