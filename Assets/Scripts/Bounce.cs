using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bounce : MonoBehaviour
{
    // References to outside Input Field and Button and Text Outputs
    public TMP_InputField elastcityInputField;
    public Button elasticityButton;
    public TMP_Text elasticityText;
    public float elasticityConstant;
    private Rigidbody rb; // Reference to Ball's rigidbody

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that the references are set in the Unity Editor
        if (elastcityInputField == null)
        {
            Debug.LogError("Elasticity Input Field reference is not set!");
            return;
        }

        if (elasticityButton == null)
        {
            Debug.LogError("Elasticity Button reference is not set!");
            return;
        }

        if (elasticityText == null)
        {
            Debug.LogError("Elasticity Text Field reference is not set!");
            return;
        }

        // Get the Rigidbody component attached to the object
        rb = GetComponent<Rigidbody>();

        elasticityButton.onClick.AddListener(UpdateConstant);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a surface and time isn't being stopped
        if (collision.collider.CompareTag("Surface") && !Input.GetKey(KeyCode.T))
        {
            // Calculate the direction of the bounce using the collision normal
            Vector3 bounceDirection = Vector3.Reflect(rb.linearVelocity, collision.GetContact(0).normal);

            // Apply a force to the object in the bounce direction
            rb.AddForce(bounceDirection * elasticityConstant * rb.mass, ForceMode.Impulse);
        }

        // Check if the collision is with a shpere
        else if (collision.collider.CompareTag("Sphere"))
        {
            // Calculate the direction of the bounce using the collision normal
            Vector3 bounceDirection = Vector3.Reflect(rb.linearVelocity, collision.GetContact(0).normal);

            // Apply a force to the object in the bounce direction
            rb.AddForce(bounceDirection * elasticityConstant * .1f * rb.mass, ForceMode.Impulse);
        }
    }

    void UpdateConstant()
    {
        // Get the elasticity constant from the input field and convert it to a float
        if (float.TryParse(elastcityInputField.text, out float elasticity))
        {
            // Change the current elasticity
            elasticityConstant = elasticity + 1f;
            elasticityText.text = "Sphere Elasticity: " + elasticity + "*Velocity";
        }
        else
        {
            Debug.LogWarning("Invalid elasticity constant entered!");
        }
    }
}