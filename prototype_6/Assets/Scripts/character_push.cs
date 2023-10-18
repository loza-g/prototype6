using ns;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class character_push : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude;
    public bool OnFire;

    private Vector2 moveInput;
    [SerializeField] private float moveSpeed = 0.01f;

    [SerializeField] private GameObject fireFXGO;
    [SerializeField] private GameObject firePrefab;

    private PlayerControls controls;

    private PushableBlock collidedPushableBlock;
    private Vector3 pushDirection;

    [SerializeField] private LayerMask grassDetectLayer;

    private CircleWipeController circleWipeController;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.HorizontalMove.performed += ctx => moveInput.x = ctx.ReadValue<float>();
        controls.Gameplay.HorizontalMove.canceled += ctx => moveInput.x = 0;
        controls.Gameplay.VerticalMove.performed += ctx => moveInput.y = ctx.ReadValue<float>();
        controls.Gameplay.VerticalMove.canceled += ctx => moveInput.y = 0;
        controls.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Gameplay.Push.performed += OnPushButtonPressed;
        controls.Gameplay.Restart.performed += ctx => GameManager.Instance.RestartCurrentScene();// circleWipeController.FadeOut(() => GameManager.Instance.RestartCurrentScene());

        circleWipeController = FindObjectOfType<CircleWipeController>();
    }

    private void OnPushButtonPressed(InputAction.CallbackContext context)
    {
        if (collidedPushableBlock != null && pushDirection != Vector3.zero)
        {
            //Vector3 moveDirection = collidedPushableBlock.gameObject.transform.position - transform.position;
            //moveDirection.y = 0;
            //moveDirection.Normalize();

            collidedPushableBlock.Move(pushDirection);

            collidedPushableBlock.CloseAllArrowIndicators();
        }

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void DisableInput()
    {
        controls.Disable();
    }

    public void EnableInput()
    {
        controls.Enable();
    }

    void Start()
    {
        OnFire = false;
    }

    void Update()
    {
        Vector3 input = -new Vector3(moveInput.x, 0, moveInput.y).normalized;

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        var skewedInput = matrix.MultiplyPoint3x4(input);
        

        transform.Translate(skewedInput * moveSpeed * Time.deltaTime);
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.Translate(0.0f, 0f, -0.01f); 
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.Translate(0.0f, 0f, 0.01f);
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Translate(0.01f, 0f, 0f);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Translate(-0.01f, 0f, 0f);
        //}


    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("grass"))
        {
            Debug.Log("collision with moveable shadow box");
            if (collidedPushableBlock != null)
            {
                collidedPushableBlock.GetComponentInChildren<Outline>().enabled = false;
                collidedPushableBlock.CloseAllArrowIndicators();
            }
            collidedPushableBlock = collision.gameObject.GetComponent<PushableBlock>();
            collidedPushableBlock.GetComponentInChildren<Outline>().enabled = true;

            SetPushDirection(collision);
            collidedPushableBlock.UpdateArrowIndicator(pushDirection);
            //rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }


        if (collision.gameObject.CompareTag("grass") && OnFire)
        {
            //destroy block
            //Destroy(collision.gameObject);
            fireFXGO.SetActive(false);
            OnFire = false;
            StartCoroutine(GenerateFireAndDestroyColliderAfterDelay(collision.collider.gameObject, igniteDelayTime+=0.35f));
            IgniteAdjacentGrassBlocks(collision.collider);

        }
    }

    

    private void SetPushDirection(Collision collision)
    {
        pushDirection = -collision.GetContact(0).normal;
        if (Mathf.Abs(pushDirection.x) < 1 && Mathf.Abs(pushDirection.z) < 1)
            pushDirection = Vector3.zero;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("grass")) 
        {

            SetPushDirection(collision);
            if (collidedPushableBlock != null)
                collidedPushableBlock.UpdateArrowIndicator(pushDirection);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("grass"))
        {
            if (collidedPushableBlock != null)
            {
                collidedPushableBlock.GetComponentInChildren<Outline>().enabled = false;
                collidedPushableBlock.CloseAllArrowIndicators();
                collidedPushableBlock = null;
            }


        }
            
    }

    float destroyTime = 2f;
    float igniteDelayTime = 0;
    RaycastHit hit;
    List<Collider> visitedGrassColliders = new List<Collider>();
    private void IgniteAdjacentGrassBlocks(Collider grassCollider)
    {
        //Collider[] colliders = Physics.OverlapSphere(position, 1f, grassDetectLayer); 
        List<Collider> colliders = new List<Collider>();

        if(Physics.Raycast(grassCollider.transform.position, Vector3.right, out hit, 1, grassDetectLayer))
        {
            if (!visitedGrassColliders.Contains(hit.collider))
            {
                colliders.Add(hit.collider);
                visitedGrassColliders.Add(hit.collider);
            }
        }
        if (Physics.Raycast(grassCollider.transform.position, Vector3.left, out hit, 1, grassDetectLayer))
        {
            if (!visitedGrassColliders.Contains(hit.collider))
            {
                colliders.Add(hit.collider);
                visitedGrassColliders.Add(hit.collider);
            }
        }
        if (Physics.Raycast(grassCollider.transform.position, Vector3.forward, out hit, 1, grassDetectLayer))
        {
            if (!visitedGrassColliders.Contains(hit.collider))
            {
                colliders.Add(hit.collider);
                visitedGrassColliders.Add(hit.collider);
            }
        }
        if (Physics.Raycast(grassCollider.transform.position, Vector3.back, out hit, 1, grassDetectLayer))
        {
            if (!visitedGrassColliders.Contains(hit.collider))
            {
                colliders.Add(hit.collider);
                visitedGrassColliders.Add(hit.collider);
            }
        }
        if (Physics.Raycast(grassCollider.transform.position, Vector3.up, out hit, 1, grassDetectLayer))
        {
            if (!visitedGrassColliders.Contains(hit.collider))
            {
                colliders.Add(hit.collider);
                visitedGrassColliders.Add(hit.collider);
            }
        }
        if (Physics.Raycast(grassCollider.transform.position, Vector3.down, out hit, 1, grassDetectLayer))
        {
            if (!visitedGrassColliders.Contains(hit.collider))
            {
                colliders.Add(hit.collider);
                visitedGrassColliders.Add(hit.collider);
            }
        }

        if (colliders.Count == 0) { igniteDelayTime = 0; return; }

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("grass"))
            {
                
                StartCoroutine(GenerateFireAndDestroyColliderAfterDelay(collider.gameObject, igniteDelayTime+= 0.35f)); // Destroy the collider object after 3 seconds
                
                IgniteAdjacentGrassBlocks(collider);
            }
        }
    }

    private IEnumerator GenerateFireAndDestroyColliderAfterDelay(GameObject colliderObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(firePrefab, colliderObject.transform);

        yield return new WaitForSeconds(destroyTime + delay);
        Destroy(colliderObject);
    }

    public void SetPlayerOnFire()
    {
        OnFire = true;
        fireFXGO.SetActive(true);
    }

  
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    Rigidbody rigidbody = hit.collider.attachedRigidbody;
    //    if(rigidbody != null)
    //    {
    //        if (hit.gameObject.CompareTag("shadow"))
    //        {
    //            Debug.Log("collision with moveable shadow box");
    //            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
    //            forceDirection.y = 0;
    //            forceDirection.Normalize();

    //            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
    //        }
    //        if (!hit.gameObject.CompareTag("shadow"))
    //        {
    //            Debug.Log("collision with static box, cannot move");
    //        }
    //        if (hit.gameObject.CompareTag("flag"))
    //        {
    //            Debug.Log("Captured Flag!!");
    //        }
    //        if (hit.gameObject.CompareTag("fire"))
    //        {
    //            Debug.Log("Collision with fire");
    //            OnFire = true;
    //        }
    //        if(hit.gameObject.CompareTag("grass") && OnFire)
    //        {
    //            //destroy block
    //        }
    //    }
    //}
}
