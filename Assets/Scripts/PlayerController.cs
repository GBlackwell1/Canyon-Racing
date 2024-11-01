using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    public float thrustAcceleration = 80f;
    public float boostAcceleration = 80f;
    public float maxBoostSpeed = 40f;
    public float maxForwardSpeed = 80f;
    public float maxReverseSpeed = -20f;
    public float boostDampener = 0.5f; // Reduction in manuverability when boosting
    private float currentSpeed = 0f;
    private float additionalSpeed = 0f;
    bool isBoosting = true;

    [Header("Roll")]
    public float rollAcceleration = 400f;
    public float maxRollSpeed = 100f;
    private float currentRollSpeed = 0f;

    [Header("Pitch and Yaw")]
    public float pitchSpeed = 1f;
    public float maxPitchSpeed = 100f;
    public float yawSpeed = 1f;
    public float maxYawSpeed = 100f;
    private float currentPitchSpeed = 0f;
    private float currentYawSpeed = 0f;

    [Header("Mouse Settings")]
    public float maxMouseX = 25f;
    public float maxMouseY = 25f;
    public float sensitivity = 1f;
    private float mouseX = 0f;
    private float mouseY = 0f;

    [Header("Camera")]
    public float normalFOV = 70;
    public float boostedFOV = 80;
    private Camera playerCamera;

    [Header("UI")]
    public GameObject speed;
    private TextMeshProUGUI speedLable;
    public GameObject crossHair;
    private RectTransform crossHairTransform;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = Camera.main;
        speedLable = speed.GetComponent<TextMeshProUGUI>();
        crossHairTransform = crossHair.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleThrust();
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            currentRollSpeed += rollAcceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentRollSpeed -= rollAcceleration * Time.deltaTime;
        }
        else
        {
            currentRollSpeed = Mathf.Lerp(currentRollSpeed, 0, Time.deltaTime * 2);
        }

        float mouseXDelta = Input.GetAxis("Mouse X") * sensitivity;
        float mouseYDelta = Input.GetAxis("Mouse Y") * sensitivity;

        mouseX = Mathf.Clamp(mouseX + mouseXDelta, -maxMouseX, maxMouseX);
        mouseY = Mathf.Clamp(mouseY + mouseYDelta, -maxMouseY, maxMouseY);

        crossHairTransform.anchoredPosition = new Vector2(mouseX * 2, mouseY * 2);

        currentYawSpeed = mouseX * yawSpeed;
        currentPitchSpeed = -mouseY * pitchSpeed;

        if (!isBoosting)
        {
            currentRollSpeed = Mathf.Clamp(currentRollSpeed, -maxRollSpeed, maxRollSpeed);
            currentPitchSpeed = Mathf.Clamp(currentPitchSpeed, -maxPitchSpeed, maxPitchSpeed);
            currentYawSpeed = Mathf.Clamp(currentYawSpeed, -maxYawSpeed, maxYawSpeed);
        }
        else
        {
            currentRollSpeed = Mathf.Clamp(currentRollSpeed, -maxRollSpeed * boostDampener, maxRollSpeed * boostDampener);
            currentPitchSpeed = Mathf.Clamp(currentPitchSpeed, -maxPitchSpeed * boostDampener, maxPitchSpeed * boostDampener);
            currentYawSpeed = Mathf.Clamp(currentYawSpeed, -maxYawSpeed * boostDampener, maxYawSpeed * boostDampener);
        }

        transform.Rotate(currentPitchSpeed * Time.deltaTime, currentYawSpeed * Time.deltaTime, currentRollSpeed * Time.deltaTime);
    }

    void HandleThrust()
    {
        if (Input.GetKey(KeyCode.W) && !isBoosting)
        {
            currentSpeed += thrustAcceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentSpeed -= thrustAcceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!isBoosting)
            {
                isBoosting = true;
            }
            else
            {
                currentSpeed += thrustAcceleration * Time.deltaTime;
            }
            additionalSpeed += boostAcceleration * Time.deltaTime;
            additionalSpeed = Mathf.Clamp(additionalSpeed, 0, maxBoostSpeed);
            if (playerCamera.fieldOfView < boostedFOV)
            {
                playerCamera.fieldOfView += Time.deltaTime * 40;
            }
        } 
        else
        {
            if (isBoosting)
            {
                isBoosting = false;
            }
            additionalSpeed = Mathf.Lerp(additionalSpeed, 0, Time.deltaTime);
            if (playerCamera.fieldOfView > normalFOV)
            {
                playerCamera.fieldOfView -= Time.deltaTime *40;
            }

        }

        playerCamera.fieldOfView = Mathf.Clamp(playerCamera.fieldOfView, normalFOV, boostedFOV);

        currentSpeed = Mathf.Clamp(currentSpeed, maxReverseSpeed, maxForwardSpeed + additionalSpeed);

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        speedLable.text = "Speed: " + currentSpeed.ToString("F2");
    }
}