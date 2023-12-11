using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class WeatherControl : MonoBehaviour
{
    public static WeatherControl Instance;
    [Header("Light")]
    [SerializeField] private Light sunLight;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI clockText;
    [SerializeField] private TextMeshProUGUI calendarText;

    [Header("Time")]
    public int Hour = 0;
    [SerializeField] private int minute = 0;
    [SerializeField] private float secondsPerMinute = 1f;
    [SerializeField] private bool realTimeClock = false;


    [Header("Day Colors")]
    [SerializeField] private Color morningLight = new Color(1f, 0.9280525f, 0.6289308f, 1f);
    [SerializeField] private Color eveningLight = new Color(0.4921679f, 0.5660581f, 0.990566f);
    [SerializeField] private Color nightLight = new Color(0.4921679f, 0.5660581f, 0.990566f);

    [Header("Date")]
    [SerializeField] private bool weeksAndMonths = true;
    [SerializeField] private int currentDay = 1;
    [SerializeField] private int currentMonth = 1;
    [SerializeField] private Months months;

    private Season currentSeason;

    [Header("Seasons")]
    [SerializeField] private bool seasons = true;
    [Range(0, 1)]
    [SerializeField] private float interpolationFactor = 0.3f;
    [SerializeField] private SeasonsSettings seasonsSettings;
    [SerializeField] private NoSeasonsSettings noSeasonsSettings;


    private List<ParticleSystem> weatherParticleSystemOptions;

    private List<float> weatherProbabilities;

    private int season = 1;
    private int weekDay = 1;
    private Color weatherColor = Color.white;
    private float lerpNormalization;
    private bool launchCoroutine = false;
    private Phenomenon currentPhenomenon;

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
    }

    void Start()
    {
        if (realTimeClock)
        {
            Hour = System.DateTime.Now.Hour;
            minute = System.DateTime.Now.Minute;
            currentMonth = System.DateTime.Now.Month;
            currentDay = System.DateTime.Now.Day;
        }
        if (!seasons)
        {
            weatherProbabilities = noSeasonsSettings.GetNoSeason().GetProbabilities();
            weatherParticleSystemOptions = noSeasonsSettings.GetNoSeason().GetParticleSystems();
        }

        launchCoroutine = true;
    }

    void Update()
    {
        TimeAndCalendarFormatChecking();

        if (launchCoroutine && !realTimeClock)
        {
            launchCoroutine = false;
            StartCoroutine("ClockSimulation");
        }
        WeatherTimeController();

        if (seasons)
        {
            SetSeason();
        }
    }


    IEnumerator ClockSimulation()
    {
        yield return new WaitForSeconds(secondsPerMinute);

        minute = minute + 1;

        if (minute == 60)
        {
            minute = 0;
            Hour = Hour + 1;
        }

        launchCoroutine = true;
    }

    static int GetIntegerDigitCountString(int value)
    {
        return value.ToString().Length;
    }

    private void RealTimeController()
    {
        TimeController();
        WeatherController();
    }

    private void SimulatedTimeController()
    {
        TimeController();
        WeatherController();
    }

    private void TimeAndCalendarFormatChecking()
    {
        string minuteString = "";
        string hourString = "";

        if (realTimeClock)
        {
            Hour = System.DateTime.Now.Hour;
            minute = System.DateTime.Now.Minute;
            currentMonth = System.DateTime.Now.Month;
            currentDay = System.DateTime.Now.Day;
        }

        if (GetIntegerDigitCountString(minute) == 1)
        {
            minuteString = "0" + minute.ToString();
        }
        else
        {
            minuteString = minute.ToString();
        }

        if (GetIntegerDigitCountString(Hour) == 1)
        {
            hourString = "0" + Hour.ToString();
        }
        else
        {
            hourString = Hour.ToString();
        }

        clockText.text = hourString + ":" + minuteString;

        if (weeksAndMonths)
        {
            calendarText.text = months.GetMonths()[currentMonth - 1].GetName() + ", " + currentDay + "\n" + GetRusName();
        }
        else
        {
            calendarText.text = "День: " + currentDay;
        }
    }
    private string GetRusName()
    {
        switch (weekDay - 1)
        {
            case (int)Days.Monday:
                return "Понедельник";
            case (int)Days.Tuesday:
                return "Вторник";
            case (int)Days.Wednesday:
                return "Среда";
            case (int)Days.Thursday:
                return "Четверг";
            case (int)Days.Friday:
                return "Пятница";
            case (int)Days.Saturday:
                return "Суббота";
            case (int)Days.Sunday:
                return "Воскресенье";
            default:
                return "";
        }
    }

    private void SetSeason()
    {
        if (currentMonth <= 0)
        {
            currentMonth = 1;
        }
        else if (currentMonth > months.GetNumberOfMonths())
        {
            currentMonth = months.GetNumberOfMonths();
        }

        if (currentMonth >= seasonsSettings.GetSpring().GetStartMonth() && currentMonth <= seasonsSettings.GetSpring().GetEndMonth())
        {
            season = 1;
            currentSeason = seasonsSettings.GetSpring();
            weatherProbabilities = seasonsSettings.GetSpring().GetProbabilities();
            weatherParticleSystemOptions = seasonsSettings.GetSpring().GetParticleSystems();
        }
        else if (currentMonth >= seasonsSettings.GetSummer().GetStartMonth() && currentMonth <= seasonsSettings.GetSummer().GetEndMonth())
        {
            season = 2;
            currentSeason = seasonsSettings.GetSummer();
            weatherProbabilities = seasonsSettings.GetSummer().GetProbabilities();
            weatherParticleSystemOptions = seasonsSettings.GetSummer().GetParticleSystems();
        }
        else if (currentMonth >= seasonsSettings.GetAutumn().GetStartMonth() && currentMonth <= seasonsSettings.GetAutumn().GetEndMonth())
        {
            season = 3;
            currentSeason = seasonsSettings.GetAutumn();
            weatherProbabilities = seasonsSettings.GetAutumn().GetProbabilities();
            weatherParticleSystemOptions = seasonsSettings.GetAutumn().GetParticleSystems();
        }
        else
        {
            season = 4;
            currentSeason = seasonsSettings.GetWinter();
            weatherProbabilities = seasonsSettings.GetWinter().GetProbabilities();
            weatherParticleSystemOptions = seasonsSettings.GetWinter().GetParticleSystems();
        }
    }

    private void TimeController()
    {

        float morningHour = 8;
        float eveningHour = 18;
        float nightHour = 21;

        if (currentSeason != null)
        {
            morningHour = currentSeason.GetMorningHour();
            eveningHour = currentSeason.GetEveningHour();
        }

        if (Hour >= 0 && Hour < morningHour)
        {
            lerpNormalization = (Hour - 0f) / (morningHour - 0f);
            lerpNormalization = lerpNormalization * lerpNormalization * (3f - 2f * lerpNormalization);
            weatherColor = Color.Lerp(nightLight, morningLight, lerpNormalization);
        }
        else if (Hour >= morningHour && Hour <= eveningHour)
        {
            lerpNormalization = (Hour - morningHour) / (eveningHour - morningHour);
            lerpNormalization = lerpNormalization * lerpNormalization * (3f - 2f * lerpNormalization);
            weatherColor = Color.Lerp(morningLight, eveningLight, lerpNormalization);
        }
        else if (Hour > eveningHour && Hour <= nightHour)
        {
            lerpNormalization = (Hour - eveningHour) / (nightHour - eveningHour);
            lerpNormalization = lerpNormalization * lerpNormalization * (3f - 2f * lerpNormalization);
            weatherColor = Color.Lerp(eveningLight, nightLight, lerpNormalization);

        }
        else if (Hour > nightHour && Hour <= 24)
        {
            lerpNormalization = (Hour - nightHour) / (24f - nightHour);
            lerpNormalization = lerpNormalization * lerpNormalization * (3f - 2f * lerpNormalization);
            weatherColor = Color.Lerp(nightLight, nightLight, lerpNormalization);

        }
    }

    private void WeatherController()
    {

        if (currentPhenomenon != null && currentPhenomenon.GetParticleSystem().isPlaying)
        {
            weatherColor = Color.Lerp(weatherColor, currentPhenomenon.GetColor(), interpolationFactor);
        }

        sunLight.color = weatherColor;

        if (Hour >= 24)
        {
            Hour = 0;
            currentDay = currentDay + 1;
            weekDay = weekDay + 1;

            float weatherValue = UnityEngine.Random.value;
            float[] probabilities = new float[weatherParticleSystemOptions.Count];

            for (int i = 0; i < weatherParticleSystemOptions.Count; i++)
            {
                probabilities[i] = weatherProbabilities[i] - weatherValue;
            }

            var probabilitiesPassed = from value in probabilities where value > 0 select value;

            if (probabilitiesPassed.Count() > 0)
            {
                if (currentPhenomenon != null && currentPhenomenon.GetParticleSystem().isPlaying)
                {
                    currentPhenomenon.GetParticleSystem().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
                var diffList = probabilitiesPassed.Select(x => new { n = x, diff = Math.Abs(x - 0f) });
                var result = diffList.Where(x => x.diff == diffList.Select(y => y.diff).Min()).First();
                weatherParticleSystemOptions[Array.IndexOf(probabilities, result.n)].Play();
                if (!seasons)
                {
                    currentPhenomenon = noSeasonsSettings.GetNoSeason().GetPhenomenons()[Array.IndexOf(probabilities, result.n)];
                }
                else if (season == 1)
                {
                    currentPhenomenon = seasonsSettings.GetSpring().GetPhenomenons()[Array.IndexOf(probabilities, result.n)];
                }
                else if (season == 2)
                {
                    currentPhenomenon = seasonsSettings.GetSummer().GetPhenomenons()[Array.IndexOf(probabilities, result.n)];
                }
                else if (season == 3)
                {
                    currentPhenomenon = seasonsSettings.GetAutumn().GetPhenomenons()[Array.IndexOf(probabilities, result.n)];
                }
                else if (season == 4)
                {
                    currentPhenomenon = seasonsSettings.GetWinter().GetPhenomenons()[Array.IndexOf(probabilities, result.n)];
                }
            }
            else
            {
                if (currentPhenomenon != null && currentPhenomenon.GetParticleSystem().isPlaying)
                {
                    currentPhenomenon.GetParticleSystem().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
        }

        if (currentDay > months.GetMonths()[currentMonth - 1].GetDays())
        {
            if (weeksAndMonths)
                currentMonth = currentMonth + 1;
            currentDay = 1;
        }

        if (weekDay > 7 && weeksAndMonths)
        {
            weekDay = 1;
        }

        if (currentMonth > months.GetNumberOfMonths() && weeksAndMonths)
        {
            currentMonth = 1;
        }
    }

    private void WeatherTimeController()
    {
        TimeController();
        WeatherController();
    }
}