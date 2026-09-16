using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AttractionManager : MonoBehaviour
{
    // References to the Input Fields, Text Fields, and Buttons
    public TMP_InputField attractionInputField;
    public Button attractionButton;
    public TMP_Text attractionText;
    public string targetTag; // Tag of the objects to apply gravitational force to
    public float attractionConstant = 0f; // Gravitational Constant

    // Start is called before the first frame update
    private void Start()
    {
        // Ensure that the references are set in the Unity Editor
        if (attractionInputField == null)
        {
            Debug.LogError("Attraction Input Field reference is not set!");
            return;
        }

        if (attractionButton == null)
        {
            Debug.LogError("Attraction Button reference is not set!");
            return;
        }

        // Add listener to the button
        attractionButton.onClick.AddListener(UpdateAttraction);
    }

    private void Update()
    {
        // Find all objects with the specified tag
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (attractionConstant != 0)
        {
            foreach (GameObject target in targets)
            {
                if (target != gameObject) // Ensure it's not the same object
                {
                    Rigidbody otherRigidbody = target.GetComponent<Rigidbody>();

                    if (otherRigidbody != null)
                    {
                        // Calculate the distance between the centers of the two objects
                        float distance = Vector3.Distance(transform.position, target.transform.position);

                        // Calculate the gravitational force magnitude
                        float forceMagnitude = attractionConstant * (otherRigidbody.mass * GetComponent<Rigidbody>().mass) / Mathf.Pow(distance, 2);

                        // Calculate the direction of the gravitational force
                        Vector3 forceDirection = (target.transform.position - transform.position).normalized;

                        // Apply the gravitational force to the object
                        otherRigidbody.AddForce(-forceDirection * forceMagnitude);
                    }
                }
            }
        }
    }

    void UpdateAttraction()
    {
        // Get the Attraction constant from the input field, convert it to a float, and update the attraction constant value
        if (float.TryParse(attractionInputField.text, out attractionConstant))
        {
            // Change the value of the Attraction in the text
            attractionText.text = "Attraction Constant: " + attractionConstant + " m³/kg/s²";
        }
        else
        {
            Debug.LogWarning("Invalid Attraction Constant entered!");
        }
    }
}

