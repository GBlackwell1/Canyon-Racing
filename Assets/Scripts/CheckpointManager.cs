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
    
    //checkpoint arrow
    private Vector3 targetPosition;
    private Vector3 checkpointPosition;
    private RectTransform pointerRectTransform;
    public GameObject CheckpointArrow;
    public GameObject Spaceship;

    public AudioClip checkpointPass;
    public AudioClip FinishClip;
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
        CheckpointArrow.SetActive(true);
        pointerRectTransform = CheckpointArrow.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //logic for arrow pointing
        if (checkpoints.Count > 0){
            targetPosition = checkpoints[0].transform.position;
            Vector3 playerForward = Spaceship.transform.forward;
            Vector3 toPosition = (targetPosition - Spaceship.transform.position).normalized;
            playerForward.y = 0; toPosition.y = 0;
 
            float angle = Vector3.SignedAngle(playerForward, toPosition, Vector3.up);           
            pointerRectTransform.eulerAngles = new Vector3(0, 0, -angle+40);
        }
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
            StartCoroutine(DisableCheckpoint(other));
            checkpoints.Remove(other);
            if (checkpoints.Count > 0)
            {
                checkpoints[0].SetActive(true);
                GameObject.Find("Spaceship").GetComponent<AudioSource>().PlayOneShot(checkpointPass, 4);
            }
            else
            {
                finishActive = true;
                GameObject.Find("Spaceship").GetComponent<AudioSource>().PlayOneShot(FinishClip, 4);
                CheckpointArrow.SetActive(false);
            }
           
            checkpointLabelTMP.SetText("Checkpoints remaining: " + checkpoints.Count + "/" + checkpointCount);

        }
        else if (other.CompareTag("Finish"))
        {
            GameManager.Instance.SaveLevelData(SceneManager.GetActiveScene().name, time);
            GameManager.Instance.GoToLevel(0);
        }
    }

    private IEnumerator DisableCheckpoint(GameObject checkpoint)
    {
        yield return new WaitForSeconds(0.25f);
        checkpoint.SetActive(false);
    }
}
