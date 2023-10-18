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

			if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
			{
				direction = new Vector3(direction.x, 0, 0).normalized;
            }
            else
			{
                direction = new Vector3(0, 0, direction.z).normalized;

            }
            StartCoroutine(MoveToTarget(transform.position + direction));
            
        }

		private IEnumerator MoveToTarget(Vector3 target)
		{
			while ((target - transform.position).sqrMagnitude > 0.01f)
			{
				transform.position = Vector3.MoveTowards(transform.position, target, pushSpeed * Time.deltaTime);
				yield return null;
			}
			transform.position = target;
		}
    }
}
