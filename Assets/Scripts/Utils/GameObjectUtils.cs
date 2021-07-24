using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameObjectUtils : MonoBehaviour
{
    public static GameManager GetGameManagerScript()
    {
        return GameObject.FindGameObjectWithTag(TagConstants.GAME_MANAGER_TAG).GetComponent<GameManager>();
    }

    public static WorldManager GetWorldManagerScript()
    {
        return GameObject.FindGameObjectWithTag(TagConstants.WORLD_MANAGER_TAG).GetComponent<WorldManager>();
    }

    public static TextMeshProUGUI GetOrganizationLevelTextComponentScript()
    {
        return GameObject.FindGameObjectWithTag(TagConstants.ORGANIZATION_LEVEL_TEXT_TAG).GetComponent<TextMeshProUGUI>();
    }

    public static TextMeshProUGUI GetOrganizationFundTextComponentScript()
    {
        return GameObject.FindGameObjectWithTag(TagConstants.ORGANIZATION_FUND_TEXT_TAG).GetComponent<TextMeshProUGUI>();
    }

    public static TextMeshProUGUI GetOrganizationMinionCoutTextComponentScript()
    {
        return GameObject.FindGameObjectWithTag(TagConstants.ORGANIZATION_VILAIN_COUNT_TEXT_TAG).GetComponent<TextMeshProUGUI>();
    }

    public static TextMeshProUGUI GetWorldTimeTextComponentScript()
    {
        return GameObject.FindGameObjectWithTag(TagConstants.WORLD_TIME_TEXT_TAG).GetComponent<TextMeshProUGUI>();
    }

    public static TextMeshProUGUI GetWorldDayTextComponentScript()
    {
        return GameObject.FindGameObjectWithTag(TagConstants.WORLD_DAY_TEXT_TAG).GetComponent<TextMeshProUGUI>();
    }

    public static TimeWeatherManager GetTimeWeatherManagerScript()
    {
        return GameObject.FindGameObjectWithTag(TagConstants.TIME_WEATHER_MANAGER_TAG).GetComponent<TimeWeatherManager>();
    }

    public static GameObject GetActionMenuCanvas()
    {
        return GameObject.FindGameObjectWithTag(TagConstants.ACTION_MENU_CANVAS_TAG);
    }

    public static GameObject GetVillainOrganizationObject()
    {
        return GameObject.FindGameObjectWithTag(TagConstants.VILLAIN_ORGANIZATION_TAG);
    }

}
