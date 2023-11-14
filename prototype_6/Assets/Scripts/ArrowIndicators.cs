using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowIndicators : MonoBehaviour
{
    [SerializeField] private GameObject[] arrowIndicatorArr;
    [SerializeField] public LayerMask boundaryLayer;
    private RaycastHit hit;
    private character_push playerPushScript;

        private void Start() {
            playerPushScript = FindObjectOfType<character_push>();
        }

        private void Update()
        {
            if (!playerPushScript.isMoving) {
                UpdateArrowIndicator();
            } else {
                CloseAllArrowIndicators();
            }
        }

        // private void SetArrowIndicatorBasedOnPlayerPos()
        // {
        //     Vector3 playerPushDirection = transform.position - hit.transform.position;
        //     playerPushDirection.y = 0;
        //     Debug.Log(playerPushDirection);
        //     UpdateArrowIndicator();
        // }

        public void UpdateArrowIndicator()
        {
            if (!Physics.Raycast(transform.position, new Vector3(1f, 0f, 0f), out hit, 1, boundaryLayer)) {
                // Debug.Log(hit.collider.gameObject.name);
                arrowIndicatorArr[0].SetActive(true);
            }
            if (!Physics.Raycast(transform.position, new Vector3(-1f, 0f, 0f), out hit, 1, boundaryLayer)) {
                // Debug.Log(hit.collider.gameObject.name);
                arrowIndicatorArr[1].SetActive(true);
            }
            if (!Physics.Raycast(transform.position, new Vector3(0f, 0f, -1f), out hit, 1, boundaryLayer)) {
                // Debug.Log(hit.collider.gameObject.name);
                arrowIndicatorArr[2].SetActive(true);
            }
            if (!Physics.Raycast(transform.position, new Vector3(0f, 0f, 1f), out hit, 1, boundaryLayer)) {
                // Debug.Log(hit.collider.gameObject.name);
                arrowIndicatorArr[3].SetActive(true);
            }
        }

        public void CloseAllArrowIndicators()
        {
            for (int i = 0; i < arrowIndicatorArr.Length; i++)
            {
                arrowIndicatorArr[i].SetActive(false);
            }
        }
}
