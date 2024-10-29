using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private GameObject spaceship;
    // Start is called before the first frame update
    void Start()
    {
        spaceship = GameObject.Find("Spaceship");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position-spaceship.transform.position;
    }
}
