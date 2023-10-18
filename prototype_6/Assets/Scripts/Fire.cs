using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class Fire : MonoBehaviour
	{
        [SerializeField] private GameObject fireFXGO;
        [SerializeField] private AudioSource audioPlayer;
   private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (fireFXGO.activeInHierarchy)
                {
                    audioPlayer.Play();
                }
                fireFXGO.SetActive(false);
                other.GetComponentInParent<character_push>().SetPlayerOnFire();
               
            }
        }
    }
}
