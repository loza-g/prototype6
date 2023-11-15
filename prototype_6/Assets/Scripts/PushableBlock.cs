using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

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

        private RaycastHit hit;

        private character_push playerPushScript;
        private bool canPush;

        private void Start()
        {
            playerPushScript = FindObjectOfType<character_push>();
            // playerPushScript.OnMoveFinishedHandler += CheckPlayerAndSetArrowIndicator;
            // CheckPlayerAndSetArrowIndicator();
            canPush = true;
        }

        private void OnDestroy()
        {
            // playerPushScript.OnMoveFinishedHandler -= CheckPlayerAndSetArrowIndicator;
        }

        public void Move(Vector3 direction)
		{
			if (Physics.Raycast(transform.position, direction, 1, boundaryLayer) || !canPush) return;
            canPush = false;
        [SerializeField] private MMF_Player pushingFeedbacks;

        private bool moving = false;
        public bool IsMoving() { return moving; }

        public void Move(Vector3 direction)
		{
			if (Physics.Raycast(transform.position, direction, 1, boundaryLayer)) return;

            moving = true;
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
            canPush = true;
            moving = false;
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
