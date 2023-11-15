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

        private void Update()
        {
            if(Physics.Raycast(transform.position, Vector3.down, out hit, 4, rayCastLayers))
			{
				rainAreaTrans.position = hit.point;
			}
        }
    }
}
