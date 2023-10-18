using ns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class character_push : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude;
    private bool OnFire;
    [SerializeField] private GameObject TinyFire;

    private float xInput, yInput;
    [SerializeField] private float moveSpeed = 0.01f;

    [SerializeField] private GameObject fireFXGO;

    private PlayerControls controls;

    private PushableBlock collidedPushableBlock;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.HorizontalMove.performed += ctx => xInput = ctx.ReadValue<float>();
        controls.Gameplay.HorizontalMove.canceled += ctx => xInput = 0;
        controls.Gameplay.VerticalMove.performed += ctx => yInput = ctx.ReadValue<float>();
        controls.Gameplay.VerticalMove.canceled += ctx => yInput = 0;
        controls.Gameplay.Push.performed += OnPushButtonPressed;
    }

    private void OnPushButtonPressed(InputAction.CallbackContext context)
    {
        if (collidedPushableBlock != null)
        {
            Vector3 moveDirection = collidedPushableBlock.gameObject.transform.position - transform.position;
            moveDirection.y = 0;
            moveDirection.Normalize();

            collidedPushableBlock.Move(moveDirection);
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

    void Start()
    {
        OnFire = false;
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(xInput, 0, yInput).normalized;

        transform.Translate(-moveDirection * moveSpeed * Time.deltaTime);
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
            if(collidedPushableBlock != null)
                collidedPushableBlock.GetComponentInChildren<Outline>().enabled = false;
            collidedPushableBlock = collision.gameObject.GetComponent<PushableBlock>();
            collidedPushableBlock.GetComponentInChildren<Outline>().enabled = true;

            //rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }

        Rigidbody rigidbody = collision.collider.attachedRigidbody;

        if (rigidbody != null)
        {
            
            if (!collision.gameObject.CompareTag("grass"))
            {
                Debug.Log("collision with static box, cannot move");
            }
            
            
            if (collision.gameObject.CompareTag("grass") && OnFire)
            {
                //destroy block
                //Destroy(collision.gameObject);
                IgniteAdjacentGrassBlocks(collision.gameObject.transform.position);

            }
        }
    }

    private void IgniteAdjacentGrassBlocks(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 1f); 
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("grass"))
            {
                // Ignite the adjacent grass block
                Debug.Log("ignited adjacent grass block with fire");
                GameObject fireAnimation = Instantiate(fireFXGO, collider.transform.position, Quaternion.identity);
                StartCoroutine(DestroyAfterDelay(fireAnimation, 2f));
                StartCoroutine(DestroyColliderAfterDelay(collider.gameObject, 2f)); // Destroy the collider object after 3 seconds
                StartCoroutine(DestroyAfterIgnition(2f));
                //Destroy(collider.gameObject);

            }
        }
    }

    private IEnumerator DestroyAfterIgnition(float delay)
    {
        yield return new WaitForSeconds(delay);
        OnFire = false;
        fireFXGO.SetActive(false);
    }
    private IEnumerator DestroyAfterDelay(GameObject objectToDestroy, float delay)
    {
        yield return new WaitForSeconds(delay);
        // Turn off the fire 
        var particleSystem = objectToDestroy.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Stop();
        }
        Destroy(objectToDestroy);
    }

    private IEnumerator DestroyColliderAfterDelay(GameObject colliderObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(colliderObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fire"))
        {
            if (TinyFire.activeInHierarchy)
            {
                Debug.Log("Collision with fire");
                OnFire = true;
                fireFXGO.SetActive(true);
                TinyFire.SetActive(false);
            }

        }
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
