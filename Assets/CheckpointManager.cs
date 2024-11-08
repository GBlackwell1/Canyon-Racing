using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    
    private GameObject finishLine;
    private List<GameObject> checkpoints = new List<GameObject>();
    [SerializeField] private int checkpointsRemaining;
    public AudioClip checkpointPass;
    public GameObject checkpointLabel;
    private TextMeshProUGUI checkpointLabelTMP;
    private float time = 0f;
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
        time += Time.deltaTime;
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
            GameManager.Instance.SaveLevelData(SceneManager.GetActiveScene().name, time);
            Debug.Log(GameManager.Instance.GetLevelData().time);
            GameManager.Instance.GoToLevel(0);
            Debug.Log("Finish reached");
            GameObject.Find("Spaceship").GetComponent<AudioSource>().PlayOneShot(checkpointPass);
        }
    }
}
