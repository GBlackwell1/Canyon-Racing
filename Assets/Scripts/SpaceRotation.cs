using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 0.004f));
    }
}
