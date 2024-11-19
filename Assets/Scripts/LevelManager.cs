using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject UI;
    public GameObject player;
    private UIManager uiManager;
    private bool isPaused = false;
    private Coroutine levelExitCoroutine;
    private Coroutine resumeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = UI.GetComponent<UIManager>();
        resumeCoroutine = StartCoroutine(StartLevel());
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (isPaused && Input.GetKeyDown(KeyCode.R))
        {
            Resume();
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
        StopCoroutine(levelExitCoroutine);
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
        uiManager.StartWarning(5);
        yield return new WaitForSeconds(5);
        GameManager.Instance.ReloadScene();
    }
}
