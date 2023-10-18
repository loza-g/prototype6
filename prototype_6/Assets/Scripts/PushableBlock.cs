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

        [SerializeField] private GameObject[] arrowIndicatorArr;
        public void Move(Vector3 direction)
		{
			if (Physics.Raycast(transform.position, direction, 1, boundaryLayer)) return;

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

        public void UpdateArrowIndicator(Vector3 pushDirection)
        {
            if (Physics.Raycast(transform.position, pushDirection, 1, boundaryLayer)) return;

           
            CloseAllArrowIndicators();

            if (pushDirection == Vector3.left)
                arrowIndicatorArr[0].SetActive(true);
            else if (pushDirection == Vector3.right)
                arrowIndicatorArr[1].SetActive(true);
            else if (pushDirection == Vector3.back)
                arrowIndicatorArr[2].SetActive(true);
            else if (pushDirection == Vector3.forward)
                arrowIndicatorArr[3].SetActive(true);
            
        }

        public void CloseAllArrowIndicators()
        {
            for (int i = 0; i < arrowIndicatorArr.Length; i++)
            {
                arrowIndicatorArr[i].SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.forward);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.back);
        }
    }
}
