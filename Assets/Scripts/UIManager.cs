using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public float PauseDuration = 0.2f;
    public GameObject pauseMenu;
    public GameObject overlay;
    private bool isPaused = false;
    private Vector2 originalPausePosition;
    private Vector2 offScreenPausePosition;

    // Start is called before the first frame update
    void Start()
    {
        var transform = pauseMenu.GetComponent<RectTransform>();
        originalPausePosition = new Vector2(transform.anchoredPosition.x, transform.anchoredPosition.y);
        offScreenPausePosition = new Vector2(transform.anchoredPosition.x - transform.rect.width, transform.anchoredPosition.y);
        transform.anchoredPosition = offScreenPausePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            overlay.SetActive(false);
            StartCoroutine(OpenPauseMenu());
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (isPaused && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ClosePauseMenu());
        }
    }

    private IEnumerator OpenPauseMenu()
    {
        var transform = pauseMenu.GetComponent<RectTransform>();
        float t = 0f;
        float multiplier = 1 / PauseDuration;
        //Debug.Log($"Time: {t}, original position: {originalPausePosition}, offscreen position: {offScreenPausePosition}, position: {transform.anchoredPosition}");
        while (transform.anchoredPosition.x < originalPausePosition.x)
        {
            t += Time.unscaledDeltaTime;
            Debug.Log($"Time: {t}, original position: {originalPausePosition}, offscreen position: {offScreenPausePosition}, position: {transform.anchoredPosition}");
            transform.anchoredPosition = Vector2.Lerp(offScreenPausePosition, originalPausePosition, t * multiplier);
            yield return null;
        }
    }

    private IEnumerator ClosePauseMenu()
    {
        var transform = pauseMenu.GetComponent<RectTransform>();
        float t = 0f;
        float multiplier = 1 / PauseDuration;
        //Debug.Log($"Time: {t}, original position: {originalPausePosition}, offscreen position: {offScreenPausePosition}, position: {transform.anchoredPosition}");
        while (transform.anchoredPosition.x > offScreenPausePosition.x)
        {
            t += Time.unscaledDeltaTime;
            Debug.Log($"Time: {t}, original position: {originalPausePosition}, offscreen position: {offScreenPausePosition}, position: {transform.anchoredPosition}");
            transform.anchoredPosition = Vector2.Lerp(originalPausePosition, offScreenPausePosition, t * multiplier);
            yield return null;
        }

        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        overlay.SetActive(true);
    }
}
