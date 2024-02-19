using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JourneyLogic : MonoBehaviour
{
    public enum ConsitutionType
    {
        Vitality,
        Willpower,
        Sanity,
        None
    }

    //STATIC VARIABLES:
    public static List<CharacterBars> activeCharacters = new List<CharacterBars>();
    public static uint stepCountStatic;
    
    [SerializeField] private float stepTimeSeconds = 1f;

    [Header("Event UI References")]
    [SerializeField] private Text eventNameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private List<Button> choiceButtons = new();

    public List<JourneyEvent> activeEvents = new();
    private bool isWaiting = false;
    private bool eventIsChosen = false;

    void Start()
    {
        foreach (var character in FindObjectsByType<CharacterBars>(FindObjectsSortMode.InstanceID))
        {
            activeCharacters.Add(character);
        }
    }

    void Update()
    {
        if (!isWaiting)
        {
            StartCoroutine(AutoPlay());
        }

        if (stepCountStatic % 5 != 0)
        {
            eventIsChosen = false;
            return;
        }
        
        if (!eventIsChosen)
        {
            eventIsChosen = true;
            
            int randomIndex = Random.Range(0, activeEvents.Count);
            DisplayEvent(activeEvents[randomIndex]);
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

            activeButton.SetActive(true);
            activeButton.GetComponentInChildren<Text>().text = journeyEvent.choiceList[i].choiceName;
        }
    }

    IEnumerator AutoPlay()
    {
        isWaiting = true;

        stepCountStatic++;

        yield return new WaitForSeconds(stepTimeSeconds);

        isWaiting = false;
    }
}
