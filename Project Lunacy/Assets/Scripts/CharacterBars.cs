using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBars : MonoBehaviour
{
    public enum ConsitutionType
    {
        Vitality,
        Willpower,
        Sanity
    }

    private int vitalityValue = 100;
    private int willpowerValue = 100;
    private int sanityValue = 100;

    [SerializeField] private Text vitalityText;
    [SerializeField] private Text willpowerText;
    [SerializeField] private Text sanityText;

    private Color vitalityColour;
    private Color willpowerColour;
    private Color sanityColour;

    private float stepTimeSeconds = 1;
    private bool isWaiting = false;

    void Start()
    {
        vitalityColour = vitalityText.color;
        willpowerColour = willpowerText.color;
        sanityColour = sanityText.color;
    }

    void Update()
    {
        if (!isWaiting)
        {
            StartCoroutine(AutoPlay());
        }

        vitalityText.text = "Vitality: " + vitalityValue;
        willpowerText.text = "Willpower: " + willpowerValue;
        sanityText.text = "Sanity: " + sanityValue;

        if (vitalityValue <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void RandomEffect()
    {
        ConsitutionType randomType = (ConsitutionType)Random.Range(0, 3);
        int multiplier = Random.Range(0, 2) * 2 - 1; //Random positive or negative
        int randomValue = Random.Range(0, 4) * 15 * multiplier;

        ChangeConstitutionValue(randomType, randomValue);
    }

    public void ChangeConstitutionValue(ConsitutionType type, int value)
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
                Debug.LogError("Invalid input for ChangeConstitutionValue. Debug immediately!");
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

    IEnumerator AutoPlay()
    {
        isWaiting = true;
        RandomEffect();

        yield return new WaitForSeconds(stepTimeSeconds);

        isWaiting = false;
    }
}
