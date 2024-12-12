using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public float PauseDuration = 0.2f;
    public GameObject PauseMenu;
    public GameObject OverlayPanel;
    public GameObject Countdown;

    private GameObject GameplayMenu;

    private GameObject Gameplay;
    private Vector2 originalPausePosition;
    private Vector2 offScreenPausePosition;
    private Coroutine countdownCoroutine;
    private Coroutine warningCoroutine;

    // Start is called before the first frame update
    void Start()
    {

        GameplayMenu = transform.Find("Gameplay").gameObject;
        Gameplay = GameplayMenu.transform.Find("control1").gameObject;

        var transformRect = PauseMenu.GetComponent<RectTransform>();
        originalPausePosition = new Vector2(transformRect.anchoredPosition.x, transformRect.anchoredPosition.y);
        offScreenPausePosition = new Vector2(transformRect.anchoredPosition.x - transformRect.rect.width, transformRect.anchoredPosition.y);
        transformRect.anchoredPosition = offScreenPausePosition;
    }

    public void PauseGame()
    {
        StopCoroutine(countdownCoroutine);
        StartCoroutine(OpenPauseMenu());
    }

    public void UnpauseGame()
    {
        StartCoroutine(ClosePauseMenu());
    }

    public void StartCountdown(int length)
    {
        countdownCoroutine = StartCoroutine(ExecuteCountdown(length, false));
    }

    public void StartWarning(int length)
    {
        warningCoroutine = StartCoroutine(ExecuteCountdown(length, true));
    }

    public void StopWarning()
    {
        if (warningCoroutine!= null)
            StopCoroutine(warningCoroutine);
        Countdown.SetActive(false);
    }

    public void GoToGameplay(){
        Gameplay.SetActive(true);
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
        Countdown.SetActive(false);
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

    private IEnumerator ExecuteCountdown(int length, bool warning)
    {
        Countdown.SetActive(true);
        var text = Countdown.GetComponent<TMPro.TextMeshProUGUI>();
        for (int i = length; i > 0; i--)
        {
            Countdown.SetActive(true);
            string result = warning ? $"Turn Back: {i}" : i.ToString();
            text.text = result;
            yield return warning ? new WaitForSeconds(1) : new WaitForSecondsRealtime(1);
        }
        if (SceneManager.GetActiveScene().name == "Level 4" && !warning )
            GameObject.Find("Info")?.SetActive(false);
        Countdown.SetActive(false);
        OverlayPanel.SetActive(true);
    }
}
