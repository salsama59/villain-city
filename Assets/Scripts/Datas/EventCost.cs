using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EventCost
{
    [SerializeField]
    private float criminalityRate;
    [SerializeField]
    private float worldWealth;

    public float CriminalityRate { get => criminalityRate; set => criminalityRate = value; }
    public float WorldWealth { get => worldWealth; set => worldWealth = value; }
}
