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

        private void Start()
        {
            playerPushScript = FindObjectOfType<character_push>();
            playerPushScript.OnMoveFinishedHandler += CheckPlayerAndSetArrowIndicator;
            CheckPlayerAndSetArrowIndicator();
        }

        private void OnDestroy()
        {
            playerPushScript.OnMoveFinishedHandler -= CheckPlayerAndSetArrowIndicator;
        }

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

        public void CheckPlayerAndSetArrowIndicator()
        {
            if (Physics.Raycast(transform.position, Vector3.right, out hit, 1, LayerMask.NameToLayer("Player")))
            {
                SetArrowIndicatorBasedOnPlayerPos();
                Debug.Log("?????");
            }
            if (Physics.Raycast(transform.position, Vector3.left, out hit, 1, LayerMask.NameToLayer("Player")))
            {
                SetArrowIndicatorBasedOnPlayerPos();
                Debug.Log("111111");
            }
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, 1, LayerMask.NameToLayer("Player")))
            {
                SetArrowIndicatorBasedOnPlayerPos();
                Debug.Log("2222222");
            }
            if (Physics.Raycast(transform.position, Vector3.back, out hit, 1, LayerMask.NameToLayer("Player")))
            {
                SetArrowIndicatorBasedOnPlayerPos();
                Debug.Log("333333333");
            }
        }

        private void SetArrowIndicatorBasedOnPlayerPos()
        {
            Vector3 playerPushDirection = transform.position - hit.transform.position;
            playerPushDirection.y = 0;
            Debug.Log(playerPushDirection);
            UpdateArrowIndicator(playerPushDirection);
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
