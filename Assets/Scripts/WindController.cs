using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WindController : MonoBehaviour
{
    // Reference to the Input Fields and Buttons
    public TMP_InputField nSInputField;
    public TMP_InputField eWInputField;
    public Button nSButton;
    public Button eWButton;
    public TMP_Text windText;
    public float nS;
    public float eW;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that the references are set in the Unity Editor
        if (nSInputField == null)
        {
            Debug.LogError("North/South Input Field reference is not set!");
            return;
        }

        if (nSButton == null)
        {
            Debug.LogError("North/South Button reference is not set!");
            return;
        }

        if (eWInputField == null)
        {
            Debug.LogError("East/West Input Field is not set!");
            return;
        }

        if (eWButton == null)
        {
            Debug.LogError("East/West Button reference is not set!");
            return;
        }

        if (windText == null)
        {
            Debug.LogError("Wind Text Field reference is not set!");
            return;
        }

        // Add listeners to the buttons
        nSButton.onClick.AddListener(UpdateNSWind);
        eWButton.onClick.AddListener(UpdateEWWind);
    }

   void UpdateNSWind()
    {
        // Get the NS Wind constant from the input field and convert it to a float
        if (float.TryParse(nSInputField.text, out nS))
        {
            // Change the current NS Wind as a force of gravity
            Physics.gravity = new Vector3(nS, Physics.gravity.y, Physics.gravity.z);
            UpdateText();
        }
        else
        {
            Debug.LogWarning("Invalid NS Wind Constant entered!");
        }
    }

    void UpdateEWWind()
    {
        // Get the EW Wind constant from the input field and convert it to a float
        if (float.TryParse(eWInputField.text, out eW))
        {
            // Change the current EW Wind as a force of gravity
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, -eW);
            UpdateText();
        }
        else
        {
            Debug.LogWarning("Invalid EW Wind Constant entered!");
        }
    }

    // Updates the text in the HUD based on the absolute values and directions of the wind
    void UpdateText()
    {
        if (nS < 0)
        {
            if (eW < 0)
            {
                windText.text = "Wind: " + -nS + " m/s² S - " + -eW + " m/s² W";
            }
            else
            {
                windText.text = "Wind: " + -nS + " m/s² S - " + eW + " m/s² E";
            }
        }
        else
        {
            if (eW < 0)
            {
                windText.text = "Wind: " + nS + " m/s² N - " + -eW + " m/s² W";
            }
            else
            {
                windText.text = "Wind: " + nS + " m/s² N - " + eW + " m/s² E";
            }
        }
    }
}