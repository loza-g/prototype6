using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_push : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude;
    private bool OnFire;
    // Start is called before the first frame update
    void Start()
    {
        OnFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0.0f, 0f, -0.01f); 
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0.0f, 0f, 0.01f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(0.01f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(-0.01f, 0f, 0f);
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if(rigidbody != null)
        {
            if (hit.gameObject.CompareTag("shadow"))
            {
                Debug.Log("collision with moveable shadow box");
                Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
            }
            if (!hit.gameObject.CompareTag("shadow"))
            {
                Debug.Log("collision with static box, cannot move");
            }
            if (hit.gameObject.CompareTag("flag"))
            {
                Debug.Log("Captured Flag!!");
            }
            if (hit.gameObject.CompareTag("fire"))
            {
                Debug.Log("Collision with fire");
                OnFire = true;
            }
            if(hit.gameObject.CompareTag("grass") && OnFire)
            {
                //destroy block
            }
        }
    }
}
