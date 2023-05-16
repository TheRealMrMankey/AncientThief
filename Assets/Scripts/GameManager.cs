using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text timeSecTxt;
    private int timeSec;
    public TMP_Text timeMinTxt;
    private int timeMin;
    public TMP_Text coinsAmountTxt;
    public int coinsAmount;
    public GameObject gate;
    public TMP_Text coinsNeededTxt;
    public int coinsNeeded;
    public int currentLvl;

    void Start()
    {
        timeSec = 0;
        timeMin = 0;
        coinsAmount = 0;

        gate.SetActive(true);

        StartCoroutine(time());
    }

    void Update()
    {
        // If the player gets all the coins, open the exit gate
        if(coinsAmount == coinsNeeded) 
            gate.SetActive(false);
        
        // Set the ui texts values
        timeSecTxt.SetText(timeSec.ToString());
        timeMinTxt.SetText(timeMin.ToString());
        coinsAmountTxt.SetText(coinsAmount.ToString());
        coinsNeededTxt.SetText(coinsNeeded.ToString());

        // 60 seconds -> 1 minute
        if (timeSec == 60)
        {
            timeMin ++;
            timeSec = 0;
        }
    }

    // Count seconds
    private IEnumerator time()
    {
        while (true)
        {
            timeSec++;
            yield return new WaitForSeconds(1);
        }
    }

    // Load next level or main menu if there are no more levels
    public void LoadNextLevel()
    {
        currentLvl++;
        if (Application.CanStreamedLevelBeLoaded("Level" + currentLvl))
            SceneManager.LoadSceneAsync("Level" + currentLvl);
        else
            SceneManager.LoadSceneAsync("Menu");
    }
}
