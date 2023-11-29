using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionMenuManager : MonoBehaviour
{
    public static int currLevel;
    public void OnClickLevel(int levelNum) {
        currLevel = levelNum;
        SceneManager.LoadScene(currLevel-1);
    }

    public void OnClickBack() {
        SceneManager.LoadScene("home_screen");
        //this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
