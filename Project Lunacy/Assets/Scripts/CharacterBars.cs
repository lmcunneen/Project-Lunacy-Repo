using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBars : JourneyLogic
{
    private int vitalityValue = 100;
    private int willpowerValue = 100;
    private int sanityValue = 100;

    [SerializeField] private Text vitalityText;
    [SerializeField] private Text willpowerText;
    [SerializeField] private Text sanityText;

    private Color vitalityColour;
    private Color willpowerColour;
    private Color sanityColour;

    private uint currentStepCount;

    void Start()
    {
        vitalityColour = vitalityText.color;
        willpowerColour = willpowerText.color;
        sanityColour = sanityText.color;
    }

    void Update()
    {
        if (CheckStepCount())
        {
            RandomEffect();
        }

        vitalityText.text = "Vitality: " + vitalityValue;
        willpowerText.text = "Willpower: " + willpowerValue;
        sanityText.text = "Sanity: " + sanityValue;

        if (vitalityValue <= 0)
        {
            Debug.Log(gameObject.name + " lasted " + stepCountStatic + " steps");

            Destroy(gameObject);
        }
    }

    private bool CheckStepCount()
    {
        if (stepCountStatic >= currentStepCount + 3)
        {
            currentStepCount = stepCountStatic; //Aligns it with current
            return true;
        }

        if (stepCountStatic >= currentStepCount + 1) //Resets text colour after one step
        {
            vitalityText.color = vitalityColour;
            willpowerText.color = willpowerColour;
            sanityText.color = sanityColour;
        }

        return false;
    }

    private void RandomEffect()
    {
        ConsitutionType randomType = (ConsitutionType)Random.Range(0, 3);
        int multiplier = Random.Range(0, 2) * 2 - 1; //Random positive or negative
        int randomValue = Random.Range(1, 4) * 15 * multiplier;

        SetConstitutionValue(randomType, randomValue);
    }

    public int GetConstitutionValue(ConsitutionType type)
    {
        switch(type)
        {
            case ConsitutionType.Vitality:
                return vitalityValue;

            case ConsitutionType.Willpower:
                return willpowerValue;

            case ConsitutionType.Sanity:
                return sanityValue;

            default:
                Debug.LogError("Invalid input for GetConstitutionType. Debug immediately!");
                return 0;
        }
    }

    public void SetConstitutionValue(ConsitutionType type, int value)
    {
        switch(type)
        {
            case ConsitutionType.Vitality:
                vitalityText.color = (value > 0) ? Color.white : Color.black;
                willpowerText.color = willpowerColour;
                sanityText.color = sanityColour;
                
                vitalityValue = FindNewValue(vitalityValue, value);
                break;

            case ConsitutionType.Willpower:
                vitalityText.color = vitalityColour;
                willpowerText.color = (value > 0) ? Color.white : Color.black;
                sanityText.color = sanityColour;

                willpowerValue = FindNewValue(willpowerValue, value);
                break;

            case ConsitutionType.Sanity:
                vitalityText.color = vitalityColour;
                willpowerText.color = willpowerColour;
                sanityText.color = (value > 0) ? Color.white : Color.black;

                sanityValue = FindNewValue(sanityValue, value);
                break;

            default:
                Debug.LogError("Invalid input for SetConstitutionValue. Debug immediately!");
                break;
        }
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
}
