using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallMassManager : MonoBehaviour
{
    public TMP_InputField massInputField; // Reference to the input field for ball bass
    public Button massButton; // Reference to the button for the mass
    public TMP_Text massText; // Reference to the output text
    private Rigidbody rb; // Rigidbody reference
    public float mass = 1; // Mass variable

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that the references are set in the Unity Editor
        if (massInputField == null)
        {
            Debug.LogError("Mass Input Field reference is not set!");
            return;
        }

        if (massButton == null)
        {
            Debug.LogError("Mass Button reference is not set!");
            return;
        }

        if (massText == null)
        {
            Debug.LogError("Mass Text Field reference is not set!");
            return;
        }

        rb = GetComponent<Rigidbody>();
        massButton.onClick.AddListener(ChangeMass);
    }

    void ChangeMass()
    {
        if (float.TryParse(massInputField.text, out mass))
        {
            // Change the current ball mass for each ball and the output text
            rb.mass = mass;
            massText.text = "Sphere Mass: " + mass + " kg";
        }
    }    
}
