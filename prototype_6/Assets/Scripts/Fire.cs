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
        private bool igniteOnce = false;
        private void OnTriggerEnter(Collider other)
        {
            if (igniteOnce) return;

            if (other.CompareTag("Player"))
            {
                fireFXGO.SetActive(false);
                other.GetComponentInParent<character_push>().SetPlayerOnFire();
                igniteOnce = true;
            }
        }
    }
}
