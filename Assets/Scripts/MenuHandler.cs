using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button menuBtn;
    public Button closeBtn;
    public Button buildBtn;
    public Button closedBtnBuild;
    public GameObject menuSelection;
    public GameObject menuSelectionBuild;

    void Start()
    {
        AddButtonListener(menuBtn, ShowMenuSelection);
        AddButtonListener(closeBtn, HideMenuSelection);
        AddButtonListener(buildBtn, ShowMenuSelectionBuild);
        AddButtonListener(closedBtnBuild, HideMenuSelectionBuild);
    }

    void AddButtonListener(Button button, UnityEngine.Events.UnityAction action)
    {
        if (button != null)
        {
            button.onClick.AddListener(action);
        }
    }

    void ShowMenuSelection()
    {
        // Set the menuSelection GameObject to active
        menuSelection.SetActive(true);
    }

    void HideMenuSelection()
    {
        // Set the menuSelection GameObject to inactive
        menuSelection.SetActive(false);
    }

    void ShowMenuSelectionBuild()
    {
        // Set the menuSelectionBuild GameObject to active
        menuSelectionBuild.SetActive(true);
        HideMenuSelection();
    }

    void HideMenuSelectionBuild()
    {
        // Set the menuSelectionBuild GameObject to inactive
        menuSelectionBuild.SetActive(false);
    }
}