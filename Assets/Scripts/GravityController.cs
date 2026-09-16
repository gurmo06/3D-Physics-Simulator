using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GravityController : MonoBehaviour
{
    public TMP_InputField gravityInputField; // Reference to the Input Field
    public Button gravityButton; // Reference to the Button
    public TMP_Text gravityText; // Reference to the output of the gravity constant
    public float gravityValue;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that the references are set in the Unity Editor
        if (gravityInputField == null)
        {
            Debug.LogError("Gravity Input Field reference is not set!");
            return;
        }

        if (gravityButton == null)
        {
            Debug.LogError("Gravity Button reference is not set!");
            return;
        }

        if (gravityText == null)
        {
            Debug.LogError("Gravity Text Field reference is not set!");
            return;
        }

        // Add listener to the button
        gravityButton.onClick.AddListener(ApplyGravity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ApplyGravity()
    {
        // Retrieve the gravity value from the input field and convert it to a float
        if (float.TryParse(gravityInputField.text, out gravityValue))
        {
            // Apply the gravity value to the physics system
            Physics.gravity = new Vector3(Physics.gravity.x, -gravityValue, Physics.gravity.z);

            // Change the value of gravity in the text
            gravityText.text = "Gravity Constant: " + gravityValue + " m/s²";
        }
        else
        {
            Debug.LogWarning("Invalid gravity value entered!");
        }
    }
}