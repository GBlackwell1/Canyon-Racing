using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    private LevelManager levelManager;
    private int asteroidsWithin = 0;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            asteroidsWithin++;
            levelManager.EnterCourse();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            asteroidsWithin--;
            if (asteroidsWithin <= 0)
            {
                levelManager.ExitCourse();
            }
        }
    }
}
