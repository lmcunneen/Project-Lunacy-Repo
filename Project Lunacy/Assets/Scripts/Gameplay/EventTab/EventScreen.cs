using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScreen : MonoBehaviour
{
    [SerializeField] private GameObject eventPanel;
    [SerializeField] private Text eventNameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private List<Button> choiceButtons = new();

    void Start()
    {
        eventPanel.SetActive(false);
    }

    public void OpenEventTab()
    {
        eventPanel.SetActive(true);
    }
    
    public void DisplayEvent(JourneyEvent journeyEvent, List<CharacterBars> eventCharacters)
    {
        eventNameText.text = journeyEvent.displayName;
        descriptionText.text = ReplaceEventText.InsertCharactersInDescription(journeyEvent.description, eventCharacters);

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
            JourneyLogic.isOccurrenceEvent = true;
            Debug.Log(journeyEvent + " is detected as an Occurrence Event. If this is wrong, fix immediately!");
        }

        else
        {
            JourneyLogic.isOccurrenceEvent = false;
        }
    }

    public void DisplayChoiceResult(ChoiceResult givenResult)
    {
        eventNameText.text = givenResult.resultName;
        descriptionText.text = givenResult.resultDescription;

        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }

        choiceButtons[0].gameObject.SetActive(true);

        choiceButtons[0].GetComponentInChildren<Text>().text = givenResult.resultChoiceText;
        choiceButtons[0].GetComponent<ChoiceButton>().MakeCloseButton();
    }

    public void CloseEventTab()
    {
        eventPanel.SetActive(false);
    }
}
