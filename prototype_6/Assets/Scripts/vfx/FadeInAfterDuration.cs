using System.Collections;
using UnityEngine;

public class FadeInAfterDuration : MonoBehaviour
{
    /// <summary>
    /// The circle wipe controller instance to be controlled. Will
    /// automatically pick up one attached to the same GameObject
    /// as this script.s
    /// </summary>
    [Tooltip("The circle wipe controller instance to be controlled. Will automatically pick up one attached to the same GameObject as this script.")]
    public CircleWipeController circleWipe;

    public float waitTime = 5f;

    private character_push character_Push;

    private void Awake()
    {
        character_Push = FindObjectOfType<character_push>();
    }
    IEnumerator Start()
    {
        character_Push.DisableInput();

        if (circleWipe == null)
        {
            circleWipe = GetComponent<CircleWipeController>();
        }

        if (circleWipe == null)
        {
            Debug.LogError("AnywhereClick requires a CircleWipeController to be set");
        }

        yield return new WaitForSeconds(waitTime);
        character_Push.EnableInput();
        circleWipe.FadeIn();
    }
}
