using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionMenuManager : MonoBehaviour
{
    public static int currLevel;

    [SerializeField] private Transform allLevelParent;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("level"))
            PlayerPrefs.SetInt("level", 1);

        int currentLevel = PlayerPrefs.GetInt("level");
        for (int i = allLevelParent.childCount - 1; i >= currentLevel; i--)
        {
            allLevelParent.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }

    public void OnClickLevel(int levelNum) 
    {
        currLevel = levelNum;
        SceneManager.LoadScene(currLevel);
    }

    public void OnClickBack() 
    {
        SceneManager.LoadScene("home_screen");
        //this.gameObject.SetActive(false);
    }

}
