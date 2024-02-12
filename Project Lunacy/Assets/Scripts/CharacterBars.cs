using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RandomEffect();
        }

        vitalityText.text = "Vitality: " + vitalityValue;
        willpowerText.text = "WIllpower: " + willpowerValue;
        sanityText.text = "Sanity: " + sanityValue;
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
        string change;
        
        switch(type)
        {
            case ConsitutionType.Vitality:
                change = (value > 0) ? "Vitality Increased" : "Vitality Decreased";
                Debug.Log(change);
                
                vitalityValue = FindNewValue(vitalityValue, value);
                break;

            case ConsitutionType.Willpower:
                change = (value > 0) ? "Willpower Increased" : "Willpower Decreased";
                Debug.Log(change);

                willpowerValue = FindNewValue(willpowerValue, value);
                break;

            case ConsitutionType.Sanity:
                change = (value > 0) ? "Sanity Increased" : "Sanity Decreased";
                Debug.Log(change);

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
}