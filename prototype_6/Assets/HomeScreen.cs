using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
    public void PlayGame()
    {
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1);
        }
        SceneManager.LoadScene("Level Selection");//load main menu
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene("Level Selection");//load main menu
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
