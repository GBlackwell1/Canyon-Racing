using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool LevelComplete { get; private set; } = false;
    private LevelData levelData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            levelData = new LevelData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToLevel(int level)
    {
        Debug.Log("Loading level " + level);
        switch (level)
        {
            case 1:
                SceneManager.LoadScene("Level 1");
                break;
            case 2:
                SceneManager.LoadScene("Level 2");
                break;
            case 3:
                SceneManager.LoadScene("Level 3");
                break;
            case 4:
                SceneManager.LoadScene("Level 4");
                break;
            case 5:
                SceneManager.LoadScene("Level 5");
                break;
            case 6:
                SceneManager.LoadScene("Level 6");
                break;
            case 7:
                SceneManager.LoadScene("Level 7");
                break;
            case 8:
                SceneManager.LoadScene("Level 8");
                break;
            default:
                SceneManager.LoadScene("Main Menu");
                break;
        }
        SceneManager.LoadScene(level);
    }

    public void RestartLevel()
    {
        int levelIndex = int.Parse(levelData.level[^1].ToString());
        GoToLevel(levelIndex);
    }

    public void GoToNextLevel()
    {
        int levelIndex = int.Parse(levelData.level[^1].ToString());
        if (levelIndex < 8)
        {
            GoToLevel(levelIndex + 1);
        }
        else
        {
            GoToLevel(0);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SaveLevelData(string level, float time, int score = 0)
    {
        LevelComplete = true;
        levelData.level = level;
        levelData.time = time;
        levelData.score = score;
    }

    public LevelData GetLevelData()
    {
        return levelData;
    }
}

public struct LevelData
{
    public string level;
    public float time;
    public int score;
}