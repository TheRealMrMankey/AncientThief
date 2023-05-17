using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

#region Variables
    public TMP_Text timeSecTxt;
    private int timeSec;
    public TMP_Text timeMinTxt;
    private int timeMin;
    public TMP_Text crystalsAmountTxt;
    public int crystalsAmount;
    public GameObject gate;
    public TMP_Text crystalsNeededTxt;
    public int crystalsNeeded;
    public int currentLvl;
    #endregion

#region Start and Update
    void Start()
    {
        timeSec = 0;
        timeMin = 0;
        crystalsAmount = 0;

        gate.SetActive(true);

        StartCoroutine(time());
    }

    void Update()
    {
        // If the player gets all the coins, open the exit gate
        if(crystalsAmount == crystalsNeeded) 
            gate.SetActive(false);
        
        // Set the ui texts values
        timeSecTxt.SetText(timeSec.ToString());
        timeMinTxt.SetText(timeMin.ToString());
        crystalsAmountTxt.SetText(crystalsAmount.ToString());
        crystalsNeededTxt.SetText(crystalsNeeded.ToString());

        // 60 seconds -> 1 minute
        if (timeSec == 60)
        {
            timeMin ++;
            timeSec = 0;
        }
    }
    #endregion

#region Timer
    // Count seconds
    private IEnumerator time()
    {
        while (true)
        {
            timeSec++;
            yield return new WaitForSeconds(1);
        }
    }
    #endregion

#region Scene Managment
    // Load next level or main menu if there are no more levels
    public void LoadNextLevel()
    {
        currentLvl++;
        if (Application.CanStreamedLevelBeLoaded("Level" + currentLvl))
            SceneManager.LoadSceneAsync("Level" + currentLvl);
        else
            SceneManager.LoadSceneAsync("Menu");
    }
#endregion

}
