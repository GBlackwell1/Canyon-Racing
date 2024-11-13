using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    private GameObject playerPos;
    private Vector3 cameraPosOffset;
    private Quaternion cameraRotOffset;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("CamPos");
        cameraPosOffset = transform.position-playerPos.transform.position;
        cameraRotOffset = Quaternion.Inverse(playerPos.transform.rotation) * transform.rotation;
    }

    // LateUpdate runs after all transforms have been positioned
    void LateUpdate()
    {
        transform.position = playerPos.transform.position + cameraPosOffset;
        transform.rotation = playerPos.transform.rotation * cameraRotOffset;
    }
}
