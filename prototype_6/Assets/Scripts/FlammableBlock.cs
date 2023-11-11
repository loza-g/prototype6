using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool wet = false;

    public bool IsWet()
    {
        return wet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("water"))
        {
            wet = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("water"))
        {
            wet = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("water"))
        {
            wet = false;
        }
    }
}
