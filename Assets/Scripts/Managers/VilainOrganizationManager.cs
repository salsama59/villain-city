using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VilainOrganizationManager : MonoBehaviour
{
    private VilainOrganization vilainOrganization;
    private readonly string ORGANIZATION_LEVEL_TEXT = "Level {}";
    private readonly string ORGANIZATION_FUND_TEXT = "{} cc";
    private readonly string ORGANIZATION_MINION_COUNT_TEXT = "{} minions";
    private TextMeshProUGUI levelTextUi;
    private TextMeshProUGUI fundTextUi;
    private TextMeshProUGUI minionCountTextUi;
    public GameObject upgradeOrganizationPanelModel;
    private GameObject upgradeOrganizationPanel;
    private GameObject actionMenuCanvas;
    public GameObject upgradeRequirementTextModel;
    public GameObject buttonGroupModel;
    private GameObject buttonGroup;
    private GameObject crimeCurrencyRequirementTextObject;
    private GameObject minionCountRequirementTextObject;
    private GameObject upgradeRequirementTitleTextObject;
    public List<Sprite> organizationSprites;
    private GameObject vilainOrganaizationObject;

    // Start is called before the first frame update
    void Start()
    {
        vilainOrganaizationObject = GameObjectUtils.GetVillainOrganizationObject();
        vilainOrganization = GameObjectUtils.GetGameManagerScript().VilainOrganization;
        levelTextUi = GameObjectUtils.GetOrganizationLevelTextComponentScript();
        fundTextUi = GameObjectUtils.GetOrganizationFundTextComponentScript();
        minionCountTextUi = GameObjectUtils.GetOrganizationMinionCoutTextComponentScript();
        actionMenuCanvas = GameObjectUtils.GetActionMenuCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateOrganizationUHD();
    }

    private void UpdateOrganizationUHD()
    {
        levelTextUi.text = this.ReplaceTextElement(ORGANIZATION_LEVEL_TEXT, vilainOrganization.Level.ToString());
        fundTextUi.text = this.ReplaceTextElement(ORGANIZATION_FUND_TEXT, vilainOrganization.Fund.ToString());
        minionCountTextUi.text = this.ReplaceTextElement(ORGANIZATION_MINION_COUNT_TEXT, vilainOrganization.MinionCount.ToString());
    }

    private string ReplaceTextElement(string textToModify, string replacementElement)
    {
        return textToModify.Replace("{}", replacementElement);
    }

    public void UpgradeOrganization()
    {
        float crimeCurrencyRequired = CalculateCrimeCurrencyRequired();
        int minionCountRequired = CalculateMinionsCountRequired();

        vilainOrganization.Level += 1;

        vilainOrganization.MinionCount -= minionCountRequired;
        if (vilainOrganization.MinionCount < 0)
        {
            vilainOrganization.MinionCount = 0;
        }

        vilainOrganization.Fund -= crimeCurrencyRequired;
        if (vilainOrganization.Fund < 0)
        {
            vilainOrganization.Fund = 0;
        }

        upgradeOrganizationPanel.SetActive(false);

        vilainOrganaizationObject.GetComponent<SpriteRenderer>().sprite = organizationSprites[vilainOrganization.Level - 1];


    }

    public void BackToActionMenu()
    {
        upgradeOrganizationPanel.SetActive(false);
    }

    public void DisplayUpgradeOrganizationMenu()
    {

        if (upgradeOrganizationPanel == null)
        {
            upgradeOrganizationPanel = Instantiate(upgradeOrganizationPanelModel, actionMenuCanvas.transform);
            Vector3 upgradeOrganizationPanelAnchoredPosition = upgradeOrganizationPanel.GetComponent<RectTransform>().anchoredPosition;
            upgradeOrganizationPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(200, -90, upgradeOrganizationPanelAnchoredPosition.z);
        }

        if (upgradeRequirementTitleTextObject == null)
        {
            upgradeRequirementTitleTextObject = Instantiate(upgradeRequirementTextModel, upgradeOrganizationPanel.transform);
        }

        if (crimeCurrencyRequirementTextObject == null)
        {
            crimeCurrencyRequirementTextObject = Instantiate(upgradeRequirementTextModel, upgradeOrganizationPanel.transform);
        }

        if (minionCountRequirementTextObject == null)
        {
            minionCountRequirementTextObject = Instantiate(upgradeRequirementTextModel, upgradeOrganizationPanel.transform);
        }

        bool isMeetingRequirement = false;

        float crimeCurrencyRequired = CalculateCrimeCurrencyRequired();
        int minionCountRequired = CalculateMinionsCountRequired();
        upgradeRequirementTitleTextObject.GetComponent<TextMeshProUGUI>().text = "Upgrade Requirement";
        crimeCurrencyRequirementTextObject.GetComponent<TextMeshProUGUI>().text = "Required : " + crimeCurrencyRequired + " cc";
        minionCountRequirementTextObject.GetComponent<TextMeshProUGUI>().text = "Required : " + minionCountRequired + " minions";


        isMeetingRequirement = vilainOrganization.Fund >= crimeCurrencyRequired && vilainOrganization.MinionCount >= minionCountRequired;

        if (buttonGroup == null)
        {
            buttonGroup = Instantiate(buttonGroupModel, upgradeOrganizationPanel.transform);
            Button[] buttons = buttonGroup.GetComponentsInChildren<Button>();

            foreach (Button button in buttons)
            {
                if (button.gameObject.CompareTag(TagConstants.ACCEPT_MISSION_BUTTON_TAG))
                {
                    button.onClick.AddListener(() => { this.UpgradeOrganization(); });

                    button.interactable = false;
                }
                else if (button.gameObject.CompareTag(TagConstants.CANCEL_MISSION_BUTTON_TAG))
                {
                    button.onClick.AddListener(() => { this.BackToActionMenu(); });
                }
            }

        }

        Button acceptButton = buttonGroup.GetComponentsInChildren<Button>().Where((buttonElement) => { return buttonElement.gameObject.CompareTag(TagConstants.ACCEPT_MISSION_BUTTON_TAG); }).First();
        acceptButton.interactable = isMeetingRequirement;

        upgradeOrganizationPanel.SetActive(true);

    }

    private int CalculateMinionsCountRequired()
    {
        return vilainOrganization.Level * 3;
    }

    private float CalculateCrimeCurrencyRequired()
    {
        return (vilainOrganization.Level + 1) * 15f;
    }
}
