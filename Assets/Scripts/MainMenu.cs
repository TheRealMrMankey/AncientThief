using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

#region Buttons
    public void Play()
    {
        SceneManager.LoadSceneAsync("Tutorial Level");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
#endregion

}
