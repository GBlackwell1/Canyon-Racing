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
    public GameObject TotalTime;
    public GameObject Camera;
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
            TotalTime.GetComponent<TMPro.TextMeshProUGUI>().SetText("Time: " + time);
        }
        else
        {
            MainMenu.SetActive(true);
        }
    }

    private void Update()
    {
        var newTransform = Camera.transform.rotation.eulerAngles;
        newTransform.y += Time.deltaTime;
        Camera.transform.rotation = Quaternion.Euler(newTransform);
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
