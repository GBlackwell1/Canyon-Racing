using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{
    public float RotationSpeed;
    private float randX, randY, randZ;
    private LevelManager LevelManager;
    private void Start()
    {

        randX = Random.Range(-RotationSpeed, RotationSpeed);
        randY = Random.Range(-RotationSpeed, RotationSpeed);
        randZ = Random.Range(-RotationSpeed, RotationSpeed);
        LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(randX, randY, randZ);
    }
}
