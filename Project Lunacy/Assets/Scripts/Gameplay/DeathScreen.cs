using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private Text distanceText;

    void Start()
    {
        deathMenu.SetActive(false);
    }

    public void OpenDeathScreen()
    {
        deathMenu.SetActive(true);
        distanceText.text = distanceText.text.Replace("<Step>", (JourneyScript.stepCountStatic - 1).ToString());

        if (JourneyScript.dayCountStatic > 1)
        {
            distanceText.text = distanceText.text.Replace("<Days>", (JourneyScript.dayCountStatic).ToString());
        }

        else
        {
            distanceText.text = distanceText.text.Replace("were <Days> days", "was " + (JourneyScript.dayCountStatic) + " day");
        }
    }

    public void RestartPlaythrough()
    {
        Debug.Log("Restarting Playthough!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NewPlaythrough()
    {
        SceneManager.LoadScene("1-StartSettings");
    }
}
