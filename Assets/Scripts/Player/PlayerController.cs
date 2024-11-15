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
    private Quaternion initialRotation;

    [Header("UI")]
    public GameObject speed;
    private TextMeshProUGUI speedLable;
    public GameObject crossHair;
    private RectTransform crossHairTransform;

    [Header("Audio")]
    public AudioClip thrustClip;
    AudioSource audioSource;
    AudioSource audioSourceShotEffect;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = Camera.main;
        initialRotation = playerCamera.transform.localRotation;
        speedLable = speed.GetComponent<TextMeshProUGUI>();
        crossHairTransform = crossHair.GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        audioSourceShotEffect = GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleThrust();
    }

    private void LateUpdate()
    {
        var multiplier = currentSpeed / maxForwardSpeed;
        Quaternion targetRotation = new Quaternion(playerCamera.transform.localRotation.y + (mouseY * .08f * multiplier),
                                                   playerCamera.transform.localRotation.x + -(mouseX * .08f * multiplier),
                                                   playerCamera.transform.localRotation.z + (mouseX * .02f) * (mouseY * .02f) * multiplier, playerCamera.transform.localRotation.w);
        playerCamera.transform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, 0.005f);
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
        // TODO: Confine this in some manner
        
       
    }

    void HandleThrust()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
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
                if (currentSpeed <= maxBoostSpeed + maxForwardSpeed
                    && !audioSourceShotEffect.isPlaying)
                    audioSourceShotEffect.Play();

            }
            else
            {
                currentSpeed += thrustAcceleration * Time.deltaTime;
            }
            additionalSpeed += boostAcceleration * Time.deltaTime;
            additionalSpeed = Mathf.Clamp(additionalSpeed, 0, maxBoostSpeed);
            if (playerCamera.fieldOfView < boostedFOV)
            {
                System.Random random = new System.Random();
                StartCoroutine(ChangeFOV(true));
                Vector3 camForce = new Vector3((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
                playerCamera.GetComponent<Rigidbody>().AddForce(camForce, ForceMode.Impulse);
                
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
                StartCoroutine(ChangeFOV(false));
            }

        }

        playerCamera.fieldOfView = Mathf.Clamp(playerCamera.fieldOfView, normalFOV, boostedFOV);

        currentSpeed = Mathf.Clamp(currentSpeed, maxReverseSpeed, maxForwardSpeed + additionalSpeed);

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        speedLable.text = "Speed: " + currentSpeed.ToString("F2");
    }
    // Adds slight delay to camera increasing and decreasing
    IEnumerator ChangeFOV(bool increaseFOV)
    {
        yield return new WaitForSeconds(0.1f);
        if (increaseFOV)
        {
            playerCamera.fieldOfView += Time.deltaTime * 20;
        }
        else
        {
            playerCamera.fieldOfView -= Time.deltaTime * 20;
        }
    }
}