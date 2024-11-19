using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public float MoveSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        Vector2 rotation = transform.rotation.eulerAngles;
        rotation.y += Time.deltaTime * MoveSpeed;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
