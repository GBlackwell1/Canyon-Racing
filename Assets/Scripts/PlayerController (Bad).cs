using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float rollSpeed = 100f;
    public float pitchSpeed = 100f;
    public float thrustAcceleration = 20f;
    public float maxForwardSpeed = 80f;
    public float maxReverseSpeed = -20f;
    private float currentSpeed = 0f;

    private float currentRollSpeed = 0f;
    private float currentPitchSpeed = 0f;
    private float currentYawSpeed = 0f;
    public float rollAcceleration = 50f;
    public float pitchAcceleration = 50f;
    public float yawAcceleration = 50f;
    public float maxRollSpeed = 200f;
    public float maxPitchSpeed = 200f;
    public float maxYawSpeed = 200f;

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleThrust();
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            currentRollSpeed += rollAcceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            currentRollSpeed -= rollAcceleration * Time.deltaTime;
        }
        else
        {
            currentRollSpeed = Mathf.Lerp(currentRollSpeed, 0, Time.deltaTime * 2);
        }

        if (Input.GetKey(KeyCode.W))
        {
            currentPitchSpeed += pitchAcceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentPitchSpeed -= pitchAcceleration * Time.deltaTime;
        }
        else
        {
            currentPitchSpeed = Mathf.Lerp(currentPitchSpeed, 0, Time.deltaTime * 2);
        }

        if (Input.GetKey(KeyCode.A))
        {
            currentYawSpeed -= yawAcceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentYawSpeed += yawAcceleration * Time.deltaTime;
        }
        else
        {
            currentYawSpeed = Mathf.Lerp(currentYawSpeed, 0, Time.deltaTime * 2);
        }

        // Clamp the roll and pitch speeds
        currentRollSpeed = Mathf.Clamp(currentRollSpeed, -maxRollSpeed, maxRollSpeed);
        currentPitchSpeed = Mathf.Clamp(currentPitchSpeed, -maxPitchSpeed, maxPitchSpeed);
        currentYawSpeed = Mathf.Clamp(currentYawSpeed, -maxYawSpeed, maxYawSpeed);

        transform.Rotate(currentPitchSpeed * Time.deltaTime, currentYawSpeed * Time.deltaTime, currentRollSpeed * Time.deltaTime);
    }

    void HandleThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed += thrustAcceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed -= thrustAcceleration * Time.deltaTime;
        }

        // Clamp the speed to the max forward and reverse speeds
        currentSpeed = Mathf.Clamp(currentSpeed, maxReverseSpeed, maxForwardSpeed);

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}