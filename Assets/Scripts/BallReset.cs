using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour
{
    // Initializers
    public Vector3 initialPosition;
    public Quaternion initialRotation;
    private Rigidbody rb; 
    private Vector3 tempVelocity;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial position and rotation when the script starts
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Get the reference to the Rigidbody component attached to the Ball
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if "R" is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Reset the velocities to zero
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Reset the position and rotation to their initial values
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }

        // Reset the position and rotation to their initial values if "F" is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }

        // Reset the velocities to zero if "E" is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Stop time if "T" is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!rb.isKinematic)
            {
                tempVelocity = rb.velocity;
                rb.isKinematic = true;
            }
            else if (rb.isKinematic)
            {
                rb.isKinematic = false;
                rb.velocity = tempVelocity;
            }
        }
    }
}