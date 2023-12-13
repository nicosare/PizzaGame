using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsGamePaused;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.activeSceneChanged += Initialization;
    }

    void Start()
    {
        var currentScene = SceneManager.GetActiveScene();
        Initialization(currentScene, currentScene);
    }

    void Initialization(Scene current, Scene next)
    {
        RatingManager.Instance.FindTextField();
        MoneyManager.Instance.FindTextField();
    }


    private void Update()
    {
        if (WeatherControl.Instance.Hour == 20)
            DayToNight();
    }

    public void PauseGame()
    {
        IsGamePaused = true;
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        IsGamePaused = false;
        Time.timeScale = 1;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    private IEnumerator LoadAsyncScene(string sceneName)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
            yield return null;
    }

    private void DayToNight()
    {
        WeatherControl.Instance.FreezeTime = true;
        WeatherControl.Instance.Hour = 0;
        LoadScene("NightScene");
    }

    private void NightToDay()
    {
        WeatherControl.Instance.FreezeTime = false;
        WeatherControl.Instance.Hour = 8;
        LoadScene("DayScene");
    }
}
