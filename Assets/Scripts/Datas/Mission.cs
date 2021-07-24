using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Mission
{
    [SerializeField]
    private int missionId;
    [SerializeField]
    private int missionMinimalLevelRequirement = 1;
    [SerializeField]
    private string missionTitle = "";
    [SerializeField]
    private string missionDescription = "";
    [SerializeField]
    private WorldTime missionDuration = null;
    [SerializeField]
    private WorldTime missionValidityTimeLimit = null;
    [SerializeField]
    private bool isMissionActive = false;
    [SerializeField]
    private MissionReward missionReward;


    public Mission(int missionMinimalLevelRequirement, string missionTitle, WorldTime missionDuration, WorldTime missionValidityTimeLimit, string missionDescription, bool isMissionActive, MissionReward missionReward, int missionId)
    {
        MissionMinimalLevelRequirement = missionMinimalLevelRequirement;
        MissionTitle = missionTitle;
        MissionDuration = missionDuration;
        MissionValidityTimeLimit = missionValidityTimeLimit;
        MissionDescription = missionDescription;
        IsMissionActive = isMissionActive;
        MissionReward = missionReward;
        MissionId = missionId;
    }

    public int MissionMinimalLevelRequirement { get => missionMinimalLevelRequirement; set => missionMinimalLevelRequirement = value; }
    public string MissionTitle { get => missionTitle; set => missionTitle = value; }
    public WorldTime MissionDuration { get => missionDuration; set => missionDuration = value; }
    public WorldTime MissionValidityTimeLimit { get => missionValidityTimeLimit; set => missionValidityTimeLimit = value; }
    public string MissionDescription { get => missionDescription; set => missionDescription = value; }
    public bool IsMissionActive { get => isMissionActive; set => isMissionActive = value; }
    public MissionReward MissionReward { get => missionReward; set => missionReward = value; }
    public int MissionId { get => missionId; set => missionId = value; }
}
