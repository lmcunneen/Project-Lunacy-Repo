using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JourneyLogic : JourneyScript
{
    //STATIC VARIABLES:
    [SerializeField] private List<CharacterBars> activeCharacters = new List<CharacterBars>();
    [SerializeField] private uint stepCountDisplay;
    
    [SerializeField] private float stepTimeSeconds = 1f;

    [Header("Event UI References")]
    [SerializeField] private GameObject eventPanel;
    [SerializeField] private Text eventNameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private List<Button> choiceButtons = new();

    public List<JourneyEvent> activeEvents = new();
    private bool isWaiting = false;
    private bool eventIsChosen = false;

    void Awake()
    {
        eventPanel.SetActive(false);
        
        foreach (var character in FindObjectsByType<CharacterBars>(FindObjectsSortMode.InstanceID))
        {
            activeCharacters.Add(character);
        }

        activeCharactersStatic = activeCharacters;

        Debug.Log(activeCharactersStatic.Count);
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
    }

    public void CloseEventTab(ChoiceData givenChoiceData)
    {
        eventPanel.SetActive(false);

        StartCoroutine(IncrementStep());

        //Will apply givenChoiceData here
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
