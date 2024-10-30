using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rollSpeed = 100f;
    public float pitchSpeed = 100f;
    public float thrustSpeed = 10f;
    private float currentSpeed = 0f;

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleThrust();
    }

    void HandleRotation()
    {
        float roll = 0f;
        float pitch = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            roll = rollSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            roll = -rollSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            pitch = pitchSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pitch = -pitchSpeed * Time.deltaTime;
        }

        transform.Rotate(pitch, 0f, roll);
    }

    void HandleThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed = thrustSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = -thrustSpeed;
        }
        else
        {
            currentSpeed = 0f;
        }

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}
