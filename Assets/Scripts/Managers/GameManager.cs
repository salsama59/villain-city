using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private VilainOrganization vilainOrganization;
    public GameObject vilainOrganizationManagerModel;
    public GameObject timeWeatherManagerModel;
    public GameObject organizationFundManager;
    public GameObject missionManager;
    public GameObject actionMenu;

    // Start is called before the first frame update
    void Start()
    {
        VilainOrganization = new VilainOrganization(0f, 1, 1, new List<StolenObject>());
        GameObject menu = Instantiate(actionMenu, this.transform.position, Quaternion.identity);
        /*Vector2 actionMenuPanelSizeDelta = menu.GetComponentInChildren<Canvas>().gameObject.GetComponentInChildren<RectTransform>().sizeDelta;*/
        GameObject actionMenuPanel = menu.GetComponentInChildren<Canvas>().gameObject.GetComponentInChildren<RectTransform>().gameObject;
        Button[] menuButtons = actionMenuPanel.GetComponentsInChildren<Button>();

        Button missionsButton = null;
        Button ugradeOrganizationButton = null;
        foreach (Button button in menuButtons)
        {
            if (button.gameObject.CompareTag(TagConstants.MENU_MISSION_BUTTON_TAG))
            {
                missionsButton = button;
            } else if(button.gameObject.CompareTag(TagConstants.MENU_UPGRADE_ORGANIZATION_BUTTON_TAG))
            {
                ugradeOrganizationButton = button;
            }
        }

        GameObject vilainOrganizationManagerGameObject = Instantiate(vilainOrganizationManagerModel, this.transform.position, Quaternion.identity);
        VilainOrganizationManager vilainOrganizationManagerScript = vilainOrganizationManagerGameObject.GetComponent<VilainOrganizationManager>();

       /* Vector2 ugradeOrganizationButtonSizeDelta = ugradeOrganizationButton.gameObject.GetComponent<RectTransform>().sizeDelta;
        ugradeOrganizationButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(ugradeOrganizationButtonSizeDelta.x, 50f);*/
        ugradeOrganizationButton.onClick.AddListener(() => { vilainOrganizationManagerScript.DisplayUpgradeOrganizationMenu(); });

        Instantiate(timeWeatherManagerModel, this.transform.position, Quaternion.identity);
        Instantiate(organizationFundManager, this.transform.position, Quaternion.identity);

        GameObject missionManagerGameObject = Instantiate(missionManager, this.transform.position, Quaternion.identity);
        MissionManager missionManagerScript = missionManagerGameObject.GetComponent<MissionManager>();
        missionsButton.onClick.AddListener(() => { missionManagerScript.OpenMissionListMenu(); });


        menu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public VilainOrganization VilainOrganization { get => vilainOrganization; set => vilainOrganization = value; }
}
