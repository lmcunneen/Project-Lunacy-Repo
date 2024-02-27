using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JourneyLogic : JourneyScript
{
    [SerializeField] private uint stepCountDisplay;

    //STATIC VARIABLES:
    [SerializeField] private List<CharacterBars> activeCharacters = new List<CharacterBars>();
    
    [SerializeField] private float stepTimeSeconds = 1f;

    [Header("Event UI References")]
    [SerializeField] private GameObject eventPanel;
    [SerializeField] private Text eventNameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private List<Button> choiceButtons = new();

    public List<JourneyEvent> activeEvents = new();
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
            
            int randomIndex = Random.Range(0, activeEvents.Count);
            DisplayEvent(activeEvents[randomIndex]);

            StopAllCoroutines();
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

    public void ChoiceResultEventTab(ChoiceData givenChoiceData)
    {
        if (isOccurrenceEvent)
        {
            //Apply the effect here
        
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

        //Will apply result to characters here
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
