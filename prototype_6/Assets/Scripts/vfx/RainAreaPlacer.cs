using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class RainAreaPlacer : MonoBehaviour
	{
		[SerializeField] private Transform rainAreaTrans;
		private RaycastHit hit;
		[SerializeField] private LayerMask rayCastLayers;
        [SerializeField] private Material grassOriginalMaterial;
        [SerializeField] private Material grassWetMaterial;

        private void Update()
        {
            if(Physics.Raycast(transform.position, Vector3.down, out hit, 4, rayCastLayers))
			{
				rainAreaTrans.position = hit.point;
			}
        }

        private void OnTriggerEnter(Collider other)
        {
			if (other.CompareTag("grass"))
			{
				other.GetComponent<Renderer>().material = grassWetMaterial;
			}
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("grass"))
            {
                other.GetComponent<Renderer>().material = grassOriginalMaterial;
            }
        }
    }
}
