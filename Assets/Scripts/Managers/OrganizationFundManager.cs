using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizationFundManager : MonoBehaviour
{

    private VilainOrganization vilainOrganization;
    private WorldTime worldTime;
    private float secondsElapsed = 0f;
    //Sixty minutes for an hour of work
    private readonly int MAXIMUM_FUND_TIME = 59;
    private readonly float MINIONS_FUND_RATE = 0.5f;
    private bool isAbleToRaiseFunds = true;

    // Start is called before the first frame update
    void Start()
    {
        VilainOrganization = GameObjectUtils.GetGameManagerScript().VilainOrganization;
        WorldTime = GameObjectUtils.GetTimeWeatherManagerScript().WorldTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isAbleToRaiseFunds)
        {
            SecondsElapsed += Time.deltaTime;

            if(SecondsElapsed >= 1)
            {
                isAbleToRaiseFunds = true;
                SecondsElapsed = 0;
            }
        }

        //hoursElapsed = WorldTime.Hours;
        if(WorldTime.Minutes == MAXIMUM_FUND_TIME && isAbleToRaiseFunds)
        {
            VilainOrganization.Fund += this.CalculateFundsAmountGained();
            isAbleToRaiseFunds = false;
        }
    }

    private float CalculateFundsAmountGained()
    {
        return VilainOrganization.MinionCount * MINIONS_FUND_RATE;
    }

    public VilainOrganization VilainOrganization { get => vilainOrganization; set => vilainOrganization = value; }
    public WorldTime WorldTime { get => worldTime; set => worldTime = value; }
    public float SecondsElapsed { get => secondsElapsed; set => secondsElapsed = value; }
}
