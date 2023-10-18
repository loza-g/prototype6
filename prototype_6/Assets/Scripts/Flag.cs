using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class Flag : MonoBehaviour
	{
        private CircleWipeController circleWipeController;
        [SerializeField] private AudioSource audioPlayer;

        private void Start()
        {
            circleWipeController = FindObjectOfType<CircleWipeController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                audioPlayer.Play();
                other.GetComponentInParent<character_push>().DisableInput();
                circleWipeController.FadeOut(() => GameManager.Instance.LoadNextScene());
            }
        }
    }
}
