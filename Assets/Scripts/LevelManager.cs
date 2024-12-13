using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject UI;
    public GameObject player;
    private GameObject brokenShip;
    private UIManager uiManager;
    private bool isPaused = false;
    private bool isRunning = false;
    private bool preventInput = false;
    private Coroutine levelExitCoroutine;
    private Coroutine resumeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = UI.GetComponent<UIManager>();   
        resumeCoroutine = StartCoroutine(StartLevel());
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.Find("Spaceship");
        brokenShip = GameObject.Find("Broken Ship");
        brokenShip.SetActive(false);
    }

    void Update()
    {
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape) && !preventInput)
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (resumeCoroutine != null)
            StopCoroutine(resumeCoroutine);
        uiManager.PauseGame();
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        uiManager.UnpauseGame();
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        resumeCoroutine = StartCoroutine(ResumeLevel());
    }

    public void ExitCourse()
    {
        levelExitCoroutine = StartCoroutine(ExitCourseCount());
    }

    public void EnterCourse()
    {
        uiManager.StopWarning();
        if (isRunning)
            StopCoroutine(levelExitCoroutine);
    }

    public void ShipDestroyed()
    {
        StartCoroutine(BreakShip());
    }

    private IEnumerator StartLevel()
    {
        Time.timeScale = 0;
        uiManager.StartCountdown(3);
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
    }

    private IEnumerator ResumeLevel()
    {
        uiManager.StartCountdown(3);
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
    }

    private IEnumerator ExitCourseCount()
    {
        isRunning = true;
        uiManager.StartWarning(5);
        yield return new WaitForSeconds(5);
        StartCoroutine(BreakShip());
    }

    private IEnumerator BreakShip()
    {
        preventInput = true;
        brokenShip.transform.position = player.transform.position;
        brokenShip.transform.rotation = player.transform.rotation;
        float speed = player.GetComponent<PlayerController>().currentSpeed;
        player.SetActive(false);
        UI.SetActive(false);
        brokenShip.SetActive(true);
        foreach (Transform child in brokenShip.transform)
        {
            child.gameObject.GetComponent<Rigidbody>().AddForce((Random.insideUnitSphere * 1000) + brokenShip.transform.forward * speed * 35);
        }
        yield return new WaitForSeconds(3);
        GameManager.Instance.ReloadScene();
    }
}
