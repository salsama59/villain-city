using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private readonly string CRIMINALITY_RATE_TEXT = "Criminality : {} %";
    private readonly string WEALTH_TEXT = "Wealth : {}";
    public GameObject worldStatusCanvas;
    public GameObject worldCriminalityRateTextObject;
    public GameObject worldWealthTextObject;
    private WorldStatus worldStatus;
    private TextMeshProUGUI wordlCriminalityRateText;
    private TextMeshProUGUI worldWealthText;

    public WorldStatus WorldStatus { get => worldStatus; set => worldStatus = value; }

    // Start is called before the first frame update
    void Start()
    {
        WorldStatus = new WorldStatus(1f, 30f);
        wordlCriminalityRateText = worldCriminalityRateTextObject.GetComponent<TextMeshProUGUI>();
        worldWealthText = worldWealthTextObject.GetComponent<TextMeshProUGUI>();
        worldStatusCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateWorldStatus();
    }

    private string ReplaceTextElement(string textToModify, string replacementElement)
    {
        return textToModify.Replace("{}", replacementElement);
    }

    private void UpdateWorldStatus()
    {
        wordlCriminalityRateText.text = this.ReplaceTextElement(CRIMINALITY_RATE_TEXT, WorldStatus.CriminalityRate.ToString());
        worldWealthText.text = this.ReplaceTextElement(WEALTH_TEXT, WorldStatus.Wealth.ToString());
    }
}
