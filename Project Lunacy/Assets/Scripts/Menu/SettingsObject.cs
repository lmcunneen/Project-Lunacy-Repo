using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsObject : MonoBehaviour
{
    public static uint numberOfPartyMembers = 20;

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public static void SaveToSettingsObject(uint partySizeRef)
    {
        numberOfPartyMembers = partySizeRef;
    }
}
