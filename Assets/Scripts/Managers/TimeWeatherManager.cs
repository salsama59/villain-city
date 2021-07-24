using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeWeatherManager : MonoBehaviour
{

    public static readonly int MAX_HOURS_IN_DAY = 24;
    public static readonly int MAX_MINUTES_IN_HOUR = 60;
    public static readonly int MAX_SECOND_IN_MINUTE = 60;

    private WorldTime worldTime;

    private float elapsedTime = 0f;

    //6 minutes in real world is one day in the game
    public static readonly float TIME_ACCELERATION_RATE = 240f;

    private TextMeshProUGUI worldTimeText;
    private TextMeshProUGUI worldDayText;

    // Start is called before the first frame update
    void Start()
    {
        WorldTime = new WorldTime(0, 0, 0, 0);
        WorldTimeText = GameObjectUtils.GetWorldTimeTextComponentScript();
        WorldDayText = GameObjectUtils.GetWorldDayTextComponentScript();
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;
        this.UpdateWorldTime();
    }

    private void UpdateWorldTime()
    {
        if(ElapsedTime >= (1 / TIME_ACCELERATION_RATE))
        {
            WorldTime.Seconds += 1;

            if(WorldTime.Seconds == MAX_SECOND_IN_MINUTE)
            {
                WorldTime.Minutes += 1;
                WorldTime.Seconds = 0;

                if(WorldTime.Minutes == MAX_MINUTES_IN_HOUR)
                {
                    WorldTime.Hours += 1;
                    WorldTime.Minutes = 0;


                    if (WorldTime.Hours == MAX_HOURS_IN_DAY)
                    {
                        WorldTime.DayCount += 1;
                        WorldTime.Hours = 0;
                    }
                }
            }

            ElapsedTime = 0f;

            WorldTimeText.text = this.BuildTimeDisplayText();
            WorldDayText.text = this.BuildDayDisplayText();
        }
    }


    private string BuildTimeDisplayText()
    {
        string hours = WorldTime.Hours.ToString();
        string minutes = WorldTime.Minutes.ToString();
        string seconds = WorldTime.Seconds.ToString();

        if (hours.Length == 1)
        {
            hours = "0" + hours;
        }

        if (minutes.Length == 1)
        {
            minutes = "0" + minutes;
        }

        if (seconds.Length == 1)
        {
            seconds = "0" + seconds;
        }

        return hours + ":" + minutes + ":" + seconds;
    }

    private string BuildDayDisplayText()
    {
        return "Day " + WorldTime.DayCount;
    }

    public WorldTime WorldTime { get => worldTime; set => worldTime = value; }
    public float ElapsedTime { get => elapsedTime; set => elapsedTime = value; }
    public TextMeshProUGUI WorldTimeText { get => worldTimeText; set => worldTimeText = value; }
    public TextMeshProUGUI WorldDayText { get => worldDayText; set => worldDayText = value; }
}
