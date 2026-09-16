using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BallManager : MonoBehaviour
{
    public GameObject objectPrefab; // Reference to the prefab of the ball object
    public Transform spawnPoint; // Reference to the spawn point for the ball
    public TMP_InputField ballsInputField; // Reference to the Input Field
    public Button ballButton; // Refernce to the Ball Button
    public TMP_Text ballText; // Reference to the output of the ball number value
    public TMP_Text ballVelocityText; // Reference to the output of the average ball velocity
    public TMP_Text sysMomentumText; // Reference to the output of the total momentum of the system
    public float ballValue = 1; // New number of balls
    private float initBallValue = 1; // Starting number of balls
    private float avgBallV = 0; // Used for average velocity calculations
    private float totalM = 0; // Used for the total system momentum

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that the references are set in the Unity Editor
        if (objectPrefab == null)
        {
            Debug.LogError("Object Prefab is not set!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn Point reference is not set!");
            return;
        }

        if (ballsInputField == null)
        {
            Debug.LogError("Sphere Input Field reference is not set!");
            return;
        }

        if (ballButton == null)
        {
            Debug.LogError("Sphere Button reference is not set!");
            return;
        }

        if (ballText == null)
        {
            Debug.LogError("Sphere Text Field reference is not set!");
            return;
        }

        if (ballVelocityText == null)
        {
            Debug.LogError("Sphere Velocity Text Field reference is not set!");
            return;
        }

        if (sysMomentumText == null)
        {
            Debug.LogError("System Momentum Text Field reference is not set!");
            return;
        }

        ballButton.onClick.AddListener(GetBallValue);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the new ball value is greater than the previous
        if (ballValue > initBallValue)
        {
            // Create balls based on the value
            for (int i = 0; i < (ballValue - initBallValue); i++)
            {
                GameObject newBall = Instantiate(objectPrefab, spawnPoint.position + new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10)), spawnPoint.rotation);

                // Set the spawn position and rotation based off the current values
                BallReset ballReset = newBall.GetComponent<BallReset>();
                ballReset.initialPosition = spawnPoint.position;
                ballReset.initialRotation = spawnPoint.rotation;

                // Set the new ball as a child of Objects
                newBall.transform.SetParent(spawnPoint);
            }
            // Set the original ball value to the new one and in the text
            initBallValue = ballValue;
            ballText.text = "Number of Balls: " + ballValue;
        }

        // Check if the new ball value is less than the previous
        if (ballValue < initBallValue)
        {
            // Destroy balls based on the new ball value in the reverse order that they were created
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Sphere");
            for (int i = 0; i < (initBallValue - ballValue); i++)
            {
                Destroy(objects[(objects.Length - 1) - i]);
            }
            // Set the original ball value to the new one
            initBallValue = ballValue;
            ballText.text = "Number of Spheres: " + ballValue;
        }

        // Find and output the average velocity of all balls on the map
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Sphere");
        foreach (GameObject ball in balls)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            avgBallV += rb.linearVelocity.magnitude;
        }
        totalM = avgBallV * balls[0].GetComponent<Rigidbody>().mass; // Calculate the total system momentum
        sysMomentumText.text = "Total System Momentum: " + Math.Round(totalM, 3) + " kg*m/s"; // Display the momentum
        avgBallV /= ballValue; // Calculate the average ball velocity
        ballVelocityText.text = "Average Sphere Velocity : " + Math.Round(avgBallV, 3) + " m/s"; // Display the average velocity
        avgBallV = 0; // Reset the average velocity value
    }

    public void GetBallValue()
    {
        // Get the number of balls from the input field and convert it to an int
        if (int.TryParse(ballsInputField.text, out int balls))
        {
            // Change the current number of balls
            ballValue = balls;
        }
        else
        {
            Debug.LogWarning("Invalid sphere quantity entered!");
        }
    }
}