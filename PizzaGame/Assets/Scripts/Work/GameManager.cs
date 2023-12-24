using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private void Start()
    {
        var currentScene = SceneManager.GetActiveScene();
        Initialization(currentScene, currentScene);
        LoadData();
    }

    private void LoadFloorLevels()
    {
        //TODO Подключиться к БД
        Debug.Log("Floors sets!");
        FloorsManager.FloorLevels.Add(0, 1);
        FloorsManager.FloorLevels.Add(1, 1);
        FloorsManager.FloorLevels.Add(2, 1);
    }

    private void LoadInventory()
    {
        //TODO Подключиться к БД
    }

    private void LoadRating()
    {
        //TODO Подключиться к БД
    }

    private void LoadBalance()
    {
        //TODO Подключиться к БД
    }

    private void LoadDays()
    {
        //TODO Подключиться к БД
    }

    private void LoadData()
    {
        LoadInventory();
        LoadRating();
        LoadBalance();
        LoadDays();
        LoadFloorLevels();
    }

    private void Initialization(Scene current, Scene next)
    {
        RatingManager.Instance.Initialization();
        MoneyManager.Instance.Initialization();
        WeatherControl.Instance.Initialization();
        Inventory.Instance.Initialization();
    }

    private void Update()
    {
        if (WeatherControl.Instance.Hour == 20)
        {
            DayToNight();
        }
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

        LoadScene("NightScene" + Random.Range(1, 3));
        WeatherControl.Instance.Hour = 24;
        WeatherControl.Instance.Minute = 0;
    }

    public void NightToDay()
    {
        WeatherControl.Instance.FreezeTime = false;
        LoadScene("DayScene");
        WeatherControl.Instance.Hour = 8;
        WeatherControl.Instance.Minute = 0;
    }
}
