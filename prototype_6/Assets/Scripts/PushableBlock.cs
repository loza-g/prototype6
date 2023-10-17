using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class PushableBlock : MonoBehaviour
	{
		[SerializeField] private LayerMask boundaryLayer;

		[SerializeField] private float pushSpeed = 1;


		public void Move(Vector3 direction)
		{
			if (Physics.Raycast(transform.position, direction, 1, boundaryLayer)) return;

			StartCoroutine(MoveToTarget(transform.position + direction));
            
        }

		private IEnumerator MoveToTarget(Vector3 target)
		{
			while ((target - transform.position).sqrMagnitude > 0.02f)
			{
				transform.position = Vector3.MoveTowards(transform.position, target, pushSpeed * Time.deltaTime);
				yield return null;
			}
			transform.position = target;
		}
    }
}
