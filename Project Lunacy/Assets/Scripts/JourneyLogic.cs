using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JourneyLogic : MonoBehaviour
{
    public enum ConsitutionType
    {
        Vitality,
        Willpower,
        Sanity
    }

    //STATIC VARIABLES:
    public static List<CharacterBars> activeCharacters = new List<CharacterBars>();
    public static uint stepCountStatic;
    
    [SerializeField] private float stepTimeSeconds = 1f;
    private bool isWaiting = false;

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
    }

    IEnumerator AutoPlay()
    {
        isWaiting = true;

        stepCountStatic++;

        yield return new WaitForSeconds(stepTimeSeconds);

        isWaiting = false;
    }
}
