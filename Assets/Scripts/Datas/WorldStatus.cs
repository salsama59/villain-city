using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStatus
{
    private float criminalityRate = 0f;
    private float wealth = 0f;

    public WorldStatus(float criminalityRate, float wealth)
    {
        CriminalityRate = criminalityRate;
        Wealth = wealth;
    }

    public float CriminalityRate { get => criminalityRate; set => criminalityRate = value; }
    public float Wealth { get => wealth; set => wealth = value; }
}
