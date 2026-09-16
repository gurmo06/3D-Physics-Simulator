using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PushForceManager : MonoBehaviour
{
    // References to input field, button, text field, and player push script
    public TMP_InputField pushInputField;
    public Button pushButton;
    public TMP_Text pushText;
    public BasicRigidBodyPush rbp;
    public float pushForce = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that the references are set in the Unity Editor
        if (pushInputField == null)
        {
            Debug.LogError("Gravity Input Field reference is not set!");
            return;
        }

        if (pushButton == null)
        {
            Debug.LogError("Gravity Button reference is not set!");
            return;
        }

        if (pushText == null)
        {
            Debug.LogError("Gravity Text Field reference is not set!");
            return;
        }

        pushButton.onClick.AddListener(UpdatePlayerForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatePlayerForce()
    {
        // Retrieve the gravity value from the input field and convert it to a float
        if (float.TryParse(pushInputField.text, out pushForce))
        {
            // Apply the new push force value to the player
            rbp.strength = pushForce;

            // Change the value of gravity in the text
            pushText.text = "Player Push Force: " + pushForce + " daN";
        }
        else
        {
            Debug.LogWarning("Invalid player push force constant entered!");
        }
    }
}
