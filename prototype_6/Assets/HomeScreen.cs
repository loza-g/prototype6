using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level Selection");//load main menu
        Debug.Log("load scene");
    }
}
