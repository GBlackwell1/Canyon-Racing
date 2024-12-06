using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    
    private GameObject finishLine;
    private List<GameObject> checkpoints = new List<GameObject>();
    private int checkpointCount = 0;
    public AudioClip checkpointPass;
    public GameObject checkpointLabel;
    private TextMeshProUGUI checkpointLabelTMP;
    private float time = 0f;
    private bool finishActive = false;
    void Start()
    {
        finishLine = GameObject.Find("FinishLine");
        finishLine.SetActive(false);
        foreach (Rigidbody checkpoint in GetComponentsInChildren<Rigidbody>())
        {
            if (checkpoint.GameObject().CompareTag("Checkpoint")) 
                checkpoints.Add(checkpoint.GameObject());
        }
        checkpointCount = checkpoints.Count;
        checkpointLabelTMP = checkpointLabel.GetComponent<TextMeshProUGUI>();
        checkpointLabelTMP.SetText("Checkpoints remaining: " + checkpoints.Count + "/" + checkpointCount);
        foreach (GameObject checkpoint in checkpoints)
        {
            checkpoint.SetActive(false);
        }
        checkpoints[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (finishActive && !finishLine.activeInHierarchy)
        {
            finishLine.SetActive(true);
            checkpointLabelTMP.color = Color.green;
            checkpointLabelTMP.SetText("Get to the finish!");
        }
        time += Time.deltaTime;
    }

    public void ColliderEvent(GameObject other)
    {
        if (checkpoints.Contains(other))
        {
            other.SetActive(false);
            checkpoints.Remove(other);
            if (checkpoints.Count > 0)
                checkpoints[0].SetActive(true);
            else
                finishActive = true;
            GameObject.Find("Spaceship").GetComponent<AudioSource>().PlayOneShot(checkpointPass, 4);
            checkpointLabelTMP.SetText("Checkpoints remaining: " + checkpoints.Count + "/" + checkpointCount);

        }
        else if (other.CompareTag("Finish"))
        {
            GameManager.Instance.SaveLevelData(SceneManager.GetActiveScene().name, time);
            GameManager.Instance.GoToLevel(0);
            GameObject.Find("Spaceship").GetComponent<AudioSource>().PlayOneShot(checkpointPass);
        }
    }
}
