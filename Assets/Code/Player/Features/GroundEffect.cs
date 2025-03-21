using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEffect : MonoBehaviour
{

    #region Variables
    private Rigidbody rb;
    public float maxGroundDistance = 3;
    public LayerMask groundLayer;
    public float maxSpeed;
    public float liftForce;
    #endregion

    #region Builtin_Functions
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb)
        {
            HandleGroundEffect();
        }

    }

    #endregion

    #region Custom_Functions

    protected virtual void HandleGroundEffect()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position , Vector3.down , out hit , groundLayer)) 
        {
            if(hit.distance < maxGroundDistance) 
            {
                float currentSpeed = rb.linearVelocity.magnitude;
                float normalizedSpeed = currentSpeed / maxSpeed;
                normalizedSpeed = Mathf.Clamp01 (normalizedSpeed);

                float distance = maxGroundDistance - hit.distance;
                float finalForce = liftForce * distance * normalizedSpeed;
                rb.AddForce(Vector3.up * finalForce);
            }
        }
    }

    #endregion
}
