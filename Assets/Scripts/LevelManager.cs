using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject UI;
    public GameObject player;
    private UIManager uiManager;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = UI.GetComponent<UIManager>();
        StartCoroutine(StartLevel());
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopAllCoroutines();
            uiManager.PauseGame();
            Time.timeScale = 0;
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (isPaused && Input.GetKeyDown(KeyCode.R))
        {
            Resume();
        }
    }

    public void Resume()
    {
        uiManager.UnpauseGame();
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(ResumeLevel());
    }

    private IEnumerator StartLevel()
    {
        uiManager.StartCountdown();
        player.GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSecondsRealtime(3);
        player.GetComponent<PlayerController>().enabled = true;
    }

    private IEnumerator ResumeLevel()
    {
        uiManager.StartCountdown();
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
    }
}
