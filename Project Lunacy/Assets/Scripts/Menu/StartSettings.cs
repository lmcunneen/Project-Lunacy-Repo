using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSettings : MonoBehaviour
{
    [Header("Setting Variables")]
    [Range(1, 4)]
    [SerializeField] private int partySizeInt = 1;

    [Header("UI References")]
    [SerializeField] private Text partySizeDisplay;

    void Start()
    {
        partySizeDisplay.text = partySizeInt.ToString();
    }

    public void IncrementPartySize()
    {
        partySizeInt = Mathf.Clamp(partySizeInt + 1, 1, 4);
        partySizeDisplay.text = partySizeInt.ToString();
    }

    public void DecrementPartySize()
    {
        partySizeInt = Mathf.Clamp(partySizeInt - 1, 1, 4);
        partySizeDisplay.text = partySizeInt.ToString();
    }

    public void StartGameplay()
    {
        SettingsObject.SaveToSettingsObject((uint)partySizeInt);

        SceneManager.LoadScene("Gameplay");
    }
}
