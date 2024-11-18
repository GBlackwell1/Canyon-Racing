using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public float PauseDuration = 0.2f;
    public GameObject PauseMenu;
    public GameObject OverlayPanel;
    public GameObject Countdown;
    private Vector2 originalPausePosition;
    private Vector2 offScreenPausePosition;

    // Start is called before the first frame update
    void Start()
    {
        var transform = PauseMenu.GetComponent<RectTransform>();
        originalPausePosition = new Vector2(transform.anchoredPosition.x, transform.anchoredPosition.y);
        offScreenPausePosition = new Vector2(transform.anchoredPosition.x - transform.rect.width, transform.anchoredPosition.y);
        transform.anchoredPosition = offScreenPausePosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        Countdown.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(OpenPauseMenu());
    }

    public void UnpauseGame()
    {
        StartCoroutine(ClosePauseMenu());
    }

    public void StartCountdown()
    {
        StartCoroutine(ExecuteCountdown());
    }

    public void RestartLevel()
    {
        GameManager.Instance.ReloadScene();
    }

    public void Quit()
    {
        GameManager.Instance.GoToLevel(0);
    }

    private IEnumerator OpenPauseMenu()
    {
        PauseMenu.SetActive(true);
        OverlayPanel.SetActive(false);
        var transform = PauseMenu.GetComponent<RectTransform>();
        float t = 0f;
        float multiplier = 1 / PauseDuration;
        while (transform.anchoredPosition.x < originalPausePosition.x)
        {
            t += Time.unscaledDeltaTime;
            transform.anchoredPosition = Vector2.Lerp(offScreenPausePosition, originalPausePosition, t * multiplier);
            yield return null;
        }
    }

    private IEnumerator ClosePauseMenu()
    {
        var transform = PauseMenu.GetComponent<RectTransform>();
        float t = 0f;
        float multiplier = 1 / PauseDuration;
        while (transform.anchoredPosition.x > offScreenPausePosition.x)
        {
            t += Time.unscaledDeltaTime;
            transform.anchoredPosition = Vector2.Lerp(originalPausePosition, offScreenPausePosition, t * multiplier);
            yield return null;
        }

        PauseMenu.SetActive(false);
        OverlayPanel.SetActive(true);
    }

    private IEnumerator ExecuteCountdown()
    {
        Countdown.SetActive(true);
        var text = Countdown.GetComponent<TMPro.TextMeshProUGUI>();
        text.text = "3";
        Countdown.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        text.text = "2";
        yield return new WaitForSecondsRealtime(1);
        text.text = "1";
        yield return new WaitForSecondsRealtime(1);
        Countdown.SetActive(false);
        OverlayPanel.SetActive(true);
    }
}
