using MoreMountains.Feedbacks;
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
		[SerializeField] public LayerMask boundaryLayer;

		[SerializeField] private float pushSpeed = 1;

        [SerializeField] private GameObject[] arrowIndicatorArr;

        [SerializeField] private MMF_Player pushingFeedbacks; 
        public void Move(Vector3 direction)
		{
			if (Physics.Raycast(transform.position, direction, 1, boundaryLayer)) return;

            StartCoroutine(MoveToTarget(transform.position + direction));

            pushingFeedbacks?.PlayFeedbacks();
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

        public bool UpdateArrowIndicator(Vector3 pushDirection)
        {
            if (Physics.Raycast(transform.position, pushDirection, 1, boundaryLayer)) return false;

           
            CloseAllArrowIndicators();

            if (pushDirection == Vector3.left) {
                arrowIndicatorArr[0].SetActive(true);
                gameObject.GetComponentInChildren<Outline>().enabled = true;
            }
            else if (pushDirection == Vector3.right) {
                arrowIndicatorArr[1].SetActive(true);
                gameObject.GetComponentInChildren<Outline>().enabled = true;
            }
            else if (pushDirection == Vector3.back) {
                arrowIndicatorArr[2].SetActive(true);
                gameObject.GetComponentInChildren<Outline>().enabled = true;
            }
            else if (pushDirection == Vector3.forward) {
                arrowIndicatorArr[3].SetActive(true);
                gameObject.GetComponentInChildren<Outline>().enabled = true;
            }

            return true;
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
