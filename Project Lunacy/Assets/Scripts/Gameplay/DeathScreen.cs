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

    public void OpenDeathScreen(int distanceLeft)
    {
        deathMenu.SetActive(true);
        distanceText.text = distanceText.text.Replace("<Dist>", distanceLeft.ToString());
        distanceText.text = distanceText.text.Replace("<Step>", (JourneyScript.stepCountStatic - 1).ToString());
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
