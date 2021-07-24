using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WorldTime 
{
    [SerializeField]
    private int dayCount = 0;
    [SerializeField]
    private int hours = 0;
    [SerializeField]
    private int minutes = 0;
    [SerializeField]
    private int seconds = 0;

    public WorldTime(int dayCount, int hours, int minutes, int seconds)
    {
        this.DayCount = dayCount;
        this.Hours = hours;
        this.Minutes = minutes;
        this.Seconds = seconds;
    }

    public int DayCount { get => dayCount; set => dayCount = value; }
    public int Hours { get => hours; set => hours = value; }
    public int Minutes { get => minutes; set => minutes = value; }
    public int Seconds { get => seconds; set => seconds = value; }
}
