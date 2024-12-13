using System.Collections;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public float HomeAnimationDuration = 0.5f;
    public float LevelEndAnimationDuration = 0.5f;
    public AudioClip MainMenuRise;
    private GameObject Home;
    private GameObject Gameplay;
    private GameObject LevelSelect;
    private GameObject LevelEnd;
    private Vector2 originalPanelPosition;
    private Vector2 offScreenPanelPosition;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Home = transform.Find("Home").gameObject;
        Gameplay = transform.Find("Gameplay").gameObject;
        LevelSelect = transform.Find("Level Select").gameObject;
        LevelEnd = transform.Find("Level End").gameObject;

        if (GameManager.Instance.LevelComplete)
        {
            LevelEnd.SetActive(true);

            // Move the menu off screen
            var transform = LevelEnd.GetComponent<RectTransform>();
            originalPanelPosition = new Vector2(transform.anchoredPosition.x, transform.anchoredPosition.y);
            offScreenPanelPosition = new Vector2(transform.anchoredPosition.x - transform.rect.width, transform.anchoredPosition.y);
            foreach (RectTransform child in LevelEnd.GetComponentInChildren<RectTransform>())
            {
                if (child.tag == "Animated")
                    child.anchoredPosition = offScreenPanelPosition;
            }

            // Set the data
            var levelStats = GameManager.Instance.GetLevelData();
            float time = (float)decimal.Round((decimal)levelStats.time, 2);
            float bestTime = float.MaxValue;
            bool newBest = false;
            if (PlayerPrefs.HasKey($"{levelStats.level} time"))
            {
                bestTime = PlayerPrefs.GetFloat($"{levelStats.level} time");
            }

            if (time < bestTime)
            {
                newBest = true;
                PlayerPrefs.SetFloat($"{levelStats.level} time", time);
                PlayerPrefs.Save();
            }

            var textBoxes = LevelEnd.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
            textBoxes[0].SetText(levelStats.level);
            textBoxes[1].SetText("Time: " + time + (newBest ? " (New Best!)" : $" (Best: {bestTime})"));

            StartCoroutine(MenuAnimation(LevelEnd, 0.5f, LevelEndAnimationDuration));
        }
        else if (GameManager.Instance.InitialLoad)
        {
            Home.SetActive(true);
            var transform = Home.GetComponent<RectTransform>();
            originalPanelPosition = new Vector2(transform.anchoredPosition.x, transform.anchoredPosition.y);
            offScreenPanelPosition = new Vector2(transform.anchoredPosition.x - transform.rect.width, transform.anchoredPosition.y);
            foreach (RectTransform child in Home.GetComponentInChildren<RectTransform>())
            {
                if(child.tag == "Animated")
                    child.anchoredPosition = offScreenPanelPosition;
            }
            StartCoroutine(MenuAnimation(Home, 3f, HomeAnimationDuration));
        }
        else
        {
            Home.SetActive(true);
        }
    }
    private void Awake()
    {
        GetComponent<AudioSource>().PlayOneShot(MainMenuRise);
    }
    public void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
    }
    public void GoToMainMenu()
    {
        Home.SetActive(true);
        Gameplay.SetActive(false);
        LevelSelect.SetActive(false);
        LevelEnd.SetActive(false);
    }

    public void GoToGameplay(){
        Home.SetActive(true);
        Gameplay.SetActive(true);
        LevelSelect.SetActive(false);
        LevelEnd.SetActive(false);
        foreach (Transform child in Gameplay.transform)
        {
            child.gameObject.SetActive(false);
        }
        Gameplay.transform.Find("Gameplay - Controls 1").gameObject.SetActive(true);
    }

    public void GoToLevelSelect()
    {
        Home.SetActive(false);
        Gameplay.SetActive(false);
        LevelSelect.SetActive(true);
        LevelEnd.SetActive(false);
    }

    public void GoToLevel(int level)
    {
        GameManager.Instance.GoToLevel(level);
    }

    public void RestartLevel()
    {
        GameManager.Instance.RestartLevel();
    }

    public void GoToNextLevel()
    {
        GameManager.Instance.GoToNextLevel();
    }

    public void Quit()
    {
        GameManager.Instance.Quit();
    }

    private IEnumerator MenuAnimation(GameObject target, float delay, float duration)
    {
        yield return new WaitForSeconds(delay);
        float multiplier = 1 / duration;
        foreach (RectTransform child in target.GetComponentInChildren<RectTransform>())
        {
            if (child.tag != "Animated")
                continue;
            float t = 0f;
            while (child.anchoredPosition.x < originalPanelPosition.x)
            {
                t += Time.deltaTime;
                child.anchoredPosition = Vector2.Lerp(offScreenPanelPosition, originalPanelPosition, t * multiplier);
                yield return null;
            }
        }
    }
}
