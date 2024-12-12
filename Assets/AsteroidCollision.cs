using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
    private LevelManager levelManager;
    private bool enter = true;
    private bool isRunning = false;
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
            enter = true;
            StopAllCoroutines();
            if (isRunning)
                levelManager.EnterCourse();
            isRunning = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            enter = false;
            if (!isRunning) 
                StartCoroutine(VerifyExit());
        }
    }
    IEnumerator VerifyExit()
    {
        isRunning = true;
        yield return new WaitForSeconds(1f);
        if (!enter) levelManager.ExitCourse();
    }
}
