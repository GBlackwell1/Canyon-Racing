using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    
    private GameObject finishLine;
    private List<GameObject> checkpoints = new List<GameObject>();
    [SerializeField] private int checkpointsRemaining;
    public AudioClip checkpointPass;
    public GameObject checkpointLabel;
    private TextMeshProUGUI checkpointLabelTMP;
    void Start()
    {
        finishLine = GameObject.Find("FinishLine");
        finishLine.SetActive(false);
        foreach (Transform checkpoint in GetComponentsInChildren<Transform>())
        {
            if (checkpoint.GameObject().CompareTag("Checkpoint")) 
                checkpoints.Add(checkpoint.GameObject());
        }
        checkpointsRemaining = checkpoints.Count;
        checkpointLabelTMP = checkpointLabel.GetComponent<TextMeshProUGUI>();
        checkpointLabelTMP.SetText("Checkpoints remaining: " + checkpointsRemaining + "/" + checkpoints.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (checkpointsRemaining <= 0)
        {
            finishLine.SetActive(true);
            checkpointLabelTMP.color = Color.green;
            checkpointLabelTMP.SetText("Get to the finish!");
        }
    }

    public void ColliderEvent(GameObject other)
    {
        if (checkpoints.Contains(other))
        {
            other.SetActive(false);
            GameObject.Find("Spaceship").GetComponent<AudioSource>().PlayOneShot(checkpointPass);
            checkpointsRemaining--;
            checkpointLabelTMP.SetText("Checkpoints remaining: " + checkpointsRemaining + "/" + checkpoints.Count);
        }
        else if (other.CompareTag("Finish"))
        {
            // TODO: Do something here, load gameover scene, level end, noise, etc.
            Debug.Log("Finish reached");
            GameObject.Find("Spaceship").GetComponent<AudioSource>().PlayOneShot(checkpointPass);
        }
    }
}
