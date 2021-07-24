using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MissionManager : MonoBehaviour
{

    private List<Mission> missions;

    public GameObject missionListWindowModel;

    private GameObject missionListWindow;

    public GameObject missionElementTextModel;

    public GameObject missionDetailMenuModel;
    private GameObject missionDetailMenu;

    public GameObject missionDetailElementModel;

    private GameObject missionDetailLevelRequirementText;
    private GameObject missionDetailTitleText;
    private GameObject missionDetailDurationText;
    private GameObject missionDetailValidityText;
    private GameObject missionDetailDescriptionText;
    private GameObject missionDetailStatusText;

    private List<Mission> inProgressMissions;
    private List<Mission> finishedMissions;

    private int currentSelectedMissionId = -1;

    private List<GameObject> missionElementGameObjectList;

    public GameObject missionListBackButtonModel;

    private GameObject missionListBackButtonGameObject;

    private VilainOrganization vilainOrganization;

    private readonly float MAX_MISSION_REFRESH_RATE = (float) (TimeWeatherManager.MAX_HOURS_IN_DAY * TimeWeatherManager.MAX_MINUTES_IN_HOUR * TimeWeatherManager.MAX_SECOND_IN_MINUTE) / TimeWeatherManager.TIME_ACCELERATION_RATE;

    private GameObject actionMenuCanvas;

    public GameObject missionDetailButtonGroupModel;
    private GameObject missionDetailButtonGroup;

    private List<Mission> missionPool;

    // Start is called before the first frame update
    void Start()
    {
        missionPool = new List<Mission>(this.LoadMissionsDatas());
        actionMenuCanvas = GameObjectUtils.GetActionMenuCanvas();
        VilainOrganization = GameObjectUtils.GetGameManagerScript().VilainOrganization;
        missionElementGameObjectList = new List<GameObject>();
        missions = new List<Mission>();
        finishedMissions = new List<Mission>();
        inProgressMissions = new List<Mission>();
        this.RefreshMissions();
    }

    private Mission[] LoadMissionsDatas()
    {
        string jsonTeamDatasPath = Application.dataPath + "/GameDatas/MissionDatas.json";
        string jsonString = File.ReadAllText(jsonTeamDatasPath);
        return JsonUtils.FromJson<Mission>("{\n\t\"items\":\t" + jsonString + "\n}");
    }

    private void GenerateMissions(int numberOfMissionsToAdd)
    {
        List<Mission> eligibleMissions = new List<Mission>(this.missionPool.Where((missionElement) => { return VilainOrganization.Level >= missionElement.MissionMinimalLevelRequirement; }));
        for(int i = 0; i < numberOfMissionsToAdd; i++)
        {
            int chosenIndex = Random.Range(0, eligibleMissions.Count);
           /* List<StolenObject> stolenObjects = new List<StolenObject>();
            StolenObject stolenObject = new StolenObject("Ring", 5f);
            stolenObjects.Add(stolenObject);
            MissionReward missionReward = new MissionReward(10f, 1, stolenObjects);
            new Mission(1, "Corrupt a police officer", new WorldTime(0, 8, 0, 0), new WorldTime(1, 0, 0, 0), "The cops love donuts." + Environment.NewLine + "Give them some", false, missionReward)*/
            missions.Add(eligibleMissions[chosenIndex]);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void RefreshMissions()
    {
        int maximumMissionsProposed = VilainOrganization.Level * 3;

        if(missions.Count < maximumMissionsProposed)
        {
            int numberOfMissionToAdd = maximumMissionsProposed - missions.Count;
            this.GenerateMissions(numberOfMissionToAdd);
        }
        TimedTaskExecutor.Create(() => { RefreshMissions(); }, MAX_MISSION_REFRESH_RATE);
    }

    public void OpenMissionListMenu()
    {

        if (missionListWindow == null)
        {
            missionListWindow = Instantiate(missionListWindowModel, actionMenuCanvas.transform);
        }
        
        for (int i = 0; i < missions.Count; i++)
        {
            int currentIndex = i;
            
            Mission mission = missions[currentIndex];
            int missionId = mission.MissionId;
            GameObject missionElementGameObject = Instantiate(missionElementTextModel, missionListWindow.transform);
            missionElementGameObject.GetComponentInChildren<TextMeshProUGUI>().text = mission.MissionTitle + Environment.NewLine + "Level required : " + mission.MissionMinimalLevelRequirement;
            if(VilainOrganization.Level >= mission.MissionMinimalLevelRequirement)
            {
                this.AddListenerToButton(missionId, missionElementGameObject);
            }
            missionElementGameObjectList.Add(missionElementGameObject);
            missionElementGameObject.SetActive(true);
        }

        if(missionListBackButtonGameObject == null)
        {
            missionListBackButtonGameObject = Instantiate(missionListBackButtonModel, this.transform.position, Quaternion.identity);
            missionListBackButtonGameObject.transform.SetParent(missionListWindow.transform);
            missionListBackButtonGameObject.GetComponent<Button>().onClick.AddListener(() => { this.BackToActionMenu(); });
        }

        missionListWindow.SetActive(true);
    }

    private void AddListenerToButton(int missionId, GameObject missionElementGameObject)
    {
        missionElementGameObject.GetComponent<Button>().onClick.AddListener(() =>{
            this.OpenMissionDetailMenu(missionId);});
    }

    public void OpenMissionDetailMenu(int missionId)
    {
        if (missionDetailMenu == null)
        {
            missionDetailMenu = Instantiate(missionDetailMenuModel, actionMenuCanvas.transform);
        }

        currentSelectedMissionId = missionId;
        Mission mission = missions.Where((missionElement) => { return missionElement.MissionId == currentSelectedMissionId; }).First();
        missionDetailMenu.SetActive(true);


        if(missionDetailLevelRequirementText == null)
        {
            missionDetailLevelRequirementText = Instantiate(missionDetailElementModel, this.transform.position, Quaternion.identity);
            missionDetailLevelRequirementText.transform.SetParent(missionDetailMenu.transform);
        }

        if (missionDetailTitleText == null)
        {
            missionDetailTitleText = Instantiate(missionDetailElementModel, this.transform.position, Quaternion.identity);
            missionDetailTitleText.transform.SetParent(missionDetailMenu.transform);
        }

        if (missionDetailDurationText == null)
        {
            missionDetailDurationText = Instantiate(missionDetailElementModel, this.transform.position, Quaternion.identity);
            missionDetailDurationText.transform.SetParent(missionDetailMenu.transform);
        }

        if (missionDetailValidityText == null)
        {
            missionDetailValidityText = Instantiate(missionDetailElementModel, this.transform.position, Quaternion.identity);
            missionDetailValidityText.transform.SetParent(missionDetailMenu.transform);
        }

        if (missionDetailDescriptionText == null)
        {
            missionDetailDescriptionText = Instantiate(missionDetailElementModel, this.transform.position, Quaternion.identity);
            missionDetailDescriptionText.transform.SetParent(missionDetailMenu.transform);
        }

        if (missionDetailStatusText == null)
        {
            missionDetailStatusText = Instantiate(missionDetailElementModel, this.transform.position, Quaternion.identity);
            missionDetailStatusText.transform.SetParent(missionDetailMenu.transform);
        }

        missionDetailLevelRequirementText.GetComponent<TextMeshProUGUI>().text = mission.MissionMinimalLevelRequirement.ToString();
        missionDetailTitleText.GetComponent<TextMeshProUGUI>().text = mission.MissionTitle;
        missionDetailDurationText.GetComponent<TextMeshProUGUI>().text = "Mission take :" + Environment.NewLine + mission.MissionDuration.DayCount + " d " + mission.MissionDuration.Hours + " h " + mission.MissionDuration.Minutes + " m " + mission.MissionDuration.Seconds + " s to complete";
        missionDetailValidityText.GetComponent<TextMeshProUGUI>().text = "Mission validity last for :" + Environment.NewLine + mission.MissionValidityTimeLimit.DayCount + " d " + mission.MissionValidityTimeLimit.Hours + " h " + mission.MissionValidityTimeLimit.Minutes + " m " + mission.MissionValidityTimeLimit.Seconds + " s";
        missionDetailDescriptionText.GetComponent<TextMeshProUGUI>().text = mission.MissionDescription;
        missionDetailStatusText.GetComponent<TextMeshProUGUI>().text = "Mission is : " + (mission.IsMissionActive ? "active" : "inactive");


        if(missionDetailButtonGroup == null)
        {
            missionDetailButtonGroup = Instantiate(missionDetailButtonGroupModel, missionDetailMenu.transform);

           Button[] buttons = missionDetailButtonGroup.GetComponentsInChildren<Button>();

            foreach (Button button in buttons)
            {
                if (button.gameObject.CompareTag(TagConstants.ACCEPT_MISSION_BUTTON_TAG))
                {
                    button.onClick.AddListener(() => { this.AcceptMission();});
                } else if (button.gameObject.CompareTag(TagConstants.CANCEL_MISSION_BUTTON_TAG))
                {
                    button.onClick.AddListener(() => { this.BackToMissionListMenu(); });
                }
            }
        }
        
    }

    public void AcceptMission()
    {
        Mission missionToAccept = missions.Where((missionElement) => { return missionElement.MissionId == currentSelectedMissionId; }).First();
        inProgressMissions.Add(missionToAccept);
        int missionToAcceptIndex = missions.IndexOf(missionToAccept);
        missions.Remove(missionToAccept);
        Destroy(missionElementGameObjectList[missionToAcceptIndex]);
        missionElementGameObjectList.RemoveAt(missionToAcceptIndex);
        missionDetailMenu.SetActive(false);
        if(missions.Count == 0)
        {
            missionListWindow.SetActive(false);
        }
        currentSelectedMissionId = -1;

        this.BeginMission(inProgressMissions[inProgressMissions.Count - 1]);
    }

    public void BackToMissionListMenu()
    {
        missionDetailMenu.SetActive(false);
        currentSelectedMissionId = -1;
    }

    public void BackToActionMenu()
    {
        missionListWindow.SetActive(false);

        if(missionDetailMenu != null)
        {
            missionDetailMenu.SetActive(false);
        }
        
        foreach (GameObject missionElementGameObject in missionElementGameObjectList)
        {
            Destroy(missionElementGameObject);
        }
        missionElementGameObjectList.Clear();

        Destroy(missionListBackButtonGameObject);
        missionListBackButtonGameObject = null;
    }


    public void BeginMission(Mission missionToStart)
    {
        float timeToExecute = (float)(missionToStart.MissionDuration.Seconds + missionToStart.MissionDuration.Minutes * TimeWeatherManager.MAX_SECOND_IN_MINUTE + missionToStart.MissionDuration.Hours * TimeWeatherManager.MAX_MINUTES_IN_HOUR * TimeWeatherManager.MAX_SECOND_IN_MINUTE + missionToStart.MissionDuration.DayCount * TimeWeatherManager.MAX_HOURS_IN_DAY * TimeWeatherManager.MAX_MINUTES_IN_HOUR * TimeWeatherManager.MAX_SECOND_IN_MINUTE) / TimeWeatherManager.TIME_ACCELERATION_RATE;
        Debug.Log("Begin mission : " + missionToStart.MissionTitle + " for " + timeToExecute + "seconds");
        missionToStart.IsMissionActive = true;
        TimedTaskExecutor.Create(() => { EndMission(missionToStart); }, timeToExecute);
        Debug.Log("Mission : " + missionToStart.MissionTitle + " in progress");
    }

    public void EndMission(Mission missionStarted)
    {
        finishedMissions.Add(missionStarted);
        inProgressMissions.Remove(missionStarted);
        VilainOrganization.OwnedObjects.AddRange(missionStarted.MissionReward.StolenObjects);
        VilainOrganization.Fund += missionStarted.MissionReward.MissionCrimeCurrencydReward;
        VilainOrganization.MinionCount += missionStarted.MissionReward.MinionGain;
        Debug.Log("Mission " + missionStarted.MissionTitle + " ended");
    }

    public VilainOrganization VilainOrganization { get => vilainOrganization; set => vilainOrganization = value; }
}
