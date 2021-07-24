using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MissionReward
{
    [SerializeField]
    private float missionCrimeCurrencydReward = 0f;
    [SerializeField]
    private int minionGain = 0;
    [SerializeField]
    private List<StolenObject> stolenObjects;

    public MissionReward(float missionCrimeCurrencydReward, int minionGain, List<StolenObject> stolenObjects)
    {
        MissionCrimeCurrencydReward = missionCrimeCurrencydReward;
        MinionGain = minionGain;
        StolenObjects = stolenObjects;
    }

    public float MissionCrimeCurrencydReward { get => missionCrimeCurrencydReward; set => missionCrimeCurrencydReward = value; }
    public int MinionGain { get => minionGain; set => minionGain = value; }
    public List<StolenObject> StolenObjects { get => stolenObjects; set => stolenObjects = value; }
}
