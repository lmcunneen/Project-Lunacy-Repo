using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBars : JourneyScript
{
    private int vitalityValue = 100;
    private int willpowerValue = 100;
    private int sanityValue = 100;

    [SerializeField] private Text nameText;
    [SerializeField] private Text vitalityText;
    [SerializeField] private Text willpowerText;
    [SerializeField] private Text sanityText;

    private Color vitalityColour;
    private Color willpowerColour;
    private Color sanityColour;

    private uint currentStepCount;

    private string characterName;
    private JSONExport jsonComponent;
    private JourneyLogic journeyLogicComponent;

    void Start()
    {
        vitalityColour = vitalityText.color;
        willpowerColour = willpowerText.color;
        sanityColour = sanityText.color;

        jsonComponent = new();

        journeyLogicComponent = FindObjectOfType<JourneyLogic>();
    }

    void Update()
    {
        ChangeBarColours();

        vitalityText.text = "Vitality: " + vitalityValue;
        willpowerText.text = "Willpower: " + willpowerValue;
        sanityText.text = "Sanity: " + sanityValue;

        if (vitalityValue <= 0)
        {
            Debug.Log(gameObject.name + " lasted " + stepCountStatic + " steps");
            
            RemoveFromActiveCharacters();

            jsonComponent.CommitToFile();

            Destroy(gameObject);
        }
    }

    private void ChangeBarColours()
    {
        if (stepCountStatic >= currentStepCount + 1) //Resets text colour after one step
        {
            //RandomEffect();
            
            vitalityText.color = vitalityColour;
            willpowerText.color = willpowerColour;
            sanityText.color = sanityColour;

            jsonComponent.UpdateData(stepCountStatic, (uint)vitalityValue, (uint)willpowerValue, (uint)sanityValue);
        }
    }

    private void RandomEffect() //Used for randomly simulating outcomes
    {
        EffectType randomType = (EffectType)Random.Range(0, 3);
        int multiplier = Random.Range(0, 2) * 2 - 1; //Random positive or negative
        int randomValue = Random.Range(1, 4) * 15 * multiplier;

        SetConstitutionValue(randomType, randomValue);
    }

    public int GetConstitutionValue(EffectType type)
    {
        switch(type)
        {
            case EffectType.Vitality:
                return vitalityValue;

            case EffectType.Willpower:
                return willpowerValue;

            case EffectType.Sanity:
                return sanityValue;

            case EffectType.None:
                Debug.LogWarning("The 'None' enum value was passed into GetConsitutionType. Debug if possible");
                return 0;

            default:
                Debug.LogError("Invalid input for GetConstitutionType. Debug immediately!");
                return 0;
        }
    }

    public void SetConstitutionValue(EffectType type, int value)
    {
        Debug.Log("Con Set for " + gameObject.name + "!");
        
        switch(type)
        {
            case EffectType.Vitality:
                vitalityText.color = (value > 0) ? Color.white : Color.black;
                willpowerText.color = willpowerColour;
                sanityText.color = sanityColour;
                
                vitalityValue = FindNewValue(vitalityValue, value);
                break;

            case EffectType.Willpower:
                vitalityText.color = vitalityColour;
                willpowerText.color = (value > 0) ? Color.white : Color.black;
                sanityText.color = sanityColour;

                willpowerValue = FindNewValue(willpowerValue, value);
                break;

            case EffectType.Sanity:
                vitalityText.color = vitalityColour;
                willpowerText.color = willpowerColour;
                sanityText.color = (value > 0) ? Color.white : Color.black;

                sanityValue = FindNewValue(sanityValue, value);
                break;

            case EffectType.None: //Set nothing
                Debug.Log("Nothing was set, as 'None' was passed into SetConsitutionValue");
                break;

            default:
                Debug.LogError("Invalid input for SetConstitutionValue. Debug immediately!");
                break;
        }

        currentStepCount = stepCountStatic; //Aligns it with current
    }

    private int FindNewValue(int currentValue, int add)
    {
        int output = currentValue + add;

        if (output >= 100) 
            return 100;

        else if (output <= 0)
        {
            return 0;
        }

        return output;
    }

    public void SetName(string givenName)
    {
        characterName = givenName;
        gameObject.name = characterName + " (Character)";
        nameText.text = characterName;
    }

    private void RemoveFromActiveCharacters()
    {
        if (journeyLogicComponent.activeCharacters.Find(item => this) != null)
        {
            journeyLogicComponent.activeCharacters.Remove(this);
            return;
        }
        
        if (journeyLogicComponent.activeEventCharacters.Find(item => this) != null)
        {
            journeyLogicComponent.activeEventCharacters.Remove(this);
            return;
        }
    }
}
