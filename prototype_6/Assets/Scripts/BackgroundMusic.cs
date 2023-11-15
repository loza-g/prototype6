using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private void Awake()
    {
        // Make sure only one instance of this object exists
        int numberOfInstances = FindObjectsOfType<BackgroundMusic>().Length;
        if (numberOfInstances > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            // Don't destroy this object when loading a new scene
            DontDestroyOnLoad(gameObject);
        }
    }
}
