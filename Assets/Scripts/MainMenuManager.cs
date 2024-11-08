using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject LevelSelect;
    public GameObject LevelComplete;
    public GameObject LevelName;
    public GameObject Time;
    public float animationDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        if (GameManager.Instance.LevelComplete)
        {
            Debug.Log(GameManager.Instance.GetLevelData().time);
            LevelComplete.SetActive(true);
            var levelStats = GameManager.Instance.GetLevelData();
            float time = (float)decimal.Round((decimal)levelStats.time, 2);
            LevelName.GetComponent<TMPro.TextMeshProUGUI>().SetText(levelStats.level);
            Time.GetComponent<TMPro.TextMeshProUGUI>().SetText("Time: " + time);
        }
        else
        {
            MainMenu.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToLevelSelect()
    {
        MainMenu.SetActive(false);
        LevelSelect.SetActive(true);
        LevelComplete.SetActive(false);
    }

    public void GoToMainMenu()
    {
        MainMenu.SetActive(true);
        LevelSelect.SetActive(false);
        LevelComplete.SetActive(false);
    }

    /*private IEnumerator AnimateMenus()
    {
        RectTransform mainMenuRect = MainMenu.GetComponent<RectTransform>();
        RectTransform levelSelectRect = LevelSelect.GetComponent<RectTransform>();

        Vector2 mainMenuOriginalPosition = mainMenuRect.anchoredPosition;
        Vector2 levelSelectOriginalPosition = levelSelectRect.anchoredPosition;

        Vector2 mainMenuTargetPosition = new Vector2(-levelSelectRect.anchoredPosition.x, mainMenuRect.anchoredPosition.y);
        Vector2 levelSelectTargetPosition = new Vector2(0, levelSelectRect.anchoredPosition.y);

        float elapsedTime = 0f;

        LevelSelect.SetActive(true);

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;

            mainMenuRect.anchoredPosition = Vector2.Lerp(mainMenuOriginalPosition, mainMenuTargetPosition, t);
            levelSelectRect.anchoredPosition = Vector2.Lerp(levelSelectOriginalPosition, levelSelectTargetPosition, t);

            yield return null;
        }

        MainMenu.SetActive(false);
    }

    private IEnumerator AnimateMenus2()
    {
        RectTransform mainMenuRect = MainMenu.GetComponent<RectTransform>();
        RectTransform levelSelectRect = LevelSelect.GetComponent<RectTransform>();

        Vector2 mainMenuOriginalPosition = mainMenuRect.anchoredPosition;
        Vector2 levelSelectOriginalPosition = levelSelectRect.anchoredPosition;

        Vector2 mainMenuTargetPosition = new Vector2(0, mainMenuRect.anchoredPosition.y);
        Vector2 levelSelectTargetPosition = new Vector2(-mainMenuRect.anchoredPosition.x, levelSelectRect.anchoredPosition.y);

        float elapsedTime = 0f;

        MainMenu.SetActive(true);

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;

            mainMenuRect.anchoredPosition = Vector2.Lerp(mainMenuOriginalPosition, mainMenuTargetPosition, t);
            levelSelectRect.anchoredPosition = Vector2.Lerp(levelSelectOriginalPosition, levelSelectTargetPosition, t);

            yield return null;
        }

        LevelSelect.SetActive(false);
    }*/
}
