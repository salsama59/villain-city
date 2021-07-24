using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    private WorldStatus worldStatus;
    private List<WorldEvent> eventsQueue;
    private List<WorldEvent> eventPool;
    private bool isEventQueueEmpty = false;
    private readonly float MAX_EVENT_REFRESH_RATE = (float)(TimeWeatherManager.MAX_HOURS_IN_DAY * TimeWeatherManager.MAX_MINUTES_IN_HOUR * TimeWeatherManager.MAX_SECOND_IN_MINUTE) / TimeWeatherManager.TIME_ACCELERATION_RATE;
    public GameObject eventCanvasModel;
    private GameObject eventCanvas;
    public GameObject textElementModel;
    private GameObject eventTitleText;
    private GameObject eventDescriptionText;
    public GameObject buttonGroupModel;
    private GameObject buttonGroup;

    // Start is called before the first frame update
    void Start()
    {
        worldStatus = GameObjectUtils.GetWorldManagerScript().WorldStatus;
        eventPool = new List<WorldEvent>(this.LoadEventsDatas());
        TimedTaskExecutor.Create(() => { this.RefreshEvents(); }, MAX_EVENT_REFRESH_RATE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private WorldEvent[] LoadEventsDatas()
    {
        string eventDataJsonPath = Application.dataPath + "/GameDatas/EventsDatas.json";
        string jsonString = File.ReadAllText(eventDataJsonPath);
        return JsonUtils.FromJson<WorldEvent>("{\n\t\"items\":\t" + jsonString + "\n}");
    }

    private void RefreshEvents()
    {
        eventsQueue = new List<WorldEvent>();

        int maximumEventsInQueue = 3;

        if (eventsQueue.Count < maximumEventsInQueue)
        {
            int numberOfEventToAdd = maximumEventsInQueue - eventsQueue.Count;
            this.AddEventToQueue(numberOfEventToAdd);
        }
        if (!isEventQueueEmpty)
        {
            TimedTaskExecutor.Create(() => { RefreshEvents(); }, MAX_EVENT_REFRESH_RATE);
        }

        if(eventsQueue.Count > 0)
        {
            this.DisplayEventPanel();
        }
        
    }

    private void AddEventToQueue(int numberOfEventToAdd)
    {
        for(int i = 0; i < numberOfEventToAdd; i++)
        {
            WorldEvent eventToAdd = this.GetNextEventFromPool();

            if(eventToAdd == null)
            {
                isEventQueueEmpty = true;
                break;
            } else
            {
                eventsQueue.Add(eventToAdd);
            }
           
        }
       
    }

    private WorldEvent GetNextEventFromPool()
    {
        if(eventPool.Count > 0)
        {
            return eventPool[0];
        } else
        {
            return null;
        }
    }

    private void RemoveEventFromPool()
    {
        eventPool.RemoveAt(0);
    }

    private void DisplayEventPanel()
    {

        WorldEvent eventToDisplay = eventsQueue[0];
        GameObject eventPanelObject = null;

        if (eventCanvas == null)
        {
            eventCanvas = Instantiate(eventCanvasModel);
        }

        eventPanelObject = eventCanvas.GetComponentInChildren<Image>().gameObject;

        if (eventTitleText == null)
        {
            eventTitleText = Instantiate(textElementModel, eventPanelObject.transform);
        }
        
        eventTitleText.GetComponent<TextMeshProUGUI>().text = "Event : " + eventToDisplay.Name;


        if(eventDescriptionText == null)
        {
            eventDescriptionText = Instantiate(textElementModel, eventPanelObject.transform);
        }
        
        eventDescriptionText.GetComponent<TextMeshProUGUI>().text = "Description : " + System.Environment.NewLine + eventToDisplay.Description;

        if(buttonGroup == null)
        {
            buttonGroup = Instantiate(buttonGroupModel, eventPanelObject.transform);
        }
        

        Button[] buttons = buttonGroup.GetComponentsInChildren<Button>();

        buttons[0].onClick.RemoveAllListeners();
        buttons[1].onClick.RemoveAllListeners();
        buttons[0].onClick.AddListener(() => { this.AcceptTheEvent(eventToDisplay); });
        buttons[1].onClick.AddListener(() => { this.DeclineTheEvent(eventToDisplay); });

        eventCanvas.SetActive(true);

    }

    private void AcceptTheEvent(WorldEvent eventDisplayed)
    {
        float criminalityRate = eventDisplayed.Cost.CriminalityRate;
        worldStatus.CriminalityRate += criminalityRate;
        if(worldStatus.CriminalityRate > 100f)
        {
            worldStatus.CriminalityRate = 100f;
        } else if(worldStatus.CriminalityRate < 0f)
        {
            worldStatus.CriminalityRate = 0f;
        }
        this.RemoveEventFromPool();
        eventCanvas.SetActive(false);
    }

    private void DeclineTheEvent(WorldEvent eventDisplayed)
    {
        float worldWealth = eventDisplayed.Cost.WorldWealth;
        worldStatus.Wealth += worldWealth;
        if(worldStatus.Wealth > 100f)
        {
            worldStatus.Wealth = 100f;
        } else if(worldStatus.Wealth < 0f)
        {
            worldStatus.Wealth = 0f;
        }
        this.RemoveEventFromPool();
        eventCanvas.SetActive(false);
    }
}
