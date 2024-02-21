using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private ChoiceData loadedChoiceData;
    private JourneyLogic journeyLogic;

    private bool isCloseButton = false;

    private void Start()
    {
        journeyLogic = FindObjectOfType<JourneyLogic>();
    }

    public void SetButtonChoiceData(ChoiceData data)
    {
        loadedChoiceData = data;
    }

    public void OnClick()
    {
        if (!isCloseButton)
        {
            journeyLogic.ChoiceResultEventTab(loadedChoiceData);
        }
        

        else
        {
            isCloseButton = false;
            journeyLogic.CloseEventTab();
        }
    }

    public void MakeCloseButton()
    {
        isCloseButton = true;
    }
}
