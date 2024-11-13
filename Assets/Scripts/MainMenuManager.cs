using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void GoToMainMenu()
    {
        MainMenu.SetActive(true);
        LevelSelect.SetActive(false);
        LevelComplete.SetActive(false);
    }

    public void GoToLevelSelect()
    {
        MainMenu.SetActive(false);
        LevelSelect.SetActive(true);
        LevelComplete.SetActive(false);
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
}
